using LTA.Animation;
using UnityEngine;

public class UnityAnim : BaseAnimation
{
    [SerializeField]
     Animator animator;

    protected Animator Animator
    {
        get
        {
            animator = GetComponent<Animator>();
            if (animator == null)
                animator = gameObject.AddComponent<Animator>();
            return animator;
        }
    }

    AnimationClip[] animationClips;

    AnimationClip[] AnimationClips
    {
        get
        {
            if (animationClips == null)
            {
                animationClips = Animator.runtimeAnimatorController.animationClips;
            }
            return animationClips;
        }

    }

    AnimationClip GetAnimatorClipInfo(string animName)
    {
        foreach (AnimationClip animationClip in AnimationClips)
        {
            if (animationClip.name == animName)
                return animationClip;
        }
        return null;
    }

    public override bool CheckAnim(string animName)
    {
        return GetAnimatorClipInfo(animName) != null;
    }
    //AnimationClip currnetAnimationClip;
    public override void Play(bool loop = true)
    {
        if (currentAnim == anim) return;
        currentAnim = anim;
        AnimationClip animationClip = GetAnimatorClipInfo(anim);
        AnimatorStateInfo animationStateInfo = Animator.GetCurrentAnimatorStateInfo(0);

        //Chua biet ngay xua dung lam gi
        //Animator.Rebind();
        //Animator.Update(0f);

        Animator.speed = 1 / speedAnim;
        if (loop)
            animationClip.wrapMode = WrapMode.Loop;
        else
            animationClip.wrapMode = WrapMode.Once;
        //Animator.CrossFadeInFixedTime(anim, 0.3f);

        //if (currnetAnimationClip == null)
        //{
        //Animator.CrossFade(anim, 0.15f);
        //}else
        //{
        //    Animator.CrossFade(anim, currnetAnimationClip.averageDuration);
        //}
        //currnetAnimationClip = animationClip;
        Animator.CrossFade(anim, 0.15f);

        //Animator.Play(anim);
        //Animator.CrossFade
    }

    string currentAnim = "";
    //private void Update()
    //{
    //    AnimatorStateInfo animationStateInfo = Animator.GetCurrentAnimatorStateInfo(0);
    //    Debug.Log(animationStateInfo.normalizedTime);
    //}

    public void StartAnim()
    {
        OnStartAnim();
    }

    public void OnEvent(string eventName)
    {
        OnEventAnim(eventName);
    }

    public void EndAnim()
    {
        OnEndAnim();
    }

    public override void Play(string animName, int track, bool loop = true)
    {
        AnimationClip animationClip = GetAnimatorClipInfo(animName);
        if (loop)
            animationClip.wrapMode = WrapMode.Loop;
        else
            animationClip.wrapMode = WrapMode.Once;
        Animator.Play(animName, track);
    }

    public override void RemoveTrack(int track)
    {
        //RemoveTrack
    }
}
