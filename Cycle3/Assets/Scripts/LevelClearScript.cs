using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LevelClearScript : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI clearedText;
    private int currentLevel = 0;

    public int CurrentLevel { get => currentLevel; set => currentLevel = value; }

    // Start is called before the first frame update
    private void OnEnable()
    {
       

        //if(currentLevel)

    }

    public void Continue()
    {
        GameManager.Instance.LoadNewLevel(true);
    }
    public void SetLevelText(int curLevel)
    {
        currentLevel = curLevel;
        clearedText.text = "Level " + currentLevel.ToString() + " clear!"; 
    }
}
