using System.Collections.Generic;
using UnityEngine;


public class SlotController : MonoBehaviour,IAddSlot,IOnDestroyDynamicSlot
{

    public static List<ISlot> slots = new List<ISlot>();

    public static ISlot GetNearSlot(Transform checkedObject,float allowDistance = 1f)
    {
        return SlotUtils.GetNearSlot(checkedObject, slots,allowDistance);
    }

    public static ISlot GetFreeSlot(Transform checkedObject, float allowDistance = 1f)
    {
        return SlotUtils.GetFreeSlot(checkedObject, slots,allowDistance);
    }

    public static List<ISlot> GetFreeSlots()
    {
        return SlotUtils.GetFreeSlots(slots);
    }


    public static List<ISlot> GetFullSlots()
    {
        return SlotUtils.GetFullSlots(slots);
    }

    public void AddSlot(ISlot slot)
    {
        slots.Add(slot);
    }

    public void OnDestroyDynamicSlot(ISlot slot)
    {
        slots.Remove(slot);
    }
}
