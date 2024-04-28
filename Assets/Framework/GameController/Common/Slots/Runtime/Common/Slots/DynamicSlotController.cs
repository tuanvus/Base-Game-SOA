
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicSlot : Slot
{
    public Transform transform;

    public DynamicSlot(Transform transform)
    {
        this.transform = transform;
    }

    public override Vector3 Pos => this.transform.position;

    public override void ReturnSlot()
    {
        element.localPosition = Vector3.zero;
    }

    protected override void DisPlaySlot(Transform own)
    {
        own.transform.SetParent(this.transform);
        own.localPosition = Vector3.zero;
    }
}

public class DynamicSlotController : BaseDynamicSlotController
{
    
    protected override ISlot CreateSlot()
    {
        return new DynamicSlot(transform);
    }

    
}
