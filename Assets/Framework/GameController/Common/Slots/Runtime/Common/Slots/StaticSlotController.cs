using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticSlot : Slot
{
    Vector3 pos;

    public StaticSlot(Vector3 pos)
    {
        this.pos = pos;
    }

    public override Vector3 Pos => pos;

    public override void ReturnSlot()
    {
        element.position = pos;
    }

    protected override void DisPlaySlot(Transform own_slot)
    {
        element.position = pos;
    }
}

public class StaticSlotController : BaseSlotController
{
    protected override void AddSlot()
    {
        base.AddSlot();
        Destroy(gameObject);
    }

    protected override ISlot CreateSlot()
    {
        return new StaticSlot(transform.position);
    }
}
