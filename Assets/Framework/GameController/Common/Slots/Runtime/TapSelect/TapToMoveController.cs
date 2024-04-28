using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTA.Base;
using System;
using LTA.Effect;
public class TapToMoveController : TweenBehaviour, IMoveInSlot
{
    EffectController effect;

    protected EffectController Effect
    {
        get
        {
            if (effect == null)
                effect = GetComponent<EffectController>();
            return effect;
        }
    }

    protected ISlot own_Slot;
    public ISlot Own_Slot { get => own_Slot; set => own_Slot = value; }

    public void MoveToSlot(ISlot slot,Action endMove)
    {
        if (own_Slot != null)
            own_Slot.Remove(transform);
        //if (own_Slot != null)
        //    own_Slot.IsMoving = true;
        slot.IsMoving = true;
        if (Effect != null)
            Effect.ShowEffect(TypeEffect.Move);
        MoveToPos(slot.Pos, () => {
            slot.IsMoving = false;
            EndMove(slot,endMove);
        }
        );

    }

    protected virtual void EndMove(ISlot slot,Action endMove)
    {
        if (Effect != null)
            Effect.HideEffect(TypeEffect.Move);
        slot.Add(transform);
        if (endMove != null)
            endMove();
    }
}
