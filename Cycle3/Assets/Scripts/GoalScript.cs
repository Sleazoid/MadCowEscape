using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    [SerializeField]
    private Animator freedomVolumeAnimator;

   
    private WacoomInputTopDown playerMove;
    // Start is called before the first frame update
    void Start()
    {
        playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<WacoomInputTopDown>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
            freedomVolumeAnimator.transform.parent.gameObject.SetActive(true);
            GameManager.Instance.LevelCleared();
            playerMove.Muuu();
           // freedomVolumeAnimator.Play("freedomVolume");
        }
    }
}
