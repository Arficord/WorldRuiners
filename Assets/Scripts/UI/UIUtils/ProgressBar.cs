using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace My.UI.Utils
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI valueTextField;
        [SerializeField] private Image progressImage;

        public void UpdateView(float currentValue, float maxValue)
        {
            if (valueTextField != null)
            {
                valueTextField.text = currentValue + "/" + maxValue;
            }
            
            if (maxValue == 0)
            {
                progressImage.fillAmount = 0;
                return;
            }

            float progress = currentValue / maxValue;
            progressImage.fillAmount = progress;
        }
    }
}