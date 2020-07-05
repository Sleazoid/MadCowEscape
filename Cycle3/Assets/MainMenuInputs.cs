using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuInputs : MonoBehaviour
{
    //InputActions inputActions;
    [SerializeField]
    private GameObject settingPanelGo;
    [SerializeField]
    private List<GameObject> mainMenuGos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenSettingsPanel()
    {
        for (int i = 0; i < mainMenuGos.Count; i++)
        {
            mainMenuGos[i].SetActive(false);
        }
        settingPanelGo.SetActive(true);
    }
    public void CloseSettingsPanel()
    {
        for (int i = 0; i < mainMenuGos.Count; i++)
        {
            mainMenuGos[i].SetActive(true);
        }
        settingPanelGo.SetActive(false);
    }
}
