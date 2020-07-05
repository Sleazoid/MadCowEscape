using System.Collections;
using System.Collections.Generic;
using System.Drawing;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class WacoomInputTopDown : MonoBehaviour
{
    InputActions inputActions;
    Vector2 valuePress;
    Rigidbody2D rb;
    Vector2 pressStartPos;
    Vector2 pressEndPos;
    [SerializeField]
    private float rollForce;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float shootForce;
    [SerializeField]
    private float shootDelay;
    public float MaxY;
    public float minY;
    public float maxX;
    public float minX;
    private Vector2 curPenPos;
    private bool bt1Press = false;
    private bool drawing = false;
    //private float screenHeight;
    //private float screenWidth;
    private bool inRange = false;
    private Camera cam;
    //[SerializeField]
    //private GameObject bulletPrefab;
    [SerializeField]
    private GameObject cowBloodPrefab;
    private bool easeToPos;
    private Vector2 lastPosOutPlayerRange;
    private float timeFromLastTip = 0f;
    [SerializeField]
    private float rollTipTime = 0.3f;
    private Animator anim;
    private bool attacking = false;
    private Vector3 penPosWorld;
    private Vector2 penDir;
    private DashTrail dashTrail;
    [SerializeField]
    private float dyingForce = 5f;
    private bool dead = false;
    private SpriteRenderer rend;
    private bool dashEnabled = true;
    [SerializeField]
    private float dashDisabledTime = 0.2f;
    [SerializeField]
    private float dashAttackTime = 0.2f;
    private CharacterSounds sounds;
    [SerializeField]
    private GameObject bloodGO;
    private Pen curPen;
    [SerializeField]
    private GameObject bloodParticles;
    private ParticleCollision particleCol;
    Vector2 leftStickValue;
    [SerializeField]
    private float gPadRotationSpeed;
    [SerializeField]
    private bool UseGamepad = true;
    private Mouse vMouse;
    public bool Dead { get => dead; set => dead = value; }

    public delegate void OnAttackStateChange(bool state);
    public static event OnAttackStateChange AttackEvent;
    private void Awake()
    {
        inputActions = new InputActions();
        inputActions.Wacom.Press.performed += ctx => WhatHappens();
        inputActions.Wacom.Press.canceled += ctx => PressCancelled();
        inputActions.Wacom.Press.started += ctx => PressStarted();
        valuePress = inputActions.Wacom.Press.ReadValue<Vector2>();
        inputActions.Wacom.oo.performed += ctx => Tip();
        inputActions.Wacom.Range.performed += ctx => InRange();
        inputActions.Wacom.Range.canceled += ctx => InRangeCancelled();
        inputActions.Wacom.btn1.performed += ctx => Btn1();
        inputActions.Wacom.btn1.canceled += ctx => Btn1Released();
        //inputActions.Wacom.btn2.performed += ctx => Btn2();
        //inputActions.Wacom.btn3.performed += ctx => Btn3();
        //inputActions.Wacom.btn4.performed += ctx => Btn4();
        inputActions.Wacom.eraser.performed += ctx => Eraser();
        curPen = Pen.current;
        // inputActions.Wacom.Press.performed += ctx =>  valuePress = ctx.ReadValue<Vector2>();

        //Gamepad
        inputActions.Gamepad.Dash.performed += ctx => Attack();
        //leftStickValue = inputActions.Gamepad.LeftStick.ReadValue<Vector2>();
        inputActions.Gamepad.LeftStick.performed += ctx => leftStickValue = ctx.ReadValue<Vector2>();
        //inputActions.Gamepad.LeftStick.performed += ctx => InRange(); 
        // inputActions.Gamepad.LeftStick.canceled += ctx => leftStickValue = new Vector2(0,0);
        //  inputActions.Gamepad.LeftStick.canceled += ctx => InRangeCancelled();
        vMouse = InputSystem.AddDevice<Mouse>();
    }
    // Start is called before the first frame update
    void Start()
    {
        // Cursor.visible = false;
        rb = GetComponent<Rigidbody2D>();
        //screenHeight = (Screen.height);
        //screenWidth = (Screen.width);
        cam = Camera.main;
        anim = GetComponent<Animator>();
        dashTrail = GetComponent<DashTrail>();
        rend = GetComponent<SpriteRenderer>();
        sounds = GetComponent<CharacterSounds>();
        if (bloodParticles)
        {
            particleCol = bloodParticles.GetComponent<ParticleCollision>();
        }
        if(UseGamepad)
        {
            rb.drag = 8f;
            moveSpeed += 0.21f;
            //dashDisabledTime += 0.2f;
        }
        // sounds.PlayNoticedClip();
        UseGamepad = GameManager.Instance.UseGamepad;
    }
    private void LeftStickControlCancelled()
    {

    }
    public void Eraser()
    {
        Debug.Log("eraser");
    }
    public void Btn1()
    {
        Debug.Log("btn111");
        Attack();
        bt1Press = true;
    }
    public void Btn1Released()
    {
        Debug.Log("btn111 released");
        // rb.AddForce(this.transform.up * swipeForce);

        bt1Press = false;
    }
    //public void Btn3()
    //{
    //    Debug.Log("btn3");
    //}
    //public void Btn2()
    //{
    //    Debug.Log("btn2");
    //}
    //public void Btn4()
    //{
    //    Debug.Log("btn4");
    //}
    public void InRange()
    {
        inRange = true;
        //  Debug.Log("WuuT");

    }
    public void InRangeCancelled()
    {
        Debug.Log("Not In RAnge");
        inRange = false;
        anim.SetFloat("Speed", 0);
    }
    public void Tip()
    {
        curPenPos = curPen.position.ReadValue();

        if (timeFromLastTip <= rollTipTime )
        {
            Attack();
        }
        timeFromLastTip = 0;
    }
    private void Attack()
    {
        if (!Dead && dashEnabled)
        {
            // Debug.Log("Attack");
            AttackEvent?.Invoke(true);
            CancelInvoke("RollIsOver");

            anim.SetBool("Attack", true);

            attacking = true;
            rb.AddForce(transform.right * rollForce, ForceMode2D.Impulse);
            dashTrail.InvokeRepeating("SpawnTrailPart", 0, 0.05f);
            InvokeRepeating("AttackRepeating", 0.05f, 0.05f);
            Invoke("RollIsOver", dashAttackTime);
            sounds.PlayAttackClip();
            dashEnabled = false;
            Invoke("EnableDash", dashDisabledTime);
        }
    }
    public void EnableDash()
    {
        dashEnabled = true;
    }
    private void AttackRepeating()
    {
        AttackEvent?.Invoke(true);
    }
    public void RollIsOver()
    {
        dashTrail.CancelInvoke("SpawnTrailPart");
        CancelInvoke("AttackRepeating");
        attacking = false;
        anim.SetBool("Attack", false);
        AttackEvent?.Invoke(false);
    }

    public void WhatHappens()
    {
        //Debug.Log("Press performed");
    }
    public void SetAttackAnimFalse()
    {
        anim.SetBool("Attack", false);
    }
    public void PressStarted()
    {
        if (!Dead)
        {

            // drawing = true;
            // Debug.Log("Press Started");
            drawing = true;
            pressStartPos = curPen.position.ReadValue();
            anim.SetFloat("Speed", 0);
            //InvokeRepeating("Shoot", 0.2f, shootDelay);
        }
    }
    public void PressCancelled()
    {
        //drawing = false;
        //Debug.Log("Press Cancelled");
        pressEndPos = curPen.position.ReadValue();
        drawing = false;

        //SwipeObject();
        // CancelInvoke("Shoot");
    }
    private void OnEnable()
    {
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();
        CancelInvoke();
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(this.transform.position, 1f);
        Gizmos.DrawRay(this.transform.position, transform.right * 5);
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log("leftStickValue " + leftStickValue);

        //    var mouseDelta = leftStickValue * 2f;
        //    var currentPosition = vMouse.position.ReadValue();
        //    InputSystem.QueueStateEvent(vMouse,
        //        new MouseState
        //        {
        //            position = currentPosition + mouseDelta,
        //            delta = mouseDelta,
        //    // Set other stuff like button states...
        //});

        if (!UseGamepad)
        {
            if (!Dead)
            {
                timeFromLastTip += Time.deltaTime;
                curPenPos = curPen.position.ReadValue();
                penPosWorld = cam.ScreenToWorldPoint(curPenPos);
                penDir = (penPosWorld - this.transform.position);
                if (!attacking)
                {
                    float angle = Mathf.Atan2(penDir.y, penDir.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                }
                if (!drawing && inRange)
                {
                    anim.SetFloat("Speed", penDir.sqrMagnitude);
                }
            }
        }
        else
        {
            if (!Dead)
            {
                //timeFromLastTip += Time.deltaTime;
                //curPenPos = curPen.position.ReadValue();
                //penPosWorld = cam.ScreenToWorldPoint(curPenPos);
                //penDir = (penPosWorld - this.transform.position);
                //if (rb.velocity.normalized > 0.1f)
                //    anim.SetFloat("Speed", rb.velocity.magnitude);
                //else
                //    anim.SetFloat("Speed", 0f);
                anim.SetFloat("Speed", leftStickValue.sqrMagnitude);
                if (!attacking && leftStickValue.sqrMagnitude > 0)
                {
                    //float rotationY = leftStickValue.y * gPadRotationSpeed;
                    //float rotationX = leftStickValue.x * gPadRotationSpeed;

                    //transform.Rotate(Vector3.forward * rotationX);

                    //Quaternion desRot = Quaternion.LookRotation(Vector2.right, leftStickValue);
                    //transform.rotation = Quaternion.RotateTowards(this.transform.rotation, desRot, gPadRotationSpeed * Time.deltaTime);
                    transform.RotateAround(this.transform.position, leftStickValue, gPadRotationSpeed * Time.deltaTime);
                    float angle = Mathf.Atan2(leftStickValue.y, leftStickValue.x) * Mathf.Rad2Deg;
                    Quaternion eulerRot = Quaternion.Euler(0.0f, 0.0f, angle);
                    transform.rotation = Quaternion.Slerp(transform.rotation, eulerRot, Time.deltaTime * gPadRotationSpeed);

                    //float angle = Mathf.Atan2(leftStickValue.y, leftStickValue.x) * Mathf.Rad2Deg;
                    //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                  
                }
                //else
                //{
                //    rb.velocity = new Vector2(0, 0);
                //}
                // if (leftStickValue.sqrMagnitude>0)
                //{

                // }
            }
        }

    }
    private void FixedUpdate()
    {
        if (!UseGamepad)
        {
            if (!Dead)
            {
                if (inRange && !drawing && !attacking)
                {

                    if (Vector2.Distance(penPosWorld, this.transform.position) > 0.3f)
                    {
                        // Debug.Log(wPos);

                        //Vector2 dirNormalized = penDir.normalized;
                        //rb.MovePosition(rb.position + dirNormalized * moveSpeed * Time.fixedDeltaTime);
                        Vector2 dirForward = transform.right;
                        //rb.MovePosition(rb.position + dirForward * moveSpeed * Time.fixedDeltaTime);
                        rb.velocity = dirForward * moveSpeed;//, rb.velocity.y);
                        easeToPos = false;
                        lastPosOutPlayerRange = penPosWorld;
                    }
                    else
                    {
                        easeToPos = true;
                    }
                }
                if (easeToPos && !attacking) // 
                {
                    rb.position = Vector2.MoveTowards(transform.position, lastPosOutPlayerRange, moveSpeed * Time.fixedDeltaTime);
                }
            }
        }
        else
        {
            if (!Dead && leftStickValue.sqrMagnitude > 0 && !attacking)
            {
                Vector2 dirForward = transform.right;
                rb.velocity = dirForward * moveSpeed;//, rb.velocity.y);
            }
        }


    }

    public void GetDamage(Vector3 enemyPos)
    {
        if (!Dead && !attacking)
        {
            DoDeadThings(enemyPos);
            //bloodGO.SetActive(true);
            //bloodGO.transform.rotation = this.transform.rotation;
        }


    }
    public void Muuu()
    {
        sounds.PlayNoticedClip();
    }
    private void DoDeadThings(Vector3 enemyPos)
    {
        sounds.PlayDeathClip();
        rend.sortingOrder = 2;
        Vector2 dirImpact = this.transform.position - enemyPos;
        rb.AddForce(dirImpact.normalized * dyingForce, ForceMode2D.Impulse);
        Dead = true;
        anim.SetBool("Dead", true);
        GameManager.Instance.PlayerDead();
        Invoke("InstantiateBlood", 0.2f);
    }
    public void PlayerGotShot(Vector3 enemyPos)
    {
        if (!Dead && !attacking)
        {
        
            DoDeadThings(enemyPos);
            Vector2 dirImpact = this.transform.position - enemyPos;
            float angle = Mathf.Atan2(dirImpact.y, dirImpact.x) * Mathf.Rad2Deg;
            particleCol.ParticlesOn();
            bloodParticles.transform.eulerAngles = new Vector3(0, 0, angle);
            bloodParticles.GetComponent<Animator>().SetTrigger("Start");
            bloodParticles.GetComponent<ParticleSystem>().Play();
        }
    }
    public void PlayerGotDriveOver(Vector3 enemyPos)
    {
        if (!Dead)
        {
            DoDeadThings(enemyPos);
            Vector2 dirImpact = this.transform.position - enemyPos;
            float angle = Mathf.Atan2(dirImpact.y, dirImpact.x) * Mathf.Rad2Deg;
            particleCol.ParticlesOn();
            bloodParticles.transform.eulerAngles = new Vector3(0, 0, angle);
            bloodParticles.GetComponent<Animator>().SetTrigger("Start");
            bloodParticles.GetComponent<ParticleSystem>().Play();
        }
    }
    private void InstantiateBlood()
    {
        bloodGO.SetActive(true);
        bloodGO.transform.rotation = this.transform.rotation;
        // GameObject bloodGO = Instantiate(cowBloodPrefab, this.transform.position, this.transform.rotation);
    }
    //private void Shoot()
    //{
    //    Debug.Log("Shoot");
    //    Vector3 wPos = cam.ScreenToWorldPoint(curPenPos);
    //    // Debug.Log(wPos);
    //    Vector2 dir = (wPos - this.transform.position);
    //    Vector2 dirNormalized = dir.normalized;
    //    GameObject bullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);
    //    bullet.GetComponent<Rigidbody2D>().AddForce(dirNormalized * shootForce, ForceMode2D.Impulse);

    //}
    //private void SwipeObject()
    //{
    //    Vector2 dir = (pressEndPos - pressStartPos);
    //    Vector2 ranged = (dir / 100) * 2f;
    //    // if(ranged>scre)
    //    Debug.Log(dir);
    //    rb.AddForce(ranged/*.normalized* swipeForce*/, ForceMode2D.Impulse);
    //}

    private void OnDestroy()
    {
        CancelInvoke();
    }
}
