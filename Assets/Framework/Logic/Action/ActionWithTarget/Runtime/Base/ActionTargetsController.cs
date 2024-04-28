using System.Collections.Generic;
using UnityEngine;
using LTA.Condition;
using LTA.Handle;
using LTA.Target;
using LTA.NonEntity;
using LTA.Data;
using System;

[System.Serializable]
    public class ActionTargetsInfo
    {
        public int numTarget = 1;
        public bool isChangeTarget = true;
        public bool includeMine = false;
    }

    [ComponentType(typeof(ActionTargetsController), typeof(ActionTargetsInfo), "actionTargets",ComponentType.NonEntities)]
    public class ActionTargetsController : ActionController,ISetInfo
    {

    FilterTargetsController filters;

    FilterTargetsController Filters
    {
        get
        {
            if (filters == null)
            {
                filters = GetComponent<FilterTargetsController>();
                if (filters == null)
                {
                    filters = gameObject.AddComponent<FilterTargetsController>();
                }
            }
            return filters;
        }
    }

    FilterTargetController filter;


    protected override void AddNonEntityInfo(string key, NonEntityInfo nonEntityInfo, Action<NonEntityController> editNonEntityController)
    {
        if (nonEntityInfo.type == typeof(FilterTargetController))
        {
            filter = Filters.GetNonEntity(nonEntityInfo);
            if (filter != null) return;
        }
        base.AddNonEntityInfo(key, nonEntityInfo, editNonEntityController);
    }
    protected override void EditNonEntityController(string key, NonEntityController nonEntityController)
    {
        if (nonEntityController is FilterTargetController)
        {
            filter = (FilterTargetController)nonEntityController;
            Filters.AddNonEntity(filter, this);
        }
        base.EditNonEntityController(key, nonEntityController);
    }

    List<Transform> targets;

        protected override void Handle(IHandle handle)
        {

            if (handle is IGetTarget&& filter != null)
            {
                filter.IncludeTarget = info.includeMine;
                targets = TargetController.GetTargets(filter, info.numTarget);
                ((IGetTarget)handle).Targets = targets;
            }
            base.Handle(handle);
        }
        
        ActionTargetsInfo info;

        public object Info { 
            set => info = (ActionTargetsInfo)value; 
        }
    protected override void Clear()
    {
        Filters.RemoveNonEntity(filter, this);
        base.Clear();
    }
}

    public interface IGetTarget
    {
        List<Transform> Targets { set; }
    }
