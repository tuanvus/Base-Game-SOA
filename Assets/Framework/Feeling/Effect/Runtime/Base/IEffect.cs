using System;
using UnityEngine;
namespace LTA.Effect
{
    public interface IEffect
    {
        TypeEffect TypeEffect { get; }
        float GetTimeDelayed();
        void ShowEffect(Action endEffect);
        void HideEffect(Action endEffect);
        void ShowEffect();
        void HideEffect();

        void Hide();
    }
}
