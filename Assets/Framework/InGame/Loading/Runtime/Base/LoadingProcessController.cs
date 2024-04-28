using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using LTA.Base;
using System;
using TMPro;

    [DisallowMultipleComponent]
    public abstract class LoadingProcessController : ProcessController
    {
        

        public Action EndLoading = null;

        public float CurrentPercent
        {
            set
            {
                CurrentValue = value;
                OnUpdate(currentValue);
            }
            get
            {
                return currentValue;
            }
        }

        public override float MaxValue 
        {
            set
            {
                base.MaxValue = value;
            }
        }

        protected override void OnUpdate(float value)
        {
            DisplayLoading(value);
            
            if (value == maxValue)
            {
                if (EndLoading != null)
                {
                    EndLoading();
                    EndLoading = null;
                }
                gameObject.SetActive(false);
            }
        }

        protected abstract void DisplayLoading(float value);


        protected virtual void Awake()
        {
            //maxValue = rect.sizeDelta.x;
            CurrentPercent = minValue;
        }

        public void Reset()
        {
            this.gameObject.SetActive(true);
            CurrentPercent = minValue;
        }

        public void SetPercent(float percent)
        {
            UpdateValue(percent * maxValue);
        }

        private void OnDisable()
        {
            CurrentPercent = minValue;
        }
    }
