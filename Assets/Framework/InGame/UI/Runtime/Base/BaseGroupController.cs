using System.Collections;
using System.Collections.Generic;
using LTA.Base;
using LTA.Effect;
using UnityEngine;

public class BaseGroupController : EffectController
{
    [SerializeField]
    float timeDelay = 0f;
    [SerializeField]
    bool isShowOnStart = true;
    [SerializeField]
    bool isDestroy = true;
    protected virtual void Start()
    {
        List<IEffect> effects = GetEffects(TypeEffect.Show);
        foreach (IEffect effect in effects)
        {
            effect.Hide();
        }
        if (isShowOnStart)
        {
            OnShowDelayTime(timeDelay);
        }
    }
    //public IEffect[] items;

    public void Hide()
    {
        List<IEffect> effects = GetEffects(TypeEffect.Show);
        foreach (IEffect effect in effects)
        {
            effect.Hide();
        }
    }

    public void OnShowDelayTime(float timeDelay)
    {
        StartCoroutine(ShowEffect(timeDelay, isDestroy));
    }
    
    public void OnHideDelayTime(float timeDelay)
    {
        StartCoroutine(HideEffect(timeDelay, isDestroy));
    }

    
    private IEnumerator ShowEffect(float timeDelay, bool isDestroy = true, bool isCheckDone = false)
    {
        yield return new WaitForEndOfFrame();
        List<IEffect> effects = GetEffects(TypeEffect.Show);
        foreach (IEffect effect in effects)
        {
            bool isDone = false;
            effect.ShowEffect(() =>
            {
                isDone = true;
            });
            if (isCheckDone)
            {
                yield return new WaitUntil(() =>
                {
                    return isDone;
                });
            }
            yield return new WaitForSeconds(timeDelay);
        }

        if (isDestroy)
        {
            foreach (IEffect effect in effects)
            {
                Destroy((MonoBehaviour)effect);
            }
        }
    }
    
    private IEnumerator HideEffect(float timeDelay, bool isDestroy = true, bool isCheckDone = false)
    {
        yield return new WaitForEndOfFrame();
        List<IEffect> effects = GetEffects(TypeEffect.Show);
        foreach (IEffect effect in effects)
        {
            bool isDone = false;
            effect.HideEffect(() =>
            {
                isDone = true;
            });
            if (isCheckDone)
            {
                yield return new WaitUntil(() =>
                {
                    return isDone;
                });
            }
            yield return new WaitForSeconds(timeDelay);
        }

        if (isDestroy)
        {
            foreach (IEffect effect in effects)
            {
                Destroy((MonoBehaviour)effect);
            }
        }
    }
}
