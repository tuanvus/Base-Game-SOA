using System.Collections;
using System.Collections.Generic;
using LTA.DesignPattern;
using UnityEngine;

public class ChangeSlotDragMoveController : SlotDragMoveController
{

    public override void OnDragUp()
    {
        if (checkSlot != null && checkSlot.IsFree)
        {
            ISlot pastSlot = Own_Slot;
            if (Own_Slot != null)
            {
                Own_Slot.IsMoving = false;
                Own_Slot.Remove(transform);
            }
            checkSlot.Add(transform);
            Own_Slot = checkSlot;
            IEndChangeSlot[] endChangeSlots = GetComponentsInChildren<IEndChangeSlot>();
            foreach(IEndChangeSlot endChangeSlot in endChangeSlots)
            {
                endChangeSlot.OnEndChangeSlot(Own_Slot, pastSlot);
            }
        }
        base.OnDragUp();
    }


}
