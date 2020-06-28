using System.Collections;
using System.Collections.Generic;
using System.Drawing;

using UnityEngine;
using UnityEngine.InputSystem;

public class WacoomInput : MonoBehaviour
{
    InputActions inputActions;
    Vector2 valuePress;
    Rigidbody2D rb;
    Vector2 pressStartPos;
    Vector2 pressEndPos;
    [SerializeField]
    private float swipeForce;

    public float MaxY;
    public float minY;
    public float maxX;
    public float minX;
    private Vector2 curPenPos;
    private bool bt1Press = false;
    private float screenHeight;
    private float screenWidth;
    private Animator anim;
    private void Awake()
    {
        inputActions = new InputActions();
        inputActions.Wacom.Press.performed += ctx => WhatHappens();
        inputActions.Wacom.Press.canceled += ctx => PressCancelled();
        inputActions.Wacom.Press.started += ctx => PressStarted();
        valuePress = inputActions.Wacom.Press.ReadValue<Vector2>();
        inputActions.Wacom.oo.performed += ctx => Tip();
        inputActions.Wacom.Range.performed += ctx => InRange();
        inputActions.Wacom.btn1.performed += ctx => Btn1();
        inputActions.Wacom.btn1.canceled += ctx => Btn1Released();
        inputActions.Wacom.btn2.performed += ctx => Btn2();
        inputActions.Wacom.btn3.performed += ctx => Btn3();
        inputActions.Wacom.btn4.performed += ctx => Btn4();
        inputActions.Wacom.eraser.performed += ctx => Eraser();
        // inputActions.Wacom.Press.performed += ctx =>  valuePress = ctx.ReadValue<Vector2>();

    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        screenHeight = (Screen.height);
        screenWidth = (Screen.width);
     
    }
    public void Eraser()
    {
        Debug.Log("eraser");
    }
    public void Btn1()
    {
        Debug.Log("btn111");
       // rb.AddForce(this.transform.up * swipeForce);
        bt1Press = true;
    }
    public void Btn1Released()
    {
        Debug.Log("btn111 released");
       // rb.AddForce(this.transform.up * swipeForce);
        bt1Press = false;
    }
    public void Btn3()
    {
        Debug.Log("btn3");
    }
    public void Btn2()
    {
        Debug.Log("btn2");
    }
    public void Btn4()
    {
        Debug.Log("btn4");
    }
    public void InRange()
    {
        Debug.Log("WuuT");
     
    }
    public void Tip()
    {
        Debug.Log("WooT");
    }
    public void WhatHappens()
    {
        Debug.Log("Press performed");
    }
    public void PressCancelled()
    {
        Debug.Log("Press Cancelled");
        pressEndPos = Pen.current.position.ReadValue();
        SwipeObject();
    }
    public void PressStarted()
    {
        Debug.Log("Press Started");
        pressStartPos = Pen.current.position.ReadValue();
    }
    private void OnEnable()
    {
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (bt1Press)
        {
            rb.AddForce(this.transform.up * swipeForce);
        }
        //Debug.Log(Pen.current.device.displayName);
        //Debug.Log(Pen.current.position.ReadValue());
        //Debug.Log(Pen.current.tilt.ReadValue());
        curPenPos = Pen.current.position.ReadValue();
        if (curPenPos.x>maxX)
        {
            maxX = curPenPos.x;
        }
        if (curPenPos.x < minX)
        {
            minX = curPenPos.x;
        }
        if (curPenPos.y > MaxY)
        {
            MaxY = curPenPos.y;
        }
        if (curPenPos.y< minY)
        {
            minY = curPenPos.y;
        }
    }

    private void SwipeObject()
    {
        Vector2 dir = (pressEndPos - pressStartPos);
        Vector2 ranged = (dir / 100)*2f;
       // if(ranged>scre)
        Debug.Log(dir);
        rb.AddForce(ranged/*.normalized* swipeForce*/, ForceMode2D.Impulse);
    }
}
