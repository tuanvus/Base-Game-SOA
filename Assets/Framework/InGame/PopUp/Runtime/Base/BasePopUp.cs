using UnityEngine;
using UnityEngine.EventSystems;
using System;
using LTA.UI;
using LTA.Effect;
using System.Collections;


	public class BasePopUp : MonoBehaviour,IPointerClickHandler {
		
	[SerializeField]	
	protected ButtonController BtnExit;

	IEffect[] effects;

    protected virtual void Awake()
	{
			if (BtnExit != null)
            {
				typeClosePopUp = TypeClosePopUp.ClickButtonToClose;
                BtnExit.OnClick((ButtonController Btn) =>
                {
                    ClosePopUp();
                });
            }
			else if (typeClosePopUp != TypeClosePopUp.None)
            {
	            typeClosePopUp = TypeClosePopUp.ClickUpToClose;
            }
		
            effects = GetComponents<IEffect>();
			foreach (IEffect effect in effects)
			{
				effect.Hide();
			}
		}

	public void Show(Action endShow = null)
        {
			Debug.Log(effects.Length);
			if (effects != null)
			{
				StartCoroutine(IeShowEffects(() =>
				{
					currentTypeClosePopUp = typeClosePopUp;
					if (endShow != null)
						endShow();
				}));
				return;
			}
			currentTypeClosePopUp = typeClosePopUp;
			if (endShow != null)
				endShow();
		}

	IEnumerator IeShowEffects(Action endShowEffect)
    {
		int countShow = 0;
		foreach (IEffect effect in effects)
        {
			effect.ShowEffect(()=>
			{
				countShow++;
			});
        }
		yield return new WaitUntil(()=> countShow >= effects.Length);
		if (endShowEffect != null)
            {
				endShowEffect();
            }
	}

	IEnumerator IeHideEffects(Action endHideEffect)
	{
		int countShow = 0;
		foreach (IEffect effect in effects)
		{
				effect.HideEffect(() =>
				{
					countShow++;
				});
		}
		yield return new WaitUntil(() => countShow >= effects.Length);
		if (endHideEffect != null)
		{
			endHideEffect();
		}
	}

	public void OnPointerClick (PointerEventData eventData)
	{
		if (currentTypeClosePopUp == TypeClosePopUp.ClickUpToClose) {
			ClosePopUp();
		}
	}

	public enum TypeClosePopUp
	{
		ClickUpToClose,
		ClickButtonToClose,
		None

	}

	protected Action _callbackClosePopUp;
	protected Action _callbackBeforeClosePopUp;
	public void SetClosePopUp(Action callbackClosePopUp)
    {
		_callbackClosePopUp = callbackClosePopUp;
    }

		public void SetBeforeClosePopUp(Action callbackBeforeClosePopUp)
		{
			_callbackBeforeClosePopUp = callbackBeforeClosePopUp;
		}
		[SerializeField]
	protected TypeClosePopUp typeClosePopUp = TypeClosePopUp.ClickUpToClose;
	protected TypeClosePopUp currentTypeClosePopUp = TypeClosePopUp.None;

	public virtual void ClosePopUp(Action endClosePopUp = null)
		{
			if (_callbackBeforeClosePopUp != null)
            {
				_callbackBeforeClosePopUp();
            }
			if (effects != null)
			{
				StartCoroutine(IeHideEffects(() =>
				{
					if (_callbackClosePopUp != null)
						_callbackClosePopUp();
					if (endClosePopUp != null)
						endClosePopUp();
					PopUp.Instance.ClosePopUp(this);
                }));
				return;
			}
			if (_callbackClosePopUp != null)
				_callbackClosePopUp();
			if (endClosePopUp != null)
				endClosePopUp();
			PopUp.Instance.ClosePopUp(this);
		}
	}

