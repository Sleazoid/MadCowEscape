using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TractorEnemy : MonoBehaviour
{
    private Animator anim;

    private bool driving = false;
    [SerializeField]
    private float speed = 5f;
    private Rigidbody2D rb;
    private WacoomInputTopDown playerMove;
    private Rigidbody2D playerRb;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = transform.GetComponentInChildren<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<WacoomInputTopDown>();
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }
    private void OnDrawGizmos()
    {
          Gizmos.DrawLine(this.transform.position, this.transform.right.normalized * 2);

    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        
        if(driving)
        {
            //Vector2 dirNormalized = (playerMove.transform.position-this.transform.position ).normalized;
            //Vector3 cross = Vector3.Cross(this.transform.right, Vector3.up);
            Vector2 dir = new Vector2(transform.up.x, transform.up.y);
            rb.MovePosition(rb.position + dir * speed * Time.fixedDeltaTime);
            //transform.position += transform.up * Time.deltaTime;
            //Vector2 dir = transform.forward.normalized;
            //rb.MovePosition(rb.position + dir * speed * Time.fixedDeltaTime);
            //  Debug.Log(rb.velocity);
            ////  Vector2 dirNormalized = (player.position - this.transform.position).normalized;
            //  float angle = Mathf.Atan2(dirNormalized.y, dirNormalized.x) * Mathf.Rad2Deg;
            //  transform.rotation = Quaternion.AngleAxis(angle, Vector3.right);
        }
    }
    public void StartDriving()
    {
        anim.SetBool("Driving", true);
        driving = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(driving && collision.gameObject.tag.Equals("Player"))
        {
            playerMove.PlayerGotDriveOver(this.transform.position);
            playerRb.simulated = false;
      
            anim.SetBool("Driving", false);
        }
    }

    private IEnumerator VolumeSlideDown()
    {
        float volume = audioSource.volume;
        while (volume > 0)
        {
            volume -= 0.01f;
            audioSource.volume = volume;
            yield return new WaitForSeconds(0.01f);
        }

        Destroy(this.gameObject);
        yield return null;
    }
    public void DestroyThisTractor()
    {
        StartCoroutine("VolumeSlideDown");
    }

    
}
