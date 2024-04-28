using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


public class ComponentInfoComparer : IEqualityComparer<ComponentInfo>
{
    public bool Equals(ComponentInfo info1, ComponentInfo info2)
    {
        return info1 == info2;
    }
    public int GetHashCode(ComponentInfo obj)
    {
        return 1;
    }
}



[System.Serializable]
public class ComponentInfo
{
    public static readonly ComponentInfoComparer ComponentInfoComparer = new ComponentInfoComparer();
    public Type type;
    public object data;

    public override bool Equals(object obj)
    {
        if (obj is null) return false;
        if (!(obj is ComponentInfo)) return false;
        ComponentInfo info2 = (ComponentInfo)obj;
        return type.Assembly.GetName() == info2.type.Assembly.GetName() && type.Name == info2.type.Name;
    }

    public static bool operator ==(ComponentInfo info1, ComponentInfo info2)
    {
        if (info2 is null) return info1 is null;
        return info1.type.Assembly.GetName() == info2.type.Assembly.GetName() && info1.type.Name == info2.type.Name;
    }

    public static bool operator !=(ComponentInfo info1, ComponentInfo info2)
    {
        if (info2 is null) return !(info1 is null);
        return info1.type.Assembly.GetName() != info2.type.Assembly.GetName() || info1.type.Name != info2.type.Name;
    }
    public override int GetHashCode()
    {
        return 1;
    }

}



public class ComponentTypeAttribute : System.Attribute
{
    public Type type;
    public Type typeInfo;
    public string identify;
    public string componentType;
    public ComponentTypeAttribute(Type type, Type typeInfo = null, string identify = null,string componentType = ComponentType.Components)
    {
        this.componentType = componentType;
        Init(type, typeInfo, identify);
    }

    public ComponentTypeAttribute(Type type, string identify = null, string componentType = ComponentType.Components)
    {
        this.componentType = componentType;
        Init(type, null, identify);
    }

    void Init(Type type, Type typeInfo = null, string identify = null)
    {
        this.type = type;
        this.typeInfo = typeInfo;
        this.identify = identify;
        if (this.identify == null) 
            this.identify = type.Name;
    }

    public JSONObject ToJson()
    {
        JSONObject data = new JSONObject();
        data.Add("assemblyName", type.Assembly.GetName().Name);
        data.Add("type", type.FullName);
        if (typeInfo != null)
        {
            Debug.Log(type.Assembly.GetName().FullName + " " + typeInfo.Assembly.GetName().FullName);
            if (type.Assembly.GetName().FullName != typeInfo.Assembly.GetName().FullName)
                data.Add("assemblyNameInfo", typeInfo.Assembly.GetName().Name);
            data.Add("typeInfo", typeInfo.FullName);
            data.Add("data", JSON.Parse(JsonUtility.ToJson(Activator.CreateInstance(typeInfo))));
        }
        JSONObject json = new JSONObject();
        json.Add(identify, data);
        return data;
    }
}

