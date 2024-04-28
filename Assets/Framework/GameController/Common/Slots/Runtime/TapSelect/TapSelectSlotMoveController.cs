using System;
using System.Collections.Generic;
using LTA.Base;
using LTA.Effect;
using UnityEngine;

public class TapSelectSlotMoveController : TapSelectSlotController
{
   // public static bool isAllowTapSelect = true;

    protected override void OnTapSelect()
    {
        //if (!isAllowTapSelect) return;
        //isAllowTapSelect = false;
        if (tapSelects.Count > 0)
        {
            if (IsCanMove)
            {
                Transform[] currentTapSelects = tapSelects.ToArray();
                ClearListSelects();
                List<ISlot> slots = GetFreeSlots();
                int done = slots.Count;
                if (done > currentTapSelects.Length) done = currentTapSelects.Length;
                for (int i = 0; i< slots.Count;i++)
                {
                    ISlot slot = slots[i];
                    if (i >= currentTapSelects.Length) break;
                    TapToMoveController tapToMove = currentTapSelects[i].GetComponent<TapToMoveController>();
                    OnStartMove(tapToMove,slot, () =>
                    {
                        done--;
                        if (done == 0)
                        {
                            OnEndMove();
                            //isAllowTapSelect = true;
                        }
                    });
                }
                return;
            }

            bool isPastSlot = IsPastSlot;
            ClearListSelects();
            //isAllowTapSelect = true;
            if (isPastSlot)
                return;
        }
        base.OnTapSelect();
        //isAllowTapSelect = true;
    }

    protected virtual void OnStartMove(TapToMoveController tapToMove,ISlot slot,Action EndMove)
    {
        tapToMove.MoveToSlot(slot, EndMove);
    }
    
    protected virtual void OnEndMove()
    {
        IEndMoveToSlot[] endMoveToSlots = GetComponents<IEndMoveToSlot>();
        foreach(IEndMoveToSlot endMoveToSlot in endMoveToSlots)
        {
            endMoveToSlot.OnEndMoveToSlot();
        }
        
    }
    protected bool IsPastSlot
    {
        get
        {
            ISlot pastSlot = tapSelects[0].GetComponent<TapToMoveController>().Own_Slot;
            if (pastSlot == null) return true;
            if (slots.Contains(pastSlot)) return true;
            return false;
        }
    }

    protected virtual bool IsCanMove
    {
        get
        {
            List<ISlot> slots = GetFreeSlots();
            return !IsPastSlot && slots.Count > 0;
        }
    }
}
