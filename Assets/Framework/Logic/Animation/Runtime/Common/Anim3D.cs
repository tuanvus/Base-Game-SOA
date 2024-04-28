using LTA.Animation;
using LTA.Display;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ComponentType(typeof(Anim3D), typeof(LTA.Animation.AnimationInfo), "anim3d")]
public class Anim3D : DisplayAnimation
{
    GameObject instance;
    protected override GameObject Instance {
        get
        {
            if (instance == null)
            {
                instance = DisplayHelper.CreatePrefab(info.path);
                instance.transform.SetParent(transform);
            }
            return instance;
        }
    }

    public override IAnimation Animation
    {
        get
        {
            IAnimation animation = Instance.GetComponent<UnityAnim>();
            if (animation == null)
            {
                animation = Instance.AddComponent<UnityAnim>();
            }
            return animation;
        }
    }
}
