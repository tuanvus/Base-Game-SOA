using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IOnChangeDirection
{
    void OnChangeDirection(Vector3 direction);
}

public interface IOnChangeValue
{
    void OnChangeValue(bool newValue);
}

public class GlobalInput
{
    private static Dictionary<string, Vector3> dic_Name_Direction = new Dictionary<string, Vector3>();
    private static Dictionary<string,List<IOnChangeDirection>> dic_Name_OnChangeDirection = new Dictionary<string,List<IOnChangeDirection>>();

    public static void AddOnChangeDirection(string nameDirection,IOnChangeDirection onChangeDirection)
    {
        if (!dic_Name_OnChangeDirection.ContainsKey(nameDirection))
        {
            dic_Name_OnChangeDirection.Add(nameDirection, new List<IOnChangeDirection>());
        }
        List<IOnChangeDirection> list = dic_Name_OnChangeDirection[nameDirection];
        if (list.Contains(onChangeDirection)) return;
        list.Add(onChangeDirection);
    }
    public static void RemoveOnChangeDirection(string nameDirection, IOnChangeDirection onChangeDirection)
    {
        if (!dic_Name_OnChangeDirection.ContainsKey(nameDirection)) return;
        List<IOnChangeDirection> list = dic_Name_OnChangeDirection[nameDirection];
        if (!list.Contains(onChangeDirection)) return;
        list.Remove(onChangeDirection);
    }

    public static void ChangeDirection(string nameDirection,Vector3 newDirection)
    {
        if (!dic_Name_Direction.ContainsKey(nameDirection))
        {
            dic_Name_Direction.Add(nameDirection, newDirection);
        }
        else
        {
            dic_Name_Direction[nameDirection] = newDirection;
        }
        if (!dic_Name_OnChangeDirection.ContainsKey(nameDirection)) return;
        List<IOnChangeDirection> list = dic_Name_OnChangeDirection[nameDirection];
        foreach(IOnChangeDirection onChangeDirection in list)
        {
            onChangeDirection.OnChangeDirection(newDirection);
        }
    }

    public static Vector3 GetDirection(string nameDirection)
    {
        if (!dic_Name_Direction.ContainsKey(nameDirection))
        {
            dic_Name_Direction.Add(nameDirection, Vector3.zero);
        }
        return dic_Name_Direction[nameDirection];
    }

    private static Dictionary<string,bool> dic_Key_Bool = new Dictionary<string,bool>();
    private static Dictionary<string, List<IOnChangeValue>> dic_Key_OnChangeValue = new Dictionary<string, List<IOnChangeValue>>();

    public static bool GetValue(string key)
    {
        if (!dic_Key_Bool.ContainsKey(key))
        {
            dic_Key_Bool.Add(key, false);
        }
        return dic_Key_Bool[key];
    }

    public static void SetValue(string key,bool newValue)
    {
        if (!dic_Key_Bool.ContainsKey(key))
        {
            dic_Key_Bool.Add(key,newValue);
        }
    }
}
