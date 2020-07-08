using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Windows.Speech;

public class ScrollingTextUI : MonoBehaviour
{
    private RectTransform thisRectTrans;
    private Vector2 startPos;
    private TextMeshProUGUI textMeshText;
    private float minX;
    private int textScrollLength;
    private int charachters;
    [SerializeField]
    private float scrollTime = 5f;
    // Start is called before the first frame update
    void Awake()
    {
        thisRectTrans = GetComponent<RectTransform>();
        startPos = thisRectTrans.anchoredPosition;
        textMeshText = GetComponent<TextMeshProUGUI>();
        //Debug.Log(textMeshText.fontSize);       
    }
    private void OnEnable()
    {
        CheckTextLength();
    }
    public void CheckTextLength()
    {
        //thisRectTrans = GetComponent<RectTransform>();
        //startPos = thisRectTrans.anchoredPosition;
        //textMeshText = GetComponent<TextMeshProUGUI>();
        charachters = textMeshText.text.Length;
        int singleCharSize = 10;
        textScrollLength = charachters * singleCharSize;
       // Debug.Log(textMeshText.text);
        minX = startPos.x - textScrollLength - 200;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("NOG"+textMeshText.text);
        if(charachters!= textMeshText.text.Length)
        {
            CheckTextLength();
        }
        thisRectTrans.anchoredPosition = new Vector2(thisRectTrans.anchoredPosition.x - (Time.deltaTime * scrollTime), thisRectTrans.anchoredPosition.y);

        if (thisRectTrans.anchoredPosition.x < minX)
        {
            thisRectTrans.anchoredPosition = startPos;
        }

    }
}
