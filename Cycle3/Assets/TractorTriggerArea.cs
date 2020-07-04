using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TractorTriggerArea : MonoBehaviour
{
    private Animator anim;
    [SerializeField]
    private AudioClip startingSound;
    [SerializeField]
    private AudioClip drivingSound;
    private AudioSource audioSource;
    private TractorEnemy tractorScript;
    private BoxCollider2D col;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<BoxCollider2D>();
       // anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        tractorScript = transform.parent.gameObject.GetComponent<TractorEnemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
           
            StartEngine();
            col.enabled = false;
        }
    }
    private void StartEngine()
    {
        audioSource.clip = startingSound;
        audioSource.Play();
        Invoke("SetToDriving", 1.9f);
    }

    private void SetToDriving()
    {
        //Debug.Log(tractorScript);
        tractorScript.StartDriving();
        audioSource.clip = drivingSound;
        audioSource.Play();
    }
    
}
