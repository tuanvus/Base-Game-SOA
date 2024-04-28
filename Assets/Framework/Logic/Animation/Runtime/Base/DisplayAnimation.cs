using LTA.Display;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LTA.Animation
{
    [System.Serializable]
    public class AnimationInfo : Display.DisplayInfo
    {
        public float speedAnim = 1f;
    }
    public abstract class DisplayAnimation : BaseDisplayController<AnimationInfo>
    {
        public abstract IAnimation Animation { get; }
        public override object Info {
            set
            {
                base.Info = value;
                Animation.SpeedAnim = info.speedAnim;
                Animation.Own = transform;
            }
        }


    }
}

