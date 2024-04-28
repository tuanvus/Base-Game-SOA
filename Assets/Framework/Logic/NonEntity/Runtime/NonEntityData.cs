using LTA.Data;
using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LTA.NonEntity
{
    public class NonEnityInfosJSONData : ComponentsJSONData<NonEntityInfo>
    {
        protected override ComponentDataInfo GetComponentDataInfo(JSONObject json, string key)
        {
                ComponentDataInfo componentDataInfo = ComponentJSONData.GetComponentJSONData("NonEntities")[key];
                if (componentDataInfo == null) throw new Exception(String.Format(ERROR_COMPONENT_TYPE_MESSAGE, key, "NonEntities"));
                return componentDataInfo;
        }

        protected override NonEntityInfo Parse(JSONObject node, string key)
        {
            if (node["name"] == null) throw new JSONObjectMissingKeyException<NonEntityInfo>("name", node);
            NonEntityInfo nonEntityInfo = base.Parse(node, key);
            nonEntityInfo.name = node["name"];
            if (node["level"] == null)
            {
                DataHelper.LogWarning(this, String.Format(WARRNING_TYPE_MISSING_KEY_DATA, typeof(NonEntityInfo), "level"), DataHelper.GetDataName(this, index));
            }
            else
            { 
                nonEntityInfo.level = node["level"].AsInt;
            }
            return nonEntityInfo;
        }
    }

    public class NonEnityInfoJSONData : ComponentJSONData<NonEntityInfo>
    {
        protected override ComponentDataInfo GetComponentDataInfo(JSONObject json, string key)
        {
            ComponentDataInfo componentDataInfo = ComponentJSONData.GetComponentJSONData("NonEntities")[key];
            if (componentDataInfo == null) throw new Exception(String.Format(ERROR_COMPONENT_TYPE_MESSAGE, key, "NonEntities"));
            return componentDataInfo;
        }

        protected override NonEntityInfo GetValue(JSONNode node, string key)
        {
            if (node["name"] == null) throw new JSONObjectMissingKeyException<NonEntityInfo>("name", node);
            NonEntityInfo nonEntityInfo = base.GetValue(node, key);
            nonEntityInfo.name = node["name"];
            if (node["level"] == null)
            {
                DataHelper.LogWarning(this, String.Format(WARRNING_TYPE_MISSING_KEY_DATA, typeof(NonEntityInfo), "level"), DataHelper.GetDataName(this, index));
            }
            else
            {
                nonEntityInfo.level = node["level"].AsInt;
            }
            return nonEntityInfo;

        }
    }

    public class MutilNonEntityInfosData : MutilData<NonEnityInfosJSONData>
    {

        public Dictionary<string,NonEntityInfo[]> this[NonEntityInfo key]
        {
            get
            {
                return this[key.name][key.level - 1];
            }
        }


        public MutilNonEntityInfosData(string typeNonEntity)
        {
            DataInfo[] dataInfos = DataHelper.DataManager[typeNonEntity];
            LoadData(dataInfos);
        }
    }
    public class MutilNonEntityInfoData : MutilData<NonEnityInfoJSONData>
    {

        public Dictionary<string, NonEntityInfo> this[NonEntityInfo key]
        {
            get
            {
                return this[key.name][key.level - 1];
            }
        }


        public MutilNonEntityInfoData(string typeNonEntity)
        {
            DataInfo[] dataInfos = DataHelper.DataManager[typeNonEntity];
            LoadData(dataInfos);
        }
    }
}
