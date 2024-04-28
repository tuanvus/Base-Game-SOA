using LTA.Base;
using System;
using System.Collections.Generic;
using UnityEngine;
namespace LTA.Effect
{
    public class EffectController : MonoBehaviour
    {
        Dictionary<TypeEffect, List<IEffect>> dic_Type_Effect = new Dictionary<TypeEffect, List<IEffect>>();
        IEffect[] effects;

        [SerializeField]
        bool isHide;
        public IEffect[] Effects
        {
            get
            {
                return effects;
            }
        }

        public List<IEffect> GetEffects(TypeEffect typeEffect)
        {
            if (!dic_Type_Effect.ContainsKey(typeEffect))
                throw new Exception("Don't have Effect Type " + typeEffect);
            return dic_Type_Effect[typeEffect];
        }

        private void Awake()
        {
            Init();
            if (isHide)
            {
                foreach(IEffect effect in Effects)
                {
                    effect.Hide();
                }
            }
        }

        public virtual void Init()
        {
            
            effects = GetComponentsInChildren<IEffect>();
            foreach (IEffect effect in effects)
            {
                TypeEffect type = effect.TypeEffect;
                if (!dic_Type_Effect.ContainsKey(type))
                {
                    dic_Type_Effect.Add(type, new List<IEffect>());
                }
                dic_Type_Effect[type].Add(effect);
            }
        }

        public void ShowEffect(TypeEffect typeEffect, Action endEffect)
        {
            try
            {
                if (!dic_Type_Effect.ContainsKey(typeEffect))
                {
                    endEffect();
                    return;
                }
                List<IEffect> effects = GetEffects(typeEffect);
                if (endEffect == null)
                {
                    ShowEffect(typeEffect);
                    return;
                }
                int countEffect = 0;
                foreach (IEffect effect in effects)
                {
                    if (effect.GetTimeDelayed() == 0)
                    {
                        effect.ShowEffect(() =>
                        {
                            countEffect++;
                            if (countEffect == effects.Count)
                            {
                                endEffect();
                            }
                        });
                    }
                    else
                    {
                        LeanTween.delayedCall(effect.GetTimeDelayed(), () =>
                        {
                            effect.ShowEffect(() =>
                            {
                                countEffect++;
                                if (countEffect == effects.Count)
                                {
                                    endEffect();
                                }
                            });
                        });
                    }
                    
                }
            }
            catch
            {
                endEffect();
            }
        }
        public void HideEffect(TypeEffect typeEffect, Action endEffect)
        {
            try
            {
                if (!dic_Type_Effect.ContainsKey(typeEffect))
                {
                    endEffect();
                    return;
                }
                List<IEffect> effects = GetEffects(typeEffect);
                if (endEffect == null)
                {
                    HideEffect(typeEffect);
                    return;
                }
                int countEffect = 0;
                foreach (IEffect effect in effects)
                {
                    effect.HideEffect(() =>
                    {
                        countEffect++;
                        if (countEffect == effects.Count)
                        {
                            endEffect();
                        }
                    });
                }
            }
            catch
            {
                endEffect();
            }
        }
        public void ShowEffect(TypeEffect typeEffect)
        {
           
            try
            {
                List<IEffect> effects = GetEffects(typeEffect);
                Debug.Log("Show Effect " + effects.Count);
                foreach (IEffect effect in effects)
                {
                    if (effect.GetTimeDelayed() == 0)
                    {
                        effect.ShowEffect();
                    }
                    else
                    {
                        LeanTween.delayedCall(effect.GetTimeDelayed(), effect.ShowEffect);
                    }

                }
            }
            catch (Exception e)
            {
                Debug.LogError("Can't Show Effect Type " + typeEffect + " " + e.Message);
            }
        }
        public void HideEffect(TypeEffect typeEffect)
        {
            try
            {
                List<IEffect> effects = GetEffects(typeEffect);
                foreach (IEffect effect in effects)
                {
                    effect.HideEffect();
                }
            }
            catch
            {
                Debug.LogError("Can't Hide Effect Type " + typeEffect);
            }
        }
        public void Hide(TypeEffect typeEffect)
        {
            try
            {
                List<IEffect> effects = GetEffects(typeEffect);
                foreach (IEffect effect in effects)
                {
                    effect.Hide();
                }
            }
            catch
            {
                Debug.LogError("Can't Hide Type " + typeEffect);
            }
        }
    }
}
