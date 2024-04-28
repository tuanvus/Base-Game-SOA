using LTA.Data;
using SimpleJSON;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ComponentsJSONData<Data> : JSONArrayDicArrayJSONObjectData<Dictionary<string,Data[]>,Data>,IDataFile where Data : ComponentInfo,new()
{

    protected const string ERROR_COMPONENT_TYPE_MESSAGE = "Component Name is {0} is not exists in Data/{1}.json \n please use Attribute [ComponentType(Type type,Type typeInfo,string identify)] with this Component";
    const string WARRNING_COMPONENT_MISSING_KEY_DATA = "Component Type is {0} with TypeInfo is {1} is missing data \n data is {2}";
    public string Extension => "json";
    protected virtual ComponentDataInfo GetComponentDataInfo(JSONObject json,string key)
    {
        if (json["componentName"] == null) throw new JSONObjectMissingKeyException<ComponentInfo>("componentName", json);
        string componentName = json["componentName"];
        ComponentDataInfo componentDataInfo = ComponentJSONData.GetComponentJSONData(key)[componentName];
        if (componentDataInfo == null) throw new Exception(String.Format(ERROR_COMPONENT_TYPE_MESSAGE, json["componentName"]));
        return componentDataInfo;
    }

    protected override Data Parse(JSONObject json, string key)
    {
        ComponentDataInfo componentDataInfo = GetComponentDataInfo(json, key);
        Data componentInfo = new Data();
        componentInfo.type = componentDataInfo.type;
        if (componentDataInfo.typeInfo == null) return componentInfo;
        if (json["data"] == null) 
        {
            throw new Exception(String.Format(WARRNING_COMPONENT_MISSING_KEY_DATA, componentInfo.type.Name, componentDataInfo.typeInfo.Name, json.ToString()));
        }
        componentInfo.data = JsonUtility.FromJson(json["data"].ToString(), componentDataInfo.typeInfo);
        return componentInfo;
    }

}

public class ComponentJSONData<Data> : JSONArrayDicData<Dictionary<string,Data>, Data>, IDataFile where Data : ComponentInfo, new()
{

    protected const string ERROR_COMPONENT_TYPE_MESSAGE = "Component Name is {0} is not exists in Data/{1}.json \n please use Attribute [ComponentType(Type type,Type typeInfo,string identify)] with this Component";
    const string WARRNING_COMPONENT_MISSING_KEY_DATA = "Component Type is {0} with TypeInfo is {1} is missing data \n data is {2}";
    public string Extension => "json";

    protected virtual string FileName { get => "Components"; }

    protected virtual ComponentDataInfo GetComponentDataInfo(JSONObject json, string key)
    {
        ComponentDataInfo componentDataInfo = ComponentJSONData.GetComponentJSONData(FileName)[key];
        if (componentDataInfo == null) throw new Exception(String.Format(ERROR_COMPONENT_TYPE_MESSAGE, key));
        return componentDataInfo;
    }

    protected override Data GetValue(JSONNode json, string key)
    {
        if (!json.IsObject) throw new JSONDataIsNotObject(json);
        ComponentDataInfo componentDataInfo = GetComponentDataInfo(json.AsObject,key);
        Data componentInfo = new Data();
        componentInfo.type = componentDataInfo.type;
        if (componentDataInfo.typeInfo == null) return componentInfo;
        if (json == null)
        {
            throw new Exception(String.Format(WARRNING_COMPONENT_MISSING_KEY_DATA, componentInfo.type.Name, componentDataInfo.typeInfo.Name, json.ToString()));
        }
        componentInfo.data = JsonUtility.FromJson(json.ToString(), componentDataInfo.typeInfo);
        return componentInfo;
    }

}

public class ComponentDataInfo
{
    public Type type;
    public Type typeInfo;
    public ComponentDataInfo(Type type,Type typeInfo = null)
    {
        this.type = type;
        this.typeInfo = typeInfo;
    }
}

public class ComponentJSONData : MapData<string, ComponentDataInfo>
{
    static Dictionary<string,ComponentJSONData> dic_Key_ComponentJSONData = new Dictionary<string, ComponentJSONData>();
    public static ComponentJSONData GetComponentJSONData(string key)
    {
        if (!dic_Key_ComponentJSONData.ContainsKey(key))
        {
            dic_Key_ComponentJSONData.Add(key, new ComponentJSONData("Data/" + key));
        }
        return dic_Key_ComponentJSONData[key];
    }
    const string ERROR_COMPONENT_TYPE_MESSAGE = "Assembly {0} can not load Type is {1} \n data is {2}";

    public ComponentJSONData(string path)
    {
        TextAsset text = Resources.Load<TextAsset>(path);
        JSONNode data = JSON.Parse(text.text)["data"];
        foreach(string key in data.Keys)
        {
            m_Data.Add(key,Parse(data[key].AsObject));
        }
    }

    protected ComponentDataInfo Parse(JSONObject json)
    {
        Assembly assembly = Assembly.Load("Assembly-CSharp");
        if (json["assemblyName"] != null)
        {
            assembly = Assembly.Load(json["assemblyName"]);
        }
        if (json["type"] == null) throw new JSONObjectMissingKeyException<ComponentInfo>("type", json);
        Type type = assembly.GetType(json["type"]);
        if (type == null) throw new Exception(String.Format(ERROR_COMPONENT_TYPE_MESSAGE, assembly.GetName().Name, json["type"], json.ToString()));
        if (json["typeInfo"] == null) return new ComponentDataInfo(type);
        if (json["assemblyNameInfo"] != null)
        {
            assembly = Assembly.Load(json["assemblyNameInfo"]);
        }
        Type typeInfo = assembly.GetType(json["typeInfo"]);
        if (typeInfo == null) throw new Exception(String.Format(ERROR_COMPONENT_TYPE_MESSAGE, assembly.GetName().Name, json["typeInfo"], json.ToString()));
        return new ComponentDataInfo(type,typeInfo);
    }
}

public class MutilComponentsData : MutilData<ComponentsJSONData<ComponentInfo>>
{

    public MutilComponentsData(string type)
    {
        DataInfo[] dataInfos = DataHelper.DataManager[type];
        LoadData(dataInfos);
    }
}

public class MutilComponentData : MutilData<ComponentJSONData<ComponentInfo>>
{

    public MutilComponentData(string type)
    {
        DataInfo[] dataInfos = DataHelper.DataManager[type];
        LoadData(dataInfos);
    }
}