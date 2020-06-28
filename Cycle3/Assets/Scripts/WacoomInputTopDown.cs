using System.Collections;
using System.Collections.Generic;
using System.Drawing;

using UnityEngine;
using UnityEngine.InputSystem;

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
    private float screenHeight;
    private float screenWidth;
    private bool inRange = false;
    private Camera cam;
    //[SerializeField]
    //private GameObject bulletPrefab;

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
    private CharacterSounds sounds;
    [SerializeField]
    private GameObject bloodGO;
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
        inputActions.Wacom.btn2.performed += ctx => Btn2();
        inputActions.Wacom.btn3.performed += ctx => Btn3();
        inputActions.Wacom.btn4.performed += ctx => Btn4();
        inputActions.Wacom.eraser.performed += ctx => Eraser();
        // inputActions.Wacom.Press.performed += ctx =>  valuePress = ctx.ReadValue<Vector2>();

    }
    // Start is called before the first frame update
    void Start()
    {
        // Cursor.visible = false;
        rb = GetComponent<Rigidbody2D>();
        screenHeight = (Screen.height);
        screenWidth = (Screen.width);
        cam = Camera.main;
        anim = GetComponent<Animator>();
        dashTrail = GetComponent<DashTrail>();
        rend = GetComponent<SpriteRenderer>();
        sounds = GetComponent<CharacterSounds>();
       // sounds.PlayNoticedClip();
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
    public void Btn3()
    {
        Debug.Log("btn3");
    }
    public void Btn2()
    {
        Debug.Log("btn2");
    }
    public void Btn4()
    {
        Debug.Log("btn4");
    }
    public void InRange()
    {
        inRange = true;
      //  Debug.Log("WuuT");

    }
    public void InRangeCancelled()
    {
      //  Debug.Log("Not In RAnge");
        inRange = false;
        anim.SetFloat("Speed", 0);
    }
    public void Tip()
    {
        curPenPos = Pen.current.position.ReadValue();

        if (timeFromLastTip <= rollTipTime)
        {
            Attack();
        }
        timeFromLastTip = 0;
    }
    private void Attack()
    {
        if (!Dead)
        {
            // Debug.Log("Attack");
            AttackEvent?.Invoke(true);
            CancelInvoke("RollIsOver");

            anim.SetBool("Attack", true);

            attacking = true;
            rb.AddForce(transform.right * rollForce, ForceMode2D.Impulse);
            dashTrail.InvokeRepeating("SpawnTrailPart", 0, 0.05f);
            InvokeRepeating("AttackRepeating",0.05f, 0.05f);
            Invoke("RollIsOver", 0.3f);
            sounds.PlayAttackClip();
        }
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

    public void PressStarted()
    {
        if (!Dead)
        {
          
            // drawing = true;
            // Debug.Log("Press Started");
            drawing = true;
            pressStartPos = Pen.current.position.ReadValue();
            anim.SetFloat("Speed", 0);
            //InvokeRepeating("Shoot", 0.2f, shootDelay);
        }
    }
    public void PressCancelled()
    {
        //drawing = false;
        //Debug.Log("Press Cancelled");
        pressEndPos = Pen.current.position.ReadValue();
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
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(this.transform.position, 1f);
        Gizmos.DrawRay(this.transform.position, transform.right * 5);
    }
    // Update is called once per frame
    void Update()
    {
        if (!Dead)
        {
            timeFromLastTip += Time.deltaTime;
            curPenPos = Pen.current.position.ReadValue();
            penPosWorld = cam.ScreenToWorldPoint(curPenPos);
            penDir = (penPosWorld - this.transform.position);
            if (!attacking)
            {
                float angle = Mathf.Atan2(penDir.y, penDir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }

            // Debug.Log(penDir);
            if (!drawing && inRange)
            {
                anim.SetFloat("Speed", penDir.sqrMagnitude);
            }

            //if (bt1Press)
            //{
            //    //   rb.AddForce(this.transform.up * swipeForce);
            //}
            //Debug.Log(Pen.current.device.displayName);
            //Debug.Log(Pen.current.position.ReadValue());
            //Debug.Log(Pen.current.tilt.ReadValue());

            //if (curPenPos.x > maxX)
            //{
            //    maxX = curPenPos.x;
            //}
            //if (curPenPos.x < minX)
            //{
            //    minX = curPenPos.x;
            //}
            //if (curPenPos.y > MaxY)
            //{
            //    MaxY = curPenPos.y;
            //}
            //if (curPenPos.y < minY)
            //{
            //    minY = curPenPos.y;
            //}
        }
    }
    private void FixedUpdate()
    {
        if (!Dead)
        {
           

            if (drawing)
            {

            }
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

    public void GetDamage(Vector3 enemyPos)
    {
        if(!Dead && !attacking)
        {
            sounds.PlayDeathClip();
            rend.sortingOrder = 0;
            Vector2 dirImpact = this.transform.position - enemyPos;
            rb.AddForce(dirImpact.normalized * dyingForce, ForceMode2D.Impulse);
            Dead = true;
            anim.SetBool("Dead", true);
            GameManager.Instance.PlayerDead();
            bloodGO.SetActive(true);
            bloodGO.transform.rotation = this.transform.rotation;
        }
       

    }
    public void Muuu()
    {
        sounds.PlayNoticedClip();
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


}
