using LTA.Data;
using UnityEngine;

namespace LTA.Display
{
    [System.Serializable]
    public class DisplayInfo
    {
        public string path;
    }

    public abstract class BaseDisplayController<T> : MonoBehaviour, ISetInfo where T : DisplayInfo
    {
        protected T info;
        public virtual object Info
        {
            set
            {
                info = (T)value;
                Show();
            }
        }

        protected abstract GameObject Instance
        {
            get;
        }

        void Show()
        {
            Instance.transform.localPosition = Vector3.zero;
        }

        private void OnDestroy()
        {
            if (Instance != null)
            {
                Destroy(Instance);
            }
        }
    }

}
