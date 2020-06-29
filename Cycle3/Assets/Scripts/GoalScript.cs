using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    [SerializeField]
    private Animator freedomVolumeAnimator;
    [SerializeField]
    private GameObject freedomVolumeGO;
    [SerializeField]
    private GameObject CanvasGO;
    [SerializeField]
    private GameObject BlockColGo;
 
    private WacoomInputTopDown playerMove;
    // Start is called before the first frame update
    void Start()
    {
        playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<WacoomInputTopDown>();

        GameManager.Instance.GoalScript = this;
        CanvasGO.SetActive(false);
    }
    public void OpenTheGoal()
    {
        BlockColGo.SetActive(false);
        CanvasGO.SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
            freedomVolumeGO.SetActive(true);
            GameManager.Instance.LevelCleared();
            playerMove.Muuu();
           // freedomVolumeAnimator.Play("freedomVolume");
        }
    }
}
