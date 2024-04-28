
using UnityEngine;
using System.Collections.Generic;
namespace LTA.Base
{
    public class LTABehaviour : MonoBehaviour
    {
        Dictionary<string,MonoBehaviour> dic_typeName_Object = new Dictionary<string, MonoBehaviour>();
        public T GetComponent<T>(bool isAutoAdd = true) where T : MonoBehaviour
        {
            string typeName = typeof(T).FullName;
            if (dic_typeName_Object.ContainsKey(typeName)) return dic_typeName_Object[typeName] as T;
            T component = base.GetComponent<T>();
            if (isAutoAdd)
            {
                if (component == null)
                    component = base.gameObject.AddComponent<T>();
            }
            if (component != null) dic_typeName_Object.Add(typeName,component);
            return component;
        }

        private void OnDestroy()
        {
            foreach(KeyValuePair<string, MonoBehaviour> typeName_Object in dic_typeName_Object)
            {
                Destroy(typeName_Object.Value);
            }
            dic_typeName_Object.Clear();
        }
    }
}
