using System.Collections.Generic;
using UnityEngine;
using LTA.NonEntity;
using LTA.Data;
namespace LTA.Target
{
    public class TargetKey
    {
        public const string BEST_TARGETS = "bestTargets";
        public const string TARGETS = "targets";
    }
    [ComponentType(typeof(FilterTargetController), "filterTargets", "NonEntities")]
    public class FilterTargetController : NonEntityController
    {
        bool includeMine = false;

        public bool IncludeTarget
        {
            set
            {
                includeMine = value;
            }
        }

        protected List<ICheckTarget> targets = new List<ICheckTarget>();
        protected List<ICheckBestTarget> bestTargets = new List<ICheckBestTarget>();
        public List<ICheckTarget> Targets
        {
            get
            {
                return this.targets;
            }
        }

        public List<ICheckBestTarget> BestTargets
        {
            get
            {
                return this.bestTargets;
            }
        }

        public T GetTarget<T>() where T : class, ICheckTarget
        {
            foreach (ICheckTarget checkTarget in targets)
            {
                if (checkTarget is T) return (T)checkTarget;
            }
            return null;
        }

        public bool CheckTarget(Transform target)
        {
            if (target == this.transform&&!includeMine) return false;
            foreach (ICheckTarget checkTarget in targets)
            {
                if (!checkTarget.CheckTarget(target)) return false;
            }
            return true;
        }

        public bool CheckBestTarget(Transform target1, Transform target2)
        {
            foreach (ICheckBestTarget checkTarget in bestTargets)
            {
                if (!checkTarget.CheckTarget(target1, target2)) return false;
            }
            return true;
        }

        protected override void EditComponent(string key, Component component)
        {
            switch (key)
            {
                case TargetKey.BEST_TARGETS:
                    ICheckBestTarget bestTarget = (ICheckBestTarget)component;
                    bestTargets.Add(bestTarget);
                    break;
                case TargetKey.TARGETS:
                    ICheckTarget target = (ICheckTarget)component;
                    targets.Add(target);
                    break;
            }
            base.EditComponent(key, component);
        }

        protected override void Clear()
        {
            base.Clear();
            bestTargets.Clear();
            targets.Clear();
        }
    }
    public interface ICheckTarget
    {
        bool CheckTarget(Transform target);
    }
    public interface ICheckBestTarget
    {
        bool CheckTarget(Transform target1, Transform target2);
    }

    [HideInInspector]
    public class FilterTargetsController : NonEntitysHasOwnController<FilterTargetController>
    {

    }

}
