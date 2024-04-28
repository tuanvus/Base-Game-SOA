using UnityEngine;

public class SlotDragMoveController : DragMoveController,IOnDrag,IOnDragDown,IOnDragUp,IMoveInSlot
{
    [SerializeField]
    float allowDistance = 1f;

    ISlot own_Slot;
    public ISlot Own_Slot { get => own_Slot; set => own_Slot = value; }
    public ISlot checkSlot = null;

    

    public void ReturnSlot()
    {
        own_Slot.IsMoving = false;
        MoveUpdate(own_Slot.Pos,own_Slot.ReturnSlot);
    }

    protected virtual void OnDestroy()
    {
        own_Slot.Remove(transform);
    }

    public virtual void OnDrag()
    {
        checkSlot = SlotController.GetNearSlot(transform, allowDistance);
        if (Effect != null)
            Effect.ShowEffect(TypeEffect.Drag);
        if (checkSlot == null)
        {

            HighLight.Instance.SetHighLight(Vector3.one * 1000);
        }
        else
        {
            HighLight.Instance.SetHighLight(checkSlot.Pos);
        }
    }

    public virtual void OnDragDown()
    {
        if (own_Slot != null)
            own_Slot.IsMoving = true;
    }

    public virtual void OnDragUp()
    {
        if (Effect != null)
            Effect.HideEffect(TypeEffect.Drag);
        HighLight.Instance.SetHighLight(Vector3.one * 1000);
        ReturnSlot();
    }
}
