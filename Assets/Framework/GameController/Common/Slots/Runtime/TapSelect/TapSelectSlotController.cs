using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class TapSelectSlotController : TapSelectController,IAddSlot,IAddChildSlot,IOnDestroyDynamicSlot
{
    protected List<ISlot> slots = new List<ISlot>();

    public void AddSlot(ISlot slot)
    {
        if (slot is Slot)
            slots.Add((Slot)slot);
    }

    public void OnAddChildSlot(ISlot slot)
    {
        if (slot is Slot)
            slots.Add(slot);
    }

    protected override void OnTapSelect()
    {
        SelectElementsInSlots();
    }

    protected virtual void SelectElementsInSlots()
    {
        Debug.Log("SelectElementsInSlots 0");
        foreach (Slot slot in slots)
        {
            if (slot.IsFree) continue;
            if (CheckToSelect(slot.Element))
            {
                Select(slot.Element);
            }
        }
        Debug.Log("SelectElementsInSlots 1");
    }

    public List<ISlot> GetFreeSlots()
    {
        return SlotUtils.GetFreeSlots(slots);
    }

    public List<ISlot> GetFullSlots()
    {
        return SlotUtils.GetFullSlots(slots);
    }

    protected virtual bool CheckToSelect(Transform transform)
    {
        return true;
    }

    public void OnDestroyDynamicSlot(ISlot slot)
    {
        if (slots.Contains(slot))
        {
            slots.Remove(slot);
        }
    }
}
