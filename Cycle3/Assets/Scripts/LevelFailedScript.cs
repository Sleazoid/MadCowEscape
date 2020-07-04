using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFailedScript : MonoBehaviour
{
    InputActions inputActions;
    private void Awake()
    {
        inputActions = new InputActions();
        inputActions.Gamepad.X.performed += ctx => Continue();

    }
    public void Continue()
    {
        GameManager.Instance.LoadNewLevel(false);
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();
       
    }
}
