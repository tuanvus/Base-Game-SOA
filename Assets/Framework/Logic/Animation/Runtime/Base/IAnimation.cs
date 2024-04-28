using System;
using UnityEngine;

namespace LTA.Animation
{
    public interface IAnimation
    {
        float SpeedAnim { set; }
        Transform Own { set; }
        bool CheckAnim(string animName);

        void Play(string animName, bool loop = true);

        void SetStartAnim(Action startAnim);

        void SetEndAnim(Action endAnim);

        void SetEventAction(string eventName,Action eventAction);

        void Play(string animName, int track, bool loop = true);

        void RemoveTrack(int track);

        void SetPreAnim(string preAnim);
    }

    public interface IOnStartAnimation
    {
        void OnStartAnimation(string animName);
    }

    public interface IOnEndAnimation
    {
        void OnEndAnimation(string animName);
    }

    public interface IOnEventAnimation
    {
        
        void OnEvent(string animName, string eventAnim);
    }
}
