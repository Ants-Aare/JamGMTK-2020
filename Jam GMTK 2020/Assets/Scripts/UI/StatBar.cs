
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

namespace GMTKJAM.UI
{
    public class StatBar : MonoBehaviour
    {
        [SerializeField]
        private Image fillImage;

        public float maxValue = 0;

        public void UpdateFillPercent(float amount)
        {
            fillImage.fillAmount = amount;
        }

        public void UpdateFill(float amount)
        {
            fillImage.fillAmount = amount / maxValue;
        }
    }
}