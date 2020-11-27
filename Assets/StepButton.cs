using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StepButton : MonoBehaviour, IPointerClickHandler
{
    public Text buttonText;
    public int currentStep;

    void Start()
    {
        currentStep = 0;
    }

    // Decrease the step by 1 on right click
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
            StepDown();
    }

    // Increase the step by 1 and update the text on the button
    public void StepUp()
    {
        currentStep++;
        buttonText.text = currentStep.ToString();
    }

    // Decrease the step by 1 and update the text on the button
    public void StepDown()
    {
        currentStep--;
        buttonText.text = currentStep.ToString();
    }

    // Reset the value of the step to 0
    public void ResetValue()
    {
        currentStep = 0;
    }
}
