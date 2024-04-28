using System.Collections;
using System.Collections.Generic;
using LTA.Base;
using LTA.Toucher;
using UnityEngine;
using LTA.Effect;
[DisallowMultipleComponent]
public class DragMoveController : TweenBehaviour,IOnTapDown, IOnTapHold,IOnTapUp
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

    protected virtual void MoveObject()
    {
#if UNITY_EDITOR || UNITY_WEBGL || UNITY_STANDALONE
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
#else
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
#endif
        Vector3 newpos = new Vector3(pos.x, pos.y, transform.position.z);
        MoveUpdate(newpos);
    }

    public virtual void OnTapDown(LayerMask layer)
    {
        if (layer.value != 1<<this.gameObject.layer) return;
        if (Effect != null)
        {
            Effect.ShowEffect(TypeEffect.Drag);
        }
        IOnDragDown[] onDragDowns = GetComponentsInChildren<IOnDragDown>();
        foreach (IOnDragDown onDragDown in onDragDowns)
        {
            onDragDown.OnDragDown();
        }
    }

    public virtual void OnTapUp(LayerMask layer)
    {
        if (layer.value != 1<<this.gameObject.layer) return;
        if (Effect != null)
        {
            Effect.Hide(TypeEffect.Drag);
        }
        IOnDragUp[] onDragUps = GetComponentsInChildren<IOnDragUp>();
        foreach (IOnDragUp onDragUp in onDragUps)
        {
            onDragUp.OnDragUp();
        }
    }

    public virtual void OnTapHold(LayerMask layer)
    {
        if (layer.value != 1<<this.gameObject.layer) return;
        MoveObject();
        IOnDrag[] onDrags = GetComponentsInChildren<IOnDrag>();
        foreach (IOnDrag onDrag in onDrags)
        {
            onDrag.OnDrag();
        }
    }
}
