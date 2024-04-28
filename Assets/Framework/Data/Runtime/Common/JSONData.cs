using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTA.Data;
using SimpleJSON;
using LTA.Error;
using System;
using System.Reflection;
using MapDataExpection = LTA.Data.MapDataExpection<string>;

public class JSONHelper
{
    static Dictionary<string, JSONNode> dic_DataName_JSONNode = new Dictionary<string, JSONNode>();
    public static JSONNode GetJSONNode(DataInfo dataInfo)
    {
        if (!dic_DataName_JSONNode.ContainsKey(dataInfo.name))
        {
            dic_DataName_JSONNode.Add(dataInfo.name, JSON.Parse((string)dataInfo.data));
        }
        return dic_DataName_JSONNode[dataInfo.name];
    }
}

public abstract class JSONData<T> : Data<T>,ILoadData where T : JSONNode
{
    
    public void LoadData(DataInfo dataInfo)
    {
        Name = dataInfo.name;
        JSONNode json = JSONHelper.GetJSONNode(dataInfo);
        if (json == null) throw new DataExpection(this, "data is null");
        if (json["data"] == null) throw new DataExpection(this, "key = data is not exist");
        try
        {
            m_Data = ConvertData(json["data"]);
        }
        catch (Exception ex)
        {
            throw new DataExpection(this, ex.Message);
        }
    }

    protected abstract T ConvertData(JSONNode json);
}

public abstract class JSONObjectData<T> : JSONData<JSONObject>
{
    protected override JSONObject ConvertData(JSONNode json)
    {
        if (!json.IsObject) throw new DataExpection(this, "data is not JSONObject");
        return json.AsObject;
    }
    public T this[string key]
    {
        get
        {
            if (m_Data[key] == null) throw new MapDataExpection(this, key, "key is not found");
            return ConvertObjectData(m_Data[key]);
        }
    }
    protected abstract T ConvertObjectData(JSONNode json);
}


public abstract class JSONArrayData<T> : JSONData<JSONArray>,IEnumerable<T>
{

    public T this[int index]
    {
        get
        {
            try
            {
                return ConvertArrayData(m_Data[index], index);
            }
            catch (Exception ex)
            {
                throw new ArrayDataExpection(this, index, ex.Message);
            }
        }
    }

    protected override JSONArray ConvertData(JSONNode json)
    {
        if (!json.IsArray) throw new JSONDataIsNotArray(json);
        return json.AsArray;
    }
    protected abstract T ConvertArrayData(JSONNode json,int index);

    public IEnumerator<T> GetEnumerator()
    {
        return GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        for (int i = 0; i < m_Data.Count; i++)
        {
            JSONNode node = m_Data[i];
            yield return ConvertArrayData(node,i);
        }
    }
}

public abstract class JSONArrayKeyData<V> : JSONArrayData<V>
{
    public abstract string Key { get; }
    protected int index;
    const string ERROR_COMPONENT_MESSAGE = "JSONData in key = {0} : ";
    protected override V ConvertArrayData(JSONNode json, int index)
    {
        if (!json.IsObject) throw new JSONDataIsNotObject(json);
        this.index = index;
        if (json[Key] == null) throw new Exception("JSONData in key =" + Key + " is null");
        try
        {
            return GetValue(json, Key);
        }
        catch (Exception ex)
        {
            throw new Exception(String.Format(ERROR_COMPONENT_MESSAGE, Key) + ex.Message);
        }
    }
    protected abstract V GetValue(JSONNode node, string key);
}

public abstract class JSONArrayKeyArrayData<V> : JSONArrayKeyData<V[]>
{
    const string ERROR_COMPONENT_MESSAGE = "JSONData in key = {0} index = {1} : ";
    protected override V[] GetValue(JSONNode node, string key)
    {
        if (!node.IsArray) throw new JSONDataIsNotArray(node);
        JSONArray array = node.AsArray;
        List<V> listValue = new List<V>();
        for (int i = 0; i < array.Count; i++)
        {
            try
            {
                listValue.Add(Parse(array[i]));
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format(ERROR_COMPONENT_MESSAGE, key, i) + ex.Message);
            }
        }
        return listValue.ToArray();
    }

    protected V GetValueInArray(JSONNode node)
    {
        if (node == null) throw new Exception("JSONData is null");
        return Parse(node);
    }

    protected abstract V Parse(JSONNode node);
}

public abstract class JSONArrayDicData<T,V> : JSONArrayData<T> where T : IDictionary<string,V>,new()
{
    protected const string WARRNING_TYPE_MISSING_KEY_DATA = "Type is {0} is missing {1}";
    protected int index;
    const string ERROR_COMPONENT_MESSAGE = "JSONData in key = {0} : ";
    protected override T ConvertArrayData(JSONNode json, int index)
    {
        if (!json.IsObject) throw new JSONDataIsNotObject(json);
        this.index = index;
        T dicData = new T();
        foreach (string key in json.Keys)
        {
            if (json[key] == null) throw new Exception("JSONData in key =" + key + " is null");
            try
            {
                dicData.Add(key, GetValue(json[key], key));
            } catch(Exception ex)
            {
                DataHelper.LogWarning(this,ex.Message, DataHelper.GetDataName(this, index) + " " + String.Format(ERROR_COMPONENT_MESSAGE, key));
            }
        }
        return dicData;
    }
    protected abstract V GetValue(JSONNode node,string key);
}

public abstract class JSONArrayDicArrayData<T, V> : JSONArrayDicData<T, V[]> where T : IDictionary<string, V[]>, new()
{
    
    const string ERROR_COMPONENT_MESSAGE = "JSONData in key = {0} index = {1} : ";
    protected override V[] GetValue(JSONNode node, string key)
    {
        if (!node.IsArray) throw new JSONDataIsNotArray(node);
        JSONArray array = node.AsArray;
        List<V> listValue = new List<V>();
        for (int i = 0; i < array.Count; i++)
        {
            try
            {
                listValue.Add(Parse(array[i],key));
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format(ERROR_COMPONENT_MESSAGE, key, i) + ex.Message);
            }
        }
        return listValue.ToArray();
    }

    protected abstract V Parse(JSONNode node,string key);
}

public abstract class JSONArrayDicArrayJSONObjectData<T, V> : JSONArrayDicArrayData<T, V> where T : IDictionary<string, V[]>, new()
{
    
    protected override V Parse(JSONNode node, string key)
    {
        if (!node.IsObject) throw new JSONDataIsNotObject(node);
        return Parse(node.AsObject,key);
    }

    protected abstract V Parse(JSONObject node,string key);
}