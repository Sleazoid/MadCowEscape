using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuInputs : MonoBehaviour
{
    //InputActions inputActions;
    [SerializeField]
    private GameObject settingPanelGo;
    [SerializeField]
    private GameObject settingPanelFirstButton;
    [SerializeField]
    private GameObject settingCloseButton;
    [SerializeField]
    private GameObject mainMenuPanelFirstButton;
    [SerializeField]
    private List<GameObject> mainMenuGos;
    // Start is called before the first frame update
    void Start()
    {
        
    }



    public void OpenSettingsPanel()
    {
        for (int i = 0; i < mainMenuGos.Count; i++)
        {
            mainMenuGos[i].SetActive(false);
        }
        EventSystem.current.SetSelectedGameObject(null);
        settingPanelGo.SetActive(true);
        EventSystem.current.SetSelectedGameObject(settingPanelFirstButton);
    }
    public void CloseSettingsPanel()
    {
        for (int i = 0; i < mainMenuGos.Count; i++)
        {
            mainMenuGos[i].SetActive(true);
        }
        EventSystem.current.SetSelectedGameObject(null);
        settingPanelGo.SetActive(false);
        EventSystem.current.SetSelectedGameObject(mainMenuPanelFirstButton);
    }
}
