using System;
using UnityEngine.EventSystems;
using LTA.Effect;
namespace LTA.UI
{
    public class ButtonOnOffController : EffectController, IPointerClickHandler
    {
        bool isOn = true;

        protected Action<bool> callBackClick;

        public bool IsOn
        {
            set
            {
                isOn = value;
                UpdateStatus();
            }
        }

        void UpdateStatus()
        {
            if (isOn)
            {
                ShowEffect(TypeEffect.Click);
            }
            else
            {
                HideEffect(TypeEffect.Click);
            }
        }

        public void OnClick(Action<bool> _callBackClick)
        {
            if (_callBackClick != null)
                callBackClick = _callBackClick;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            isOn = !isOn;
            UpdateStatus();
            if (callBackClick != null)
            {
                callBackClick(isOn);
            }
        }
    }
}
