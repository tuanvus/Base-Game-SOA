using System;
using UnityEngine;
using UnityEngine.EventSystems;
using LTA.Base;
namespace LTA.UI
{
    [DisallowMultipleComponent]
    public class BtnMove : TweenBehaviour , IDragHandler, IBeginDragHandler, IEndDragHandler
    {

        //protected Action<ButtonController> callBackBeginDrag;


        //public void OnBeginDrag(Action<ButtonController> _callBackBeginDrag)
        //{
        //    if (_callBackBeginDrag != null)
        //        callBackBeginDrag = _callBackBeginDrag;
        //}
        bool isDragging = false;
        public void OnBeginDrag(PointerEventData eventData)
        {
            //if (callBackBeginDrag != null)
            //    callBackBeginDrag(this);
            isDragging = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector3 newpPos = new Vector3(
                    eventData.position.x - Screen.width/2,
                    eventData.position.y - Screen.height/2,
                    0
                );
            MoveUpdateLocal(newpPos);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            isDragging = false;
        }

        //public override void OnPointerClick(PointerEventData eventData)
        //{
        //    if (isDragging) return;
        //    base.OnPointerClick(eventData);
        //}
    }
}
