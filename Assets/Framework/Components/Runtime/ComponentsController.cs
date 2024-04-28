using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentsController : MonoBehaviour
{
    List<Component> components = new List<Component>();

    //protected List<Component> Components
    //{
    //    get
    //    {
    //        return components;
    //    }
    //}

    protected void AddComponentInfos(Dictionary<string, ComponentInfo[]> dic_Key_Components,Action<string,Component> editComponent)
    {
        foreach (KeyValuePair<string, ComponentInfo[]> keyValue in dic_Key_Components)
        {
            string key = keyValue.Key;
            AddComponentInfos(keyValue.Value, (component) =>
            {
                components.Add(component);
                editComponent(key,component);
            });
        }
    }

    protected void AddComponentInfo(Dictionary<string, ComponentInfo> dic_Key_Components, Action<string, Component> editComponent)
    {
        foreach (KeyValuePair<string, ComponentInfo> keyValue in dic_Key_Components)
        {
            string key = keyValue.Key;
            ComponentHelper.AddComponent(gameObject, keyValue.Value , (component) =>
            {
                components.Add(component);
                editComponent(key, component);
            });
        }
    }

    protected void AddComponentInfos(ComponentInfo[] componentInfos, Action<Component> editComponent)
    {
        for (int i = 0; i < componentInfos.Length; i++)
        {
            ComponentHelper.AddComponent(gameObject, componentInfos[i], editComponent);
        }
    }

    protected virtual void Clear()
    {
        foreach (Component component in components)
        {
            if (component == null) continue;
            Destroy(component);
        }
        components.Clear();
    }
    private void OnDestroy()
    {
        Clear();
    }
}
