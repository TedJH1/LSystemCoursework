using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderText : MonoBehaviour
{
    public Text angleText;
    public float defaultVal;

    // Start is called before the first frame update
    void Start()
    {
        HandleValueChanged(defaultVal);
        GetComponentInParent<Slider>().onValueChanged.AddListener(HandleValueChanged);
    }

    // If the slider value changes, the text updates to reflect this
    private void HandleValueChanged(float value)
    {
        angleText.text = value.ToString("#0.00");
    }
}
