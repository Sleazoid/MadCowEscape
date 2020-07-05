using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SliderValueChangeScript : MonoBehaviour
{

    private Slider thisSlider;
    // Start is called before the first frame update
    void Start()
    {
        thisSlider = GetComponent<Slider>();
    }

    
    public void OnValueChange()
    {
        GameManager.Instance.SetInputDevice((int)thisSlider.value);
    }
}
