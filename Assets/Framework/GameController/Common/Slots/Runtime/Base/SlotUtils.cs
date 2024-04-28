using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotUtils
{
    public static ISlot GetNearSlot(Transform checkedObject, List<ISlot> checkSlots, float allowDistance = 1f)
    {
        ISlot result = null;
        float minDistance = 10000;
        foreach (ISlot slot in checkSlots)
        {
            if (slot.IsMoving) continue;
            float distance = Vector3.Distance(slot.Pos, checkedObject.transform.position);
            if ((distance <= minDistance || result == null) && distance <= allowDistance)
            {
                result = slot;
                minDistance = distance;
            }
        }
        return result;
    }

    public static ISlot GetFreeSlot(Transform checkedObject, List<ISlot> checkSlots, float allowDistance = 1f)
    {
        ISlot result = null;
        float minDistance = 10000;
        foreach (ISlot slot in checkSlots)
        {
            if (!slot.IsFree) continue;
            float distance = Vector3.Distance(slot.Pos, checkedObject.transform.position);
            if ((distance <= minDistance || result == null) && distance <= allowDistance)
            {
                result = slot;
                minDistance = distance;
            }
        }
        return result;
    }


    public static List<ISlot> GetFreeSlots(List<ISlot> checkSlots)
    {
        List<ISlot> results = new List<ISlot>();
        foreach (ISlot slot in checkSlots)
        {
            if (slot.IsFree&&!slot.IsMoving)
                results.Add(slot);
        }
        return results;
    }

    public static List<ISlot> GetFullSlots(List<ISlot> checkSlots)
    {
        List<ISlot> results = new List<ISlot>();
        foreach (ISlot slot in checkSlots)
        {
            if (!slot.IsFree)
                results.Add(slot);
        }
        return results;
    }

    public static List<ISlot> GetRandomSlots(List<ISlot> checkSlots)
    {
        for (int i = 0; i < checkSlots.Count; i++)
        {
            ISlot slotCurrent = checkSlots[i];
            int randomIndex = Random.Range(i, checkSlots.Count);
            checkSlots[i] = checkSlots[randomIndex];
            checkSlots[randomIndex] = slotCurrent;
        }
        return checkSlots;
    }
    
    
    
}
