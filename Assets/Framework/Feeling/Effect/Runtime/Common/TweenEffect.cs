using LTA.Base;
using System;
namespace LTA.Effect
{
    public abstract class TweenEffect : BaseEffect
    {
        public float timeEffect = 0.05f;
        
        public LeanTweenType _LeanTweenType = LeanTweenType.linear;
        
        TweenBehaviour behaviour;

        protected TweenBehaviour Behaviour
        {
            get
            {
                if (behaviour == null)
                {
                    behaviour = gameObject.AddComponent<TweenBehaviour>();
                    behaviour.timePerforme = timeEffect;
                    behaviour._LeanTweenType = _LeanTweenType;
                }
                return behaviour;
            }
        }

        private void OnDestroy()
        {
            if (behaviour != null)
                Destroy(behaviour);
        }
    }
}
