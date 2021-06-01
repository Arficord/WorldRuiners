using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public TextMeshProUGUI valueTextField;
    public Image progressImage;

    public void UpdateView(float currentValue, float maxValue)
    {
        if (maxValue == 0)
        {
            throw new DivideByZeroException("Progress bar maxValue cannot be zero!");
        }
        
        float progress = currentValue / maxValue;
        progressImage.fillAmount = progress;
        if (valueTextField != null)
        {
            valueTextField.text = currentValue + "/" + maxValue;
        }
    }
}
