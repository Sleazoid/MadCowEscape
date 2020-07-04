using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class EnemyGranadeNoticeAreaScript : MonoBehaviour
{
    private bool playerWasOnArea = false;
    private EnemyGrenadeMove enemyMove;
    private Transform playerTransform;
    public LayerMask IgnoreMe;
    private Animator anim;
    private CharacterSounds sounds;
    private float noticeDistance = 6f;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        enemyMove = this.gameObject.transform.parent.GetComponent<EnemyGrenadeMove>();
        noticeDistance = enemyMove.HitDistance;
        anim = enemyMove.gameObject.GetComponent<Animator>();
        sounds = enemyMove.gameObject.GetComponent<CharacterSounds>();
       
    }
    private void OnDrawGizmos()
    {
        //   Gizmos.DrawSphere(this.transform.position, 1f);

    }
    // Update is called once per frame
    void Update()
    {
        //if (playerWasOnArea)
        {
            Vector3 rayDir = playerTransform.position - this.transform.position;
            Debug.DrawRay(this.transform.position, rayDir * noticeDistance);
           
            //  Vector3 rayDir = playerTransform.position - this.transform.position;
            RaycastHit2D hit = Physics2D.Raycast(this.transform.position, rayDir, noticeDistance, ~IgnoreMe);
            if (hit)
            {
                if (hit.collider.gameObject.tag.Equals("Player") && enemyMove.HuntPlayer == false)
                {
                    enemyMove.StartShooting();
                    enemyMove.HuntPlayer = true;
                    //sounds.PlayNoticedClip();
                    //   Debug.Log("PLAYER NOTICEd");
                }

                //if (hit.collider != null)
                //{
                //    Debug.Log(hit.collider.name);
                //}
            }
            else
            {
                //enemyMove.StopShooting();
                enemyMove.HuntPlayer = false;
            }
        }


    }
 
}
