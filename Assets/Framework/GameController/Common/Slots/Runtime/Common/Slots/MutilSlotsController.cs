using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutilSlots : ISlot
{
    protected List<ISlot> slots =  new List<ISlot>();
    bool isMoving = false;

    Transform transform;

    public MutilSlots(Transform transform)
    {
        this.transform = transform;
    }

    public void AddSlot(ISlot slot)
    {
        if (slots.Contains(slot)) return;
        slots.Add(slot);
    }

    public Vector3 Pos { get => transform.position; }

    public bool IsMoving { get => isMoving; set => isMoving = value; }

    public bool IsFree
    {
        get
        {
            foreach (ISlot slot in slots)
            {
                if (slot.IsFree) return true;
            }
            return false;
        }
    }

    public void Add(Transform target)
    {
        if (!IsFree) return;
        if (target == null) return;
        foreach (ISlot slot in slots)
        {
            if (slot.IsFree)
            {
                slot.Add(target);
            }
        }
    }

    public bool Contains(Transform target)
    {
        if (target == null) return false;
        foreach (ISlot slot in slots)
        {
            if (slot.Contains(target)) ;
            {
                return true;
            }
        }
        return false;
    }

    public void Remove(Transform target)
    {
        if (target == null) return;
        foreach (ISlot slot in slots)
        {
            if (slot.Contains(target)) ;
            {
                slot.Remove(target);
                return;
            }
        }
    }

    public void ReturnSlot()
    {
        foreach (ISlot slot in slots)
        {
            slot.ReturnSlot();
        }
    }

}

public class MutilSlotsController : BaseDynamicSlotController,IAddChildSlot
{
    public void OnAddChildSlot(ISlot slot)
    {
        ((MutilSlots)this.slot).AddSlot(slot);
    }

    protected override ISlot CreateSlot()
    { 
        return new MutilSlots(transform);
    }
}
