using System;
using LTA.Toucher;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTA.Effect;
public class TapSelectController : MonoBehaviour, IOnTapDown
{
    public static List<Transform> tapSelects = new List<Transform>();

    protected static int GetIndexElementSelected(Transform element)
    {
        for (int i = 0;i < tapSelects.Count;i++)
        {
            if (element == tapSelects[i])
            {
                return i;
            }
        }
        return -1;
    }

    public virtual void OnTapDown(LayerMask layer)
    {
        if (layer.value !=  1<<this.gameObject.layer) return;
        OnTapSelect();
    }

    protected virtual void OnTapSelect()
    {
        
        Select(transform);
    }

    protected void Select(Transform target)
    {
        Debug.Log("Select 0");
        EffectController effect = target.GetComponent<EffectController>();
        if (!tapSelects.Contains(target))
        {
            OnAddElement(target, effect);
        }
        else
        {
            OnRemoveElement(target, effect);
        }
        Debug.Log("Select 1");
    }

    protected virtual void OnAddElement(Transform target,EffectController effect)
    {
        Debug.Log("OnAddElement 0");
        if (effect != null)
            effect.ShowEffect(TypeEffect.Select);
        Debug.Log("OnAddElement 1");
        tapSelects.Add(target);
        Debug.Log("OnAddElement 2");
        IOnSelect[] onSelects = target.GetComponents<IOnSelect>();
        Debug.Log("OnAddElement 3");
        if (onSelects != null)
        {
            foreach (IOnSelect onSelect in onSelects)
            {
                onSelect.OnSelect();
            }
        }
        Debug.Log("OnAddElement 4");
    }

    protected virtual void OnRemoveElement(Transform target, EffectController effect)
    {
        if (effect != null)
            effect.HideEffect(TypeEffect.Select);
        tapSelects.Remove(target);
        IOnDeselect[] onDeselects = GetComponents<IOnDeselect>();
        foreach(IOnDeselect onDeselect in onDeselects)
        {
            onDeselect.OnDeselect();
        }
    }
    protected void ClearListSelects()
    {
        Debug.Log("ClearListSelects 0");
        foreach(Transform target in tapSelects)
        {
            EffectController effect = target.GetComponent<EffectController>();
            if (effect != null)
                effect.HideEffect(TypeEffect.Select);
        }
        Debug.Log("ClearListSelects 1");
        tapSelects.Clear();
    }

    private void OnDestroy()
    {
        if (tapSelects.Contains(transform))
            tapSelects.Remove(transform);
    }
}
