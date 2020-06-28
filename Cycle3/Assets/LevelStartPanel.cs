using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStartPanel : MonoBehaviour
{
    [SerializeField]
    private float dissapearTime = 2f;
    // Start is called before the first frame update
  
    private void OnEnable()
    {
        Invoke("DisableThisGameObject", dissapearTime);
    }
    private void OnDisable()
    {
        CancelInvoke();
    }
    private void DisableThisGameObject()
    {
        this.gameObject.SetActive(false);
    }
  

}
