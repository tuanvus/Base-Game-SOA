using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Slot : ISlot
{
    protected Transform element;
    bool isMoving = false;

    public abstract Vector3 Pos { get; }

    public virtual Transform Element
    {
        get
        {
            return element;
        }

    }

    public bool IsMoving { get => isMoving; set => isMoving = value; }

    public bool IsFree => element == null;

    public void Add(Transform target)
    {
        if (target != null)
        {
            IMoveInSlot[] moveInSlots = target.GetComponents<IMoveInSlot>();
            foreach(IMoveInSlot moveInSlot in moveInSlots)
            {
                moveInSlot.Own_Slot = this;
            }
            DisPlaySlot(target);
            IOnPutInSlot[] onPutInSlots = target.GetComponentsInChildren<IOnPutInSlot>();
            foreach (IOnPutInSlot onPutInSlot in onPutInSlots)
            {
                onPutInSlot.PutInSlot(this);
            }
        }
        element = target;
    }

    public bool Contains(Transform target)
    {
        return element == target;
    }

    public void Remove(Transform target)
    {
        if (element == target)
            element = null;
    }

    public abstract void ReturnSlot();
    protected abstract void DisPlaySlot(Transform own_slot);

}

public abstract class BaseSlotController : MonoBehaviour
{
    public ISlot slot;
    private void Awake()
    {
        AddSlot();
    }

    protected virtual void AddSlot()
    {
        slot = CreateSlot();
        IAddSlot[] addSlots = GetComponents<IAddSlot>();
        foreach(IAddSlot addSlot in addSlots)
        {
            addSlot.AddSlot(slot);
        }
        if (transform.parent == null) return;
        IAddChildSlot[] addChildSlots = transform.parent.GetComponents<IAddChildSlot>();
        foreach (IAddChildSlot addChildSlot in addChildSlots)
        {
            addChildSlot.OnAddChildSlot(slot);
        }
    }

    protected abstract ISlot CreateSlot();
}
