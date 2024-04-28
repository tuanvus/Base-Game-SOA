using LTA.Animation;
using LTA.Handle;
using LTA.NonEntity;
using System;
using UnityEngine;

namespace LTA.State
{

    [System.Serializable]
    public class StateInfo
    {
        public string anim;
        public int priority = 0;
        //public string[] ExceptionStates;
        //public bool isCanSkiped = true;
        public bool isLoop = true;
        public NonEntityInfo nextState = null;
    }

    [ComponentType(typeof(StateController), typeof(StateInfo), "states", "NonEntities")]
    public class StateController : ActionController,ISetInfo,IHandle
    {

        StatesController states;

        StatesController States
        {
            get
            {
                if (states == null)
                {
                    states = GetComponent<StatesController>();
                    if (states == null)
                    {
                        states = gameObject.AddComponent<StatesController>();
                    }
                }
                return states;
            }
        }
        StateInfo info;
        
        public object Info
        {
            set
            {
                info = (StateInfo)value;
            }
        }

        public StateInfo StateInfo
        {
            get
            {
                return info;
            }
        }

        public override bool IsAllowAction {
            get {
                //Debug.Log(NonEntityInfo.name + " IsAllowAction 0");
                if (!isEndLoadState) return false;
                //Debug.Log(NonEntityInfo.name + " IsAllowAction 1");
                if (!base.IsAllowAction) return false;
                //Debug.Log(NonEntityInfo.name + " IsAllowAction 2");
                if (!States.CheckState(this))
                {
                    ResetCondition();
                }
                //Debug.Log(NonEntityInfo.name + " IsAllowAction 3");
                return States.CheckState(this);
            }
        }

        Action<IHandle> endHandle;
        public Action<IHandle> EndHandle { set => endHandle = value; }

        bool isEndLoadState = false;

        protected override void AddInfos()
        {
            base.AddInfos();
            //States.AddNonEntity(this);
            handles.Add(this);
            if (States.DefaultState == null)
            {
                States.DefaultState = this;
            }
            isEndLoadState = true;
        }

        protected override void Clear()
        {
            isEndLoadState = false;
            base.Clear();
            //States.RemoveNonEntity(this);
        }

        public void Handle()
        {
            States.CurrentState = this;
            if (endHandle != null)
            {
                endHandle(this);
            }
        }
    }


}
