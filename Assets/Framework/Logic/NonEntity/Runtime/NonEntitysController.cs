using LTA.NonEntity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonEntitysController<T> : MonoBehaviour where T : NonEntityController
{
    protected Dictionary<NonEntityInfo, T> dic_Type_NonEntity = new Dictionary<NonEntityInfo,T>(NonEntityInfo.EntityComparer);

    public T GetNonEntity(NonEntityInfo nonEntityInfo)
    {
        if (dic_Type_NonEntity.ContainsKey(nonEntityInfo)) return dic_Type_NonEntity[nonEntityInfo];
        return null;
    }

    public void AddNonEntity(T nonEntityController)
    {
        if (nonEntityController == null) return;
        NonEntityInfo entityInfo = nonEntityController.NonEntityInfo;
        if (!dic_Type_NonEntity.ContainsKey(entityInfo))
        {
            dic_Type_NonEntity.Add(entityInfo, nonEntityController);
        }
    }
    public void RemoveNonEntity(T nonEntityController)
    {
        if (nonEntityController == null) return;
        NonEntityInfo entityInfo = nonEntityController.NonEntityInfo;
        if (dic_Type_NonEntity.ContainsKey(entityInfo))
        {
            dic_Type_NonEntity.Remove(entityInfo);
        }
    }

    public void Clear()
    {

    } 
}
public class NonEntitysComponents<T> : ComponentsController where T : NonEntityController
{
    protected Dictionary<NonEntityInfo, T> dic_Type_NonEntity = new Dictionary<NonEntityInfo, T>(NonEntityInfo.EntityComparer);

    public T GetNonEntity(NonEntityInfo nonEntityInfo)
    {
        if (dic_Type_NonEntity.ContainsKey(nonEntityInfo)) return dic_Type_NonEntity[nonEntityInfo];
        return null;
    }

    public void AddNonEntity(T nonEntityController)
    {
        if (nonEntityController == null) return;
        NonEntityInfo entityInfo = nonEntityController.NonEntityInfo;
        if (!dic_Type_NonEntity.ContainsKey(entityInfo))
        {
            dic_Type_NonEntity.Add(entityInfo, nonEntityController);
        }
    }
    public void RemoveNonEntity(T nonEntityController)
    {
        if (nonEntityController == null) return;
        NonEntityInfo entityInfo = nonEntityController.NonEntityInfo;
        if (dic_Type_NonEntity.ContainsKey(entityInfo))
        {
            dic_Type_NonEntity.Remove(entityInfo);
        }
    }

}

public class NonEntitysHasOwnController<T> : NonEntitysController<T> where T : NonEntityController
{
    protected Dictionary<NonEntityInfo, List<MonoBehaviour>> dic_Type_ListOwnNonEntity = new Dictionary<NonEntityInfo, List<MonoBehaviour>>(NonEntityInfo.EntityComparer);

    public void AddNonEntity(T nonEntityController, MonoBehaviour ownUseFilter)
    {
        if (nonEntityController == null) return;
        NonEntityInfo entityInfo = nonEntityController.NonEntityInfo;
        if (!dic_Type_ListOwnNonEntity.ContainsKey(entityInfo))
        {
            dic_Type_ListOwnNonEntity.Add(entityInfo, new List<MonoBehaviour>());
        }
        if (!dic_Type_ListOwnNonEntity[entityInfo].Contains(ownUseFilter))
        {
            dic_Type_ListOwnNonEntity[entityInfo].Add(ownUseFilter);
        }
        AddNonEntity(nonEntityController);
    }
    public void RemoveNonEntity(T nonEntityController, MonoBehaviour ownUseFilter)
    {
        if (nonEntityController == null) return;
        NonEntityInfo entityInfo = nonEntityController.NonEntityInfo;
        if (dic_Type_ListOwnNonEntity.ContainsKey(entityInfo) && dic_Type_ListOwnNonEntity[entityInfo].Contains(ownUseFilter))
        {
            dic_Type_ListOwnNonEntity[entityInfo].Remove(ownUseFilter);
        }
        if (dic_Type_ListOwnNonEntity[entityInfo].Count == 0)
        {
            dic_Type_ListOwnNonEntity.Remove(entityInfo);
        }
        RemoveNonEntity(nonEntityController);
    }
}
