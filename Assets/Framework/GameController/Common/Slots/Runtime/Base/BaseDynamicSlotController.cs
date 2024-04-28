using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseDynamicSlotController : BaseSlotController
{

    private void OnDestroy()
    {
        IOnDestroyDynamicSlot[] onDestroyDynamicSlots = GetComponentsInChildren<IOnDestroyDynamicSlot>();
        foreach (IOnDestroyDynamicSlot onDestroyDynamicSlot in onDestroyDynamicSlots)
        {
            onDestroyDynamicSlot.OnDestroyDynamicSlot(slot);
        }
    }
}
