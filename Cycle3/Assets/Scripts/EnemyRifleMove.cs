using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemyRifleMove : MonoBehaviour
{
    private Transform player;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float fireRate;
    [SerializeField]
    private float shootForce;
    [SerializeField]
    private GameObject bulletPrefab;
    private bool moving = true;
    private Rigidbody2D rb;
    private WacoomInputTopDown playerMove;
    private Animator anim;
    private bool dead = false;
    private CapsuleCollider2D triggerCol;
    private bool isOnPlayerRange = false;
    [SerializeField]
    private float impactForce = 2;
    [SerializeField]
    private GameObject bloodParticles;
    private ParticleCollision particleCol;
    private EnemyShotgunRifleAreaScript noticeArea;
    private CharacterSounds sounds;
    private bool huntPlayer = false;
    private bool rotateTowardsPlayer = true;
    private bool shootRotating = false;
    [SerializeField]
    private float hitDistance = 0.4f;
    public LayerMask IgnoreMe;
    private bool walkAway = false;
    private bool playerDead = false;
    private Quaternion targetRotation;
    private float targetAngle;
    private float prevZRot;
    [SerializeField]
    public float rotationTime = 10f;
    private AudioSource audiosource;
    private SpriteRenderer rend;
    [SerializeField]
    private float stillTimeAfterShot = 1f;
    public bool HuntPlayer { get => huntPlayer; set => huntPlayer = value; }
    public bool WalkAway { get => walkAway; set => walkAway = value; }
    public float HitDistance { get => hitDistance; set => hitDistance = value; }

    // Start is called before the first frame update
    void Start()
    {
        sounds = GetComponent<CharacterSounds>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerMove = player.GetComponent<WacoomInputTopDown>();
        //StartCoroutine("ShootPlayer");
        triggerCol = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        particleCol = bloodParticles.GetComponent<ParticleCollision>();
        noticeArea = transform.GetComponentInChildren<EnemyShotgunRifleAreaScript>();
        rend = GetComponent<SpriteRenderer>();
        audiosource = GetComponent<AudioSource>();
        targetRotation = transform.rotation;
    }
    private void OnEnable()
    {
        WacoomInputTopDown.AttackEvent += EnemyIsDead;
    }
    private void OnDisable()
    {
        WacoomInputTopDown.AttackEvent -= EnemyIsDead;
    }
    // Update is called once per frame

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(this.transform.position, transform.right * HitDistance);
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
    }
    public void RaycastShooting()
    {


        if (!playerDead)
        {
            Vector3 rayDir = this.transform.right;
            // Debug.DrawRay(this.transform.position, rayDir * hitDistance);
            //  Vector3 rayDir = playerTransform.position - this.transform.position;
            RaycastHit2D hit = Physics2D.Raycast(this.transform.position, rayDir, HitDistance, ~IgnoreMe);
            if (hit)
            {
                // Debug.Log("hit "+ hit.collider.gameObject.name);
                if (hit.collider.gameObject.tag.Equals("Player"))
                {
                    //ShootBehaviour();
                    //Debug.Log("hit plaay");
                    playerMove.PlayerGotShot(this.transform.position);
                }

            }
        }

    }
    //private IEnumerator ShootPlayer()
    //{
    //    while (1 == 1)
    //    {
    //        moving = true;
    //        yield return new WaitForSeconds(timeToShoot);
    //        moving = false;
    //        yield return new WaitForSeconds(0.5f);
    //        ShootBullet();

    //    }
    //    //  yield return null;
    //}
    //private void ShootBullet()
    //{
    //    Vector2 dirNormalized = (player.position - this.transform.position).normalized;
    //    GameObject bullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);
    //    bullet.GetComponent<Rigidbody2D>().AddForce(dirNormalized * shootForce, ForceMode2D.Impulse);
    //}
    private void Update()
    {
        if (!dead && HuntPlayer && rotateTowardsPlayer && !playerMove.Dead)
        {
            Vector2 dirNormalized = (player.position - this.transform.position).normalized;
            float angle = Mathf.Atan2(dirNormalized.y, dirNormalized.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            //targetRotation *= Quaternion.AngleAxis(angle, Vector3.forward);

            //transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationTime * Time.deltaTime);
        }
        if (shootRotating)
        {
            //Vector2 dirNormalized = (player.position - this.transform.position).normalized;

            //Vector2 playerDir = player.position + player.right * 100;
            //// if()

            //float angle = Mathf.Atan2(dirNormalized.y, dirNormalized.x) * Mathf.Rad2Deg;
            ////transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            //targetRotation *= Quaternion.AngleAxis(angle, Vector3.forward);

            //transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationTime * Time.deltaTime);
            if(targetAngle==0)
            {
                Vector2 dirNormalized = (player.position - this.transform.position).normalized;
                float angle = Mathf.Atan2(dirNormalized.y, dirNormalized.x) * Mathf.Rad2Deg;
                targetRotation *= Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationTime * Time.deltaTime);
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, targetAngle), rotationTime * Time.deltaTime);

            }

        }
    }
    private void FixedUpdate()
    {

        if (playerMove.Dead && walkAway && !dead)
        {
            Vector2 dirNormalized = (this.transform.position - player.position).normalized;
            rb.MovePosition(rb.position + dirNormalized * moveSpeed * Time.fixedDeltaTime);
            moveSpeed = 0.5f;
            float angle = Mathf.Atan2(dirNormalized.y, dirNormalized.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            //targetRotation *= Quaternion.AngleAxis(angle, Vector3.forward);

            //transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationTime * Time.deltaTime);
        }

    }

    public void TryHitPlayer()
    {
        playerMove.GetDamage(this.transform.position);
        anim.SetBool("Hitting", false);
    }
    private void EnemyIsDead(bool value)
    {
        if (isOnPlayerRange && !dead)
        {
            audiosource.loop = false;
            shootRotating = false;
            StopShooting();
            CancelInvoke();
            playerMove.EnableDash();
            GameManager.Instance.CollisionImpulseEffect();
            anim.Play("RifleGuyDeath");
            noticeArea.gameObject.SetActive(false);
            sounds.PlayDeathClip();
            //  int randomZRot = Random.Range(0, 360);
            Vector2 dirImpact = this.transform.position - player.position;
            float angle = Mathf.Atan2(dirImpact.y, dirImpact.x) * Mathf.Rad2Deg;
            particleCol.ParticlesOn();
            bloodParticles.transform.eulerAngles = new Vector3(0, 0, angle);
            bloodParticles.GetComponent<Animator>().SetTrigger("Start");
            bloodParticles.GetComponent<ParticleSystem>().Play();
            //anim.SetBool("Dead", true);
            dead = true;
            triggerCol.enabled = false;
            Vector2 impactDir = this.transform.position - player.position;
            rb.AddForce(impactDir.normalized * impactForce, ForceMode2D.Impulse);
            StartCoroutine(StopBlood());
            WacoomInputTopDown.AttackEvent -= EnemyIsDead;
            GameManager.Instance.EnemyDied();
            rend.sortingOrder = 0;
            rb.simulated = false;
        }

    }

    private IEnumerator StopBlood()
    {
        yield return new WaitForSeconds(0.3f);
        bloodParticles.GetComponent<ParticleSystem>().Stop();
        rb.velocity = new Vector2(0, 0);
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            isOnPlayerRange = true;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            isOnPlayerRange = false;
        }
    }
    public void StartShooting()
    {
        if (!playerDead)
        {
            anim.SetBool("Shooting", false);
            sounds.PlayAttackClip();
            // InvokeRepeating("ShootInvoke", fireRate, fireRate);
            // StartCoroutine("ShootingTiming");
            Invoke("ShootingTiming", fireRate);
            Invoke("UpdatePrevZRot", 0.2f);
        }

    }
    private void UpdatePrevZRot()
    {
        // Tämä vain katoo mihin suuntaan ollaan pöyrimässä
        prevZRot = transform.rotation.eulerAngles.z;
    }
    private void ShootingTiming()
    {
        //while (1 == 1)
        //{

        Shoot();

        //    yield return null;
        //}

    }

    public void StopShooting()
    {
        StopAllCoroutines();
        anim.Play("haulikkoDudeIdle");

    }
    public void Shoot()
    {
        CancelInvoke("UpdatePrevZRot");
        float diff = transform.rotation.eulerAngles.z - prevZRot;
        if (diff < 0)
        {
            targetAngle = transform.rotation.eulerAngles.z - 90f;
        }
        else
        {
            targetAngle = transform.rotation.eulerAngles.z + 90f;
        }
        if (Mathf.Abs(diff) < 5f)
        {
            targetAngle = 0;
        }
        audiosource.loop = true;
        rotateTowardsPlayer = false;
        // targetRotation = transform.rotation;
        shootRotating = true;
        anim.SetBool("Shooting", true);

        ShootBehaviour();

    }
    private void ShootBehaviour()
    {
        sounds.PlayGunClip();
        rotateTowardsPlayer = false;
        Invoke("StartRotateTowardsPlayer", stillTimeAfterShot);

    }
    private void StartRotateTowardsPlayer()
    {
        anim.SetBool("Shooting", false);
        audiosource.loop = false;
        audiosource.Stop();
        rotateTowardsPlayer = true;
        shootRotating = false;
        Invoke("StartShooting", fireRate);
    }

    public void PlayerIsDead()
    {
        playerDead = true;
        StopAllCoroutines();

        if (!dead)
        {
            anim.Play("haulikkoDudeIdle");
            Invoke("StartWalkingAway", 0.8f);
        }
    }
    private void StartWalkingAway()
    {

        anim.SetBool("Shooting", false);
        audiosource.loop = false;
        audiosource.Stop();
        rotateTowardsPlayer = true;
        shootRotating = false;
        walkAway = true;
        anim.Play("rifleGuyWalk");

    }
}
