using LTA.Effect;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseButtonController : EffectController,IPointerClickHandler
{
	public virtual void OnPointerClick(PointerEventData eventData)
	{
		ShowEffect(TypeEffect.Click,()=> {
			OnClick();
			HideEffect(TypeEffect.Click);
		});
	}

	protected abstract void OnClick();
}
