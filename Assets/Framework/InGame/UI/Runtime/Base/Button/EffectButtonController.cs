using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTA.Effect;
using UnityEngine.EventSystems;
using UnityEditor;
using System.Reflection;
using System;

namespace LTA.UI
{
	[DisallowMultipleComponent]
	public abstract class EffectButtonController : EffectController, IPointerUpHandler, IPointerDownHandler
	{

        public virtual void OnPointerUp(PointerEventData eventData)
		{
			HideEffect(TypeEffect.Tap);
		}

		public virtual void OnPointerDown(PointerEventData eventData)
		{
			ShowEffect(TypeEffect.Tap);
		}
    }
}
