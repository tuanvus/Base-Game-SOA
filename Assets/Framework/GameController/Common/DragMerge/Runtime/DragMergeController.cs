using LTA.Base;
using System.Collections.Generic;
//using LTA.UI.Effect;
using UnityEngine;
[RequireComponent(typeof(TweenBehaviour))]
public abstract class DragMergeController : MergeController,IOnDrag,IOnDragDown,IOnDragUp
{
    TweenBehaviour behaviourController;

    TweenBehaviour BehaviourController
    {
        get
        {
            if (behaviourController == null)
            {
                behaviourController = gameObject.AddComponent<TweenBehaviour>();
                behaviourController.timePerforme = 0.05f;
            }
            return behaviourController;
        }
    }

    protected static List<MergeController> canMerges = new List<MergeController>();

    public static List<MergeController> GetMergeObjects(MergeController checkedObject)
    {
        List<MergeController> results = new List<MergeController>();
        foreach (MergeController merge in merges)
        {
            if (merge == checkedObject) continue;
            if (checkedObject.CheckMerge(merge))
            {
                results.Add(merge);
            }
        }
        return results;
    }

    MergeController checkmerge;
    public void OnDrag()
    {
        checkmerge = GetMergeObject(this);
        if (checkmerge == null)
        {
            return;
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (canMerges.Contains(this)) canMerges.Remove(this);
    }

    public void OnDragDown()
    {
        canMerges = GetMergeObjects(this);
    }

    public void OnDragUp()
    {
        if (checkmerge == null)
        {
            return;
        }
        BehaviourController.MoveToPos(checkmerge.transform.position + Vector3.up * 0.1f, () =>
        {
            BehaviourController.MoveToPos(checkmerge.transform.position, () =>
            {
                Merge(checkmerge);
            });
        });
    }
}
