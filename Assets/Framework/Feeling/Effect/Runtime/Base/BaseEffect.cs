using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LTA.Effect
{
    public abstract class BaseEffect: MonoBehaviour,IEffect
    {
        [SerializeField]
        TypeEffect typeEffect;
        [SerializeField]
        float delayTime = 0f;
        public TypeEffect TypeEffect {
            get
            {
                return typeEffect;
            }
        }

        public abstract void Hide();

        public abstract void HideEffect(Action endEffect);

        public abstract void HideEffect();

        public float GetTimeDelayed()
        {
            return delayTime;
        }

        public abstract void ShowEffect(Action endEffect);

        public abstract void ShowEffect();
    }
}
