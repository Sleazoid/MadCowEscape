using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFailedScript : MonoBehaviour
{
   

    public void Continue()
    {
        GameManager.Instance.LoadNewLevel(false);
    }
}
