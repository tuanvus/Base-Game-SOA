using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTA.Display;
namespace LTA.Animation
{

    public abstract class BaseAnimation : MonoBehaviour, IAnimation
    {
        protected float speedAnim = 1;

        public float SpeedAnim
        {
            set
            {
                speedAnim = value;
            }
        }

        public string pre_Anim = "";
        
        public string anim = "";

        bool isLoop = true;

        Transform own;

        public Transform Own
        {
            set
            {
                own = value;
            }
        }

        //public abstract int LayerOrder { get; set; }

        //public abstract float Height { get; }

        //public abstract float Alpha { get; set; }

        Action startAnim;

        Action endAnim;

        Dictionary<string,Action> dic_Event_Action  = new Dictionary<string,Action>();

        public abstract bool CheckAnim(string animName);

        public void Play(string animName, bool loop = true)
        {
            anim = animName;
            isLoop = loop;
            Play(loop);
        }

        public abstract void Play(string animName,int track, bool loop = true);

        public abstract void RemoveTrack(int track);

        public abstract void Play(bool loop = true);
        protected virtual void OnStartAnim()
        {
            IOnStartAnimation[] onStartAnimations = own.GetComponents<IOnStartAnimation>();
            foreach(IOnStartAnimation onStartAnimation in onStartAnimations)
            {
                onStartAnimation.OnStartAnimation(anim);
            }
            if (startAnim != null)
            {
                startAnim();
                startAnim = null;
            }
        }

        protected virtual void OnEventAnim(string eventAnim)
        {
            IOnEventAnimation[] onEventAnimations = own.GetComponents<IOnEventAnimation>();
            foreach(IOnEventAnimation onEventAnimation in  onEventAnimations)
            {
                onEventAnimation.OnEvent(anim, eventAnim);
            }
            if (dic_Event_Action.ContainsKey(eventAnim))
            {
                dic_Event_Action[eventAnim]();
                dic_Event_Action.Remove(eventAnim);
            }
        }

        protected virtual void OnEndAnim()
        {
            IOnEndAnimation[] onEndAnimations = own.GetComponents<IOnEndAnimation>();
            foreach(IOnEndAnimation onEndAnimation in onEndAnimations)
            {
                onEndAnimation.OnEndAnimation(anim);
            }
            if (endAnim != null)
            {
                endAnim();
                endAnim = null;
            }
        }


        public void SetPreAnim(string preAnim)
        {
            this.pre_Anim = preAnim;
            Play(isLoop);
        }

        public void SetStartAnim(Action startAnim)
        {
            this.startAnim = startAnim;
        }

        public void SetEndAnim(Action endAnim)
        {
            this.endAnim = endAnim;
        }

        public void SetEventAction(string eventName, Action eventAction)
        {
            if (dic_Event_Action.ContainsKey(eventName))
            {
                dic_Event_Action[eventName] = eventAction;
                return;
            }
            dic_Event_Action.Add(eventName, eventAction);
        }

        //public virtual void OnUpLevel(int level)
        //{
        //    if (!CharacterDataController.Instance.animationVO.CheckKey(OwnName)) return;
        //    AnimationInfo animationInInfo = CharacterDataController.Instance.animationVO.GetData<AnimationInfo>(OwnName,level);
        //    if (animationInInfo == null) return;
        //    speedAnim = animationInInfo.speedAnim;
        //    IEndLoadAnimation[] endLoadAnimations = transform.parent.GetComponents<IEndLoadAnimation>();
        //    foreach(IEndLoadAnimation endLoadAnimation in endLoadAnimations)
        //    {
        //        endLoadAnimation.OnEndLoadAnimtion();
        //    }
        //}
    }
}
