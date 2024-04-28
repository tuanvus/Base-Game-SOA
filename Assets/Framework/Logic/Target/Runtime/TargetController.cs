
using LTA.Data;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace LTA.Target
{
    [ComponentType(typeof(TargetController), "target")]
    public class TargetController : MonoBehaviour /*,ICameraMove*/
    {
        static List<TargetController> targets = new List<TargetController>();
        
        public static Transform GetTarget(FilterTargetController filter)
        {
            TargetController bestTarget = null;
            foreach (TargetController target in targets)
            {
                if (target == null) continue;
                if (filter == null || !filter.CheckTarget(target.transform)) continue;
                if (bestTarget == null || filter.CheckBestTarget(bestTarget.transform,target.transform))
                {
                    bestTarget = target;
                }
            }
            if (bestTarget != null)
                return bestTarget.transform;
            return null;
        }

        public static List<Transform> GetTargets(FilterTargetController filter,int numTarget)
        {
            //Debug.Log(numTarget + " " +filter.name);
            if (numTarget == 0) return new List<Transform>();
            List<Transform> bestTargets = new List<Transform>();
            if (numTarget == 1)
            {
                List<Transform> results = new List<Transform>();
                Transform target = GetTarget(filter);
                if (target != null)
                    results.Add(target);
                return results;
            }
            foreach (TargetController target in targets)
            {
                if (target == null) continue;
                if (filter == null || !filter.CheckTarget(target.transform)) continue;
                if (bestTargets.Count == numTarget)
                {
                    Transform removeTarget = null;
                    foreach (Transform bestTarget in bestTargets)
                    {
                        if (!filter.CheckBestTarget(target.transform,bestTarget))
                        {
                            removeTarget = bestTarget;
                        }
                    }
                    if (removeTarget == null)
                    {
                        continue;
                    }
                    bestTargets.Remove(removeTarget);
                }
                bestTargets.Add(target.transform);
            }
            return bestTargets;
        }

        private void Start()
        {
            targets.Add(this);
        }

        private void OnDestroy()
        {
            if (targets.Contains(this))
                targets.Remove(this);
        }
    }
}
