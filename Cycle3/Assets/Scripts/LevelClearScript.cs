using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LevelClearScript : MonoBehaviour
{
    InputActions inputActions;
    [SerializeField]
    private TextMeshProUGUI clearedText;
    private int currentLevel = 0;
    [SerializeField]
    private GameObject levelClearVolumeGO;
    public int CurrentLevel { get => currentLevel; set => currentLevel = value; }
    private void Awake()
    {
        inputActions = new InputActions();
        inputActions.Gamepad.X.performed += ctx => Continue();

    }
    // Start is called before the first frame update
    private void OnEnable()
    {
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();

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
    public void EnableVolume()
    {
        levelClearVolumeGO.SetActive(true);
    }
    public void DisableVolume()
    {
        levelClearVolumeGO.SetActive(false);
    }

}
