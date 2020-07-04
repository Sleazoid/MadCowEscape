using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehaviour : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> explosions;
    AudioSource audiosource;
    private SpriteRenderer rend;
    private CircleCollider2D col;
    // Start is called before the first frame update
    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        audiosource = GetComponent<AudioSource>();
        col = GetComponent<CircleCollider2D>();
    }
    public void DestroyThisObjects()
    {
        rend.enabled = false;
        col.enabled = false;
        Invoke("DestroyOnDelay", 1.5f);
    }
    private void DestroyOnDelay()
    {
        Destroy(this.gameObject);
    }
    private void OnDestroy()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
            GameManager.Instance.SetDamageToPlayer(this.transform.position);
        }
    }
    public void ExplosionTime()
    {
        int random = Random.Range(0, explosions.Count);
        audiosource.clip = explosions[random];
        audiosource.Play();
        GameManager.Instance.ExplosionImpulseEffect();
    }
}
