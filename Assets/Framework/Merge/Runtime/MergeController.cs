
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class MergeController : MonoBehaviour
{
    protected static List<MergeController> merges = new List<MergeController>();

    protected const float distanceAllowMerge = 1f;

    public static MergeController GetMergeObject(MergeController checkedObject)
    {
        MergeController result = null;
        float minDistance = 10000;
        foreach (MergeController merge in merges)
        {
            if (merge == checkedObject) continue;
            float distance = Vector3.Distance(merge.transform.position, checkedObject.transform.position);
            if (checkedObject.CheckMerge(merge) && (distance <=  minDistance || result == null) && distance <= distanceAllowMerge)
            {
                result = merge;
                minDistance = distance;
            }
        }
        return result;
    }

    public abstract bool CheckMerge(MergeController merge);

    protected virtual void Awake()
    {
        merges.Add(this);
    }

    public abstract void Merge(MergeController merge,Action endMerge = null);

    protected virtual void OnDestroy()
    {
        if (merges.Contains(this))
            merges.Remove(this);
    }


}
