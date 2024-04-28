using UnityEngine;
using LTA.Base;
using TMPro;
namespace LTA.UI
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextMoney : TweenBehaviour
    {
        public TextMeshProUGUI m_Text;

        long currentValue;

        public long Value
        {
            get
            {
                return currentValue;
            }
        }

        private void Awake()
        {
            m_Text = GetComponent<TextMeshProUGUI>();
        }

        public void SetMoney(long newValue)
        {
            if (newValue < 0)
                currentValue = 0;
            else
                currentValue = newValue;
            if (m_Text == null) return;
            m_Text.text = Utils.ConvertMoneyToString(currentValue);
        }

        public void ChangeMoney(long newValue)
        {
            long prevValue = currentValue;
            currentValue = newValue;
            UpdateValue(prevValue, currentValue, UpdateMoney);
        }

        public void UpdateMoney(float value)
        {
            if (m_Text == null) return;
            m_Text.text = Utils.ConvertMoneyToString((int)value);
        }
    }
}

