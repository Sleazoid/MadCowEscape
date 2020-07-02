using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemyGrenadeMove : MonoBehaviour
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
    private EnemyShotgunNoticeAreaScript noticeArea;
    private CharacterSounds sounds;
    private bool huntPlayer = false;
    private bool rotateTowardsPlayer = true;
    [SerializeField]
    private float hitDistance = 0.4f;
    public LayerMask IgnoreMe;
    private bool walkAway = false;
    private bool playerDead = false;
    private SpriteRenderer rend;
    [SerializeField]
    private float stillTimeAfterShot = 1f;
    public bool HuntPlayer { get => huntPlayer; set => huntPlayer = value; }
    public bool WalkAway { get => walkAway; set => walkAway = value; }

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
        noticeArea = transform.GetComponentInChildren<EnemyShotgunNoticeAreaScript>();
        rend = GetComponent<SpriteRenderer>();
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
        Gizmos.DrawRay(this.transform.position, transform.right * hitDistance);
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
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

    private void FixedUpdate()
    {
        if (!dead && HuntPlayer && rotateTowardsPlayer && !playerMove.Dead)
        {
            Vector2 dirNormalized = (player.position - this.transform.position).normalized;
            float angle = Mathf.Atan2(dirNormalized.y, dirNormalized.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            //if (moving)
            //{
            //rb.MovePosition(rb.position + dirNormalized * moveSpeed * Time.fixedDeltaTime);
            //}
            //if (Vector2.Distance(this.transform.position, player.position) < 4f && moving == true)
            //{
            //    moving = false;
            //    //ShootPlayer();
            //}
            //else
            //{
            //    moving = true;
            //}
            //if (Vector2.Distance(this.transform.position, player.position) < hitDistance)
            //{
            //    //anim.SetBool("Hitting", true);
            //    //anim.Play("haulikkoDudeShoot");
            //}
        }
        if (playerMove.Dead && walkAway && !dead)
        {
            Vector2 dirNormalized = (this.transform.position - player.position).normalized;
            rb.MovePosition(rb.position + dirNormalized * moveSpeed * Time.fixedDeltaTime);
            moveSpeed = 0.5f;
            float angle = Mathf.Atan2(dirNormalized.y, dirNormalized.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
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
            CancelInvoke();
            playerMove.EnableDash();
            GameManager.Instance.CollisionImpulseEffect();
            anim.Play("haulikkoDudeDead");
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
            anim.Play("haulikkoDudeAim");
            sounds.PlayAttackClip();
            // InvokeRepeating("ShootInvoke", fireRate, fireRate);
            // StartCoroutine("ShootingTiming");
            Invoke("ShootingTiming", fireRate);
        }

    }
    private void ShootingTiming()
    {
        //while (1 == 1)
        //{

        Shoot();

        //    yield return null;
        //}

    }
    private void ShootInvoke()
    {
        anim.Play("haulikkoDudeShoot");
    }
    public void StopShooting()
    {
        StopAllCoroutines();
        anim.Play("haulikkoDudeIdle");

    }
    public void Shoot()
    {
        Vector3 rayDir = player.position - this.transform.position;
        // Debug.DrawRay(this.transform.position, rayDir * hitDistance);

        //  Vector3 rayDir = playerTransform.position - this.transform.position;
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, rayDir, hitDistance, ~IgnoreMe);
        if (hit)
        {
            //Debug.Log("hit "+ hit.collider.gameObject.name);
            if (hit.collider.gameObject.tag.Equals("Player"))
            {
                ShootBehaviour();
                //  Debug.Log("hit plaay");
            }

        }
    }
    private void ShootBehaviour()
    {
        sounds.PlayGunClip();
        playerMove.PlayerGotShot(this.transform.position);
        rotateTowardsPlayer = false;
        Invoke("StartRotateTowardsPlayer", stillTimeAfterShot);

    }
    private void StartRotateTowardsPlayer()
    {
        rotateTowardsPlayer = true;

        Invoke("StartShooting", fireRate);
    }
    //private void ShootingAgain()
    //{

    //    //yield return new WaitForSeconds(fireRate);
    //    anim.Play("haulikkoDudeShoot");
    //}
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
        walkAway = true;
        anim.Play("haulikkoDudeWalk");

    }
}
