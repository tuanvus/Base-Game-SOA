using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

    [DisallowMultipleComponent]
    public class FillImageProcessController : LoadingProcessController
    {
        //[SerializeField]
        //RectTransform rect;

        [SerializeField]
        Image imgLoading;

        [SerializeField]
        TextMeshProUGUI txtProcessing;

        protected override void DisplayLoading(float value)
        {
            if (txtProcessing != null)
                txtProcessing.text = "Loading : " + (int)(((float)value / maxValue) * 100) + "%";
            if (imgLoading != null)
                imgLoading.fillAmount = (float)value / maxValue;
        }

        protected override void Awake()
        {
            maxValue = 1;
            minValue = 0;
            base.Awake();
        }
    }
