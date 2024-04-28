using UnityEngine;
using System;

namespace  LTA.Base
{
    public abstract class ProcessController : TweenBehaviour
    {

        protected float maxValue;

        protected float minValue = 0f;

        protected float currentValue;

        public virtual float MaxValue
            {
                set
                {
                    maxValue = value;
                    if (currentValue >= maxValue)
                        CurrentValue = maxValue;
                }
            }

        public virtual float MinValue
        {
            set
            {
                minValue = value;
                if (maxValue <= minValue)
                {
                    MaxValue = minValue * 2;
                    return;
                }
                if (currentValue > minValue)
                    CurrentValue = minValue;
            }

        }

        public virtual float CurrentValue
        {
            set
            {
                try
                {
                    currentValue = value;
                    if (currentValue <= minValue)
                    {
                        currentValue = minValue;
                    };
                    if (currentValue > maxValue) currentValue = maxValue;
                }
                catch (Exception e)
                {
                    Debug.LogError("Error SetValue Processing " + e.Message);
                }
            }
        }

        protected void UpdateValue(float value,Action OnCompleteProcessing = null){
            try
            {
                float previousValue = currentValue;
                CurrentValue = value;
                UpdateValue(previousValue, currentValue, OnUpdate, OnCompleteProcessing);
            }
            catch (Exception e)
            {
                Debug.LogError("Error EditValue Processing " + e.Message);
            }
        }

        protected abstract void OnUpdate(float value);
    }
}
