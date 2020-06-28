using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemyMove : MonoBehaviour
{
    private Transform player;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float timeToShoot;
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
    private float impactForce= 2;
    [SerializeField]
    private GameObject bloodParticles;
    private ParticleCollision particleCol;
    private EnemyNoticeAreaScript noticeArea;
    private CharacterSounds sounds;
    private bool huntPlayer = false;
    [SerializeField]
    private float hitDistance = 0.4f;
    private bool walkAway = false;

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
        noticeArea = Transform.FindObjectOfType<EnemyNoticeAreaScript>();
       
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
    void Update()
    {

    }
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
    private void ShootBullet()
    {
        Vector2 dirNormalized = (player.position - this.transform.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().AddForce(dirNormalized * shootForce, ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        if(!dead && HuntPlayer && !playerMove.Dead)
        {
            Vector2 dirNormalized = (player.position - this.transform.position).normalized;
            float angle = Mathf.Atan2(dirNormalized.y, dirNormalized.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            //if (moving)
            //{
                rb.MovePosition(rb.position + dirNormalized * moveSpeed * Time.fixedDeltaTime);
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
            if (Vector2.Distance(this.transform.position, player.position) < hitDistance)
            {
                anim.SetBool("Hitting", true);
           
            }
        }
        if(playerMove.Dead && walkAway)
        {
            Vector2 dirNormalized = ( this.transform.position - player.position ).normalized;
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
        if(isOnPlayerRange && !dead)
        {
            GameManager.Instance.CollisionImpulseEffect();
            anim.Play("enemyDead");
            noticeArea.gameObject.SetActive(false);
            sounds.PlayDeathClip();
            int randomZRot = Random.Range(0, 360);
            particleCol.ParticlesOn();
            bloodParticles.transform.eulerAngles = new Vector3(0, 0, randomZRot);
            bloodParticles.GetComponent<Animator>().SetTrigger("Start");
            bloodParticles.GetComponent<ParticleSystem>().Play();
            //anim.SetBool("Dead", true);
            dead = true;
            triggerCol.enabled = false;
            Vector2 impactDir = this.transform.position - player.position;
            rb.AddForce(impactDir.normalized * impactForce, ForceMode2D.Impulse);
            StartCoroutine(StopBlood());
            WacoomInputTopDown.AttackEvent -= EnemyIsDead;
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
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    playerMove.GetDamage();
    //}
}
