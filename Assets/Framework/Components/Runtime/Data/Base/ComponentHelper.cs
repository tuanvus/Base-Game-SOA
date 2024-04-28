using SimpleJSON;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using LTA.Error;
using LTA.Data;
public interface ISetInfo
{
    object Info
    {
        set;
    }
}

public interface ISetInfo<T> : ISetInfo
{
    T OwnInfo
    {
        get;
    }
}

public class ComponentType
{
    public const string Components = "Components";
    public const string NonEntities = "NonEntities";
    public const string Entities = "Entities";
}

public class ComponentHelper
{
    static Dictionary<string, MutilComponentData> dic_TypeData_ComponentData = new Dictionary<string, MutilComponentData>();
    public static MutilComponentData GetComponentData(string typeNonEntity)
    {
        try
        {
            if (!dic_TypeData_ComponentData.ContainsKey(typeNonEntity))
            {
                dic_TypeData_ComponentData.Add(typeNonEntity, new MutilComponentData(typeNonEntity));
            }
            return dic_TypeData_ComponentData[typeNonEntity];
        }
        catch (Exception ex)
        {
            throw new ErrorException("Error with TypeNonEntity : " + typeNonEntity + " : " + ex.Message);
        }
    }
    static Dictionary<string, MutilComponentsData> dic_TypeData_ComponentsData = new Dictionary<string, MutilComponentsData>();
    public static MutilComponentsData GetComponentsData(string typeNonEntity)
    {
        try
        {
            if (!dic_TypeData_ComponentsData.ContainsKey(typeNonEntity))
            {
                dic_TypeData_ComponentsData.Add(typeNonEntity, new MutilComponentsData(typeNonEntity));
            }
            return dic_TypeData_ComponentsData[typeNonEntity];
        }
        catch (Exception ex)
        {
            throw new ErrorException("Error with TypeNonEntity : " + typeNonEntity + " : " + ex.Message);
        }
    }
    public static Component AddComponent(GameObject gameObject, ComponentInfo componentInfo, Action<Component> editComponent = null)
    {
        Component component = gameObject.AddComponent(componentInfo.type);
        if (componentInfo.data == null)
        {
            if (editComponent != null)
                editComponent(component);
            return component;
        }
        ISetInfo setInfo = (ISetInfo)component;
        setInfo.Info = componentInfo.data;
        if (editComponent != null)
            editComponent(component);
        return component;
    }

    

}
