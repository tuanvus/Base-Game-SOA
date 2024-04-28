using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTA.Effect;
using UnityEngine.EventSystems;

namespace LTA.UI
{
    [DisallowMultipleComponent]
    public class TabController : ButtonController
    {

        [SerializeField]
        protected bool isClicked = false;
        
        private void Start()
        {
            IsClicked = isClicked;
        }

        void EnableEffect(bool enable)
        {
            IEffect[] effects = Effects;
            foreach (IEffect effect in effects)
            {
                ((Behaviour)effect).enabled = enable;
            }
        }

        public virtual bool IsClicked
        {
            set
            {
                isClicked = value;
                if (isClicked)
                {
                    ShowEffect(TypeEffect.Select);
                }
                else
                {
                    HideEffect(TypeEffect.Select);
                }
                EnableEffect(!isClicked);
            }
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            if (isClicked) return;
            base.OnPointerClick(eventData);
        }

    }
}

