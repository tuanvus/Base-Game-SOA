using System.Collections.Generic;
using UnityEngine;
using LTA.Condition;
using LTA.Handle;
using LTA.NonEntity;
using LTA.Action;
using LTA.Data;
using System.Collections;

namespace LTA.Action
{
    public class ActionKey
    {
        public const string CONDITIONS = "conditions";
        public const string HANDLES = "handles";
    }
    public interface IOnAction
    {
        void OnAction(ActionController action);
    }

    public interface IOnEndAction
    {
        void OnEndAction(ActionController action);
    }

    public interface IOnRemoveAction
    {
        void OnRemoveAction(ActionController action);
    }

    public interface IGetOwnAction
    {
        public ActionController OwnAction { set; }
    }
}

[ComponentType(typeof(ActionController),"actions",ComponentType.NonEntities)]
public class ActionController : NonEntityController
    {

        protected List<ICondition>      conditions              = new List<ICondition>();
        protected List<IResetCondition> resetConditions         = new List<IResetCondition>();
        protected List<IHandle>   handles                       = new List<IHandle>();
        protected List<IClearHandle> clearHandles               = new List<IClearHandle>();

        protected override void EditComponent(string key, Component component)
        {
            SetOwnAction(component);
            switch (key)
            {
                case ActionKey.CONDITIONS:
                    ICondition condition = (ICondition)component;
                    condition.SuitableCondition = OnSuitableCondition;
                    conditions.Add(condition);
                    if (component is IResetCondition)
                    {
                        resetConditions.Add((IResetCondition)component);
                    }
                    break;
                case ActionKey.HANDLES:
                    IHandle handle = (IHandle)component;
                    handles.Add(handle);
                    if (handle is IClearHandle)
                    {
                        clearHandles.Add((IClearHandle)handle);
                    }
                    break;
            }
            base.EditComponent(key, component);
        }

        void SetOwnAction(Component component)
        {
            if (component is IGetOwnAction)
            {
                ((IGetOwnAction)component).OwnAction = this;
            }
        }

        void OnSuitableCondition(ICondition condition)
        {
            if (!IsAllowAction) return;
            Action();
        }

        int countActionComplete;

        bool isEndAction = true;

        public bool IsEndAction
        {
            set
            {
                if (value)
                {
                    ClearHandle();
                    IOnEndAction[] onEndActions = GetComponentsInChildren<IOnEndAction>();
                    foreach (IOnEndAction onEndAction in onEndActions)
                    {
                        onEndAction.OnEndAction(this);
                    }
                }
                isEndAction = value;
                StopAllCoroutines();
            }
            get
            {
                return isEndAction;
            }
        }
        

        protected virtual void OnEndAction()
        {
            if (!IsAllowAction) return;
            Action();
        }

        public void Action()
        {
            //if (handles == null) return;
            IsEndAction = false;
            ResetCondition();
            countActionComplete = 0;
            IOnAction[] onActions = GetComponentsInChildren<IOnAction>();
            foreach (IOnAction onAction in onActions)
            {
                onAction.OnAction(this);
            }
            if (handles.Count == 0)
            {
                IsEndAction = true;
                
                OnEndAction();
                return;
            }
            foreach (IHandle handle in handles)
            {
                handle.EndHandle = EndAction;
                Handle(handle);
            }
        }
       

        protected virtual void Handle(IHandle handle)
        {
            handle.Handle();
        }

        //int count = 0;

        void EndAction(IHandle handle)
        {
            //if (count == 10)
            //{
            //    return;
            //}
            //count++;
            handle.EndHandle = null;
            countActionComplete++;
            if (countActionComplete == handles.Count)
            {
                StartCoroutine(IeEndAction());
            }
        }
        IEnumerator IeEndAction()
        {
            yield return null;
            IsEndAction = true;
            OnEndAction();
        }

    protected void ResetCondition()
        {
            foreach (IResetCondition resetCondition in resetConditions)
            {
                resetCondition.ResetCondition();
            }
        }

        public void ClearHandle()
        {
            foreach (IClearHandle clearHandle in clearHandles)
            {
                clearHandle.OnClear();
            }
        }

        public virtual bool IsAllowAction
        {
            get
            {
                //Debug.Log(NonEntityInfo.name + " " + IsAvailableConditions);
                //Debug.Log(NonEntityInfo.name + " " + isEndAction);
                return IsAvailableConditions && isEndAction;
            }
        }
        
        protected bool IsAvailableConditions
        {
            get
            {
                foreach (ICondition condition in conditions)
                {
                    //Debug.Log(NonEntityInfo.name + " " + condition.IsSuitable);
                    if (!condition.IsSuitable)
                        return false;
                }
                return true;
            }
        }

        protected override void Clear()
        {
            IOnRemoveAction[] onRemoveActions = GetComponentsInChildren<IOnRemoveAction>();
            foreach (IOnRemoveAction onRemoveAction in onRemoveActions)
            {
                onRemoveAction.OnRemoveAction(this);
            }
            base.Clear();
            ClearHandle();
            conditions.Clear();
            resetConditions.Clear();
            handles.Clear();
            clearHandles.Clear();
        }
    }

