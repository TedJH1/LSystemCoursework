using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PresetOptions : MonoBehaviour
{
    public InputField axiom;
    public InputField f;
    public InputField x;
    public Text step;
    public Slider angle;

    // Uses dropdown menu to get the preset values
    public void ChangePreset(int preset)
    {
        switch (preset)
        {
            case 0:
                SetValues("F", "F[+F]F[-F]F", "X", "0", 25.7f);
                break;
            case 1:
                SetValues("F", "F[+F]F[-F][F]", "X", "0", 20f);
                break;
            case 2:
                SetValues("F", "FF-[-F+F+F]+[+F-F-F]", "X", "0", 22.5f);
                break;
            case 3:
                SetValues("X", "FF", "F[+X]F[-X]+X", "0", 20f);
                break;
            case 4:
                SetValues("X", "FF", "F[+X][-X]FX", "0", 25.7f);
                break;
            case 5:
                SetValues("X", "FF", "F-[[X]+X]+F[+FX]-X", "0", 22.5f);
                break;
            case 6:
                SetValues("F", "FF+[+F-F-F]-[-F+F+F]", "X", "0", 22.5f);
                break;
            case 7:
                SetValues("FX", "F", "[-FX]+FX", "0", 40f);
                break;
            default:
                throw new InvalidOperationException("Invalid Option Selected");
        }
    }

    // Inserts the preset values into the option boxes
    private void SetValues(string axiomVal, string fVal, string xVal, string stepVal, float angleVal)
    {
        axiom.text = axiomVal;
        f.text = fVal;
        x.text = xVal;
        step.text = stepVal;
        angle.value = angleVal;
    }
}
