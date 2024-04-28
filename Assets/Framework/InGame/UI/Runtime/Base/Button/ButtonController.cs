using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using System;
using LTA.Effect;

namespace LTA.UI
{
	[DisallowMultipleComponent]
	public class ButtonController : BaseButtonController
	{
        protected Action<ButtonController> callBackClick;

		public void OnClick(Action<ButtonController> _callBackClick)
		{
			if (_callBackClick != null)
				callBackClick = _callBackClick;
		}

        protected override void OnClick()
        {
			if (callBackClick != null)
				callBackClick(this);
		}
    }
}
