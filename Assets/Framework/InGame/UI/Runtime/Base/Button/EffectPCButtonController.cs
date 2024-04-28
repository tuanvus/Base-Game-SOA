using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace LTA.UI
{
    public class EffectPCButtonController : EffectButtonController, IPointerEnterHandler, IPointerExitHandler
    {
        bool isProcessing = false;
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (isProcessing) return;
                isProcessing = true;
            ShowEffect(TypeEffect.Select,()=>
            {
                isProcessing = false;
            });
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            HideEffect(TypeEffect.Select,()=>
            {
                isProcessing = false;
            });
        }
    }
}
