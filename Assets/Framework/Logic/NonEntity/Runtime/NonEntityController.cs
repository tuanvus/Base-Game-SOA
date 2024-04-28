using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTA.Error;
namespace LTA.NonEntity
{

    [System.Serializable]
    public class NonEntityInfo : ComponentInfo
    {
        public static IEqualityComparer<NonEntityInfo> EntityComparer = new NonEntityInfoComparer();
        public string name;
        public int level = 1;

        public override string ToString()
        {
            return "name : " + name + " level : " + level; 
        }
        public override bool Equals(object obj)
        {
            bool result = base.Equals(obj);
            if (!(obj is NonEntityInfo)) return false;
            NonEntityInfo info2 = (NonEntityInfo)obj;
            return result && name == info2.name && level == info2.level;
        }
    }

    public class NonEntityInfoComparer : IEqualityComparer<NonEntityInfo>
    {

        public bool Equals(NonEntityInfo x, NonEntityInfo y)
        {
            if (y is null) return y is null;
            return x.name == y.name && x.level == y.level;
        }

        public int GetHashCode(NonEntityInfo obj)
        {
            return 1;
        }
    }


    public class NonEntityController : ComponentsController
    {
        protected string typeData = "Unknown";

        public string TypeData
        {
            set
            {
                typeData = value;
            }
        }

        List<NonEntityController> nonEntities = new List<NonEntityController>();

        [SerializeField]
        NonEntityInfo nonEntityInfo;

        MutilComponentData componentData;
        MutilComponentsData componentDatas;
        MutilNonEntityInfosData nonEntityInfosData;
        MutilNonEntityInfoData nonEntityInfoData;
        public virtual NonEntityInfo NonEntityInfo
        {
            set
            {
                componentData       = ComponentHelper.GetComponentData(typeData);
                componentDatas      = ComponentHelper.GetComponentsData(typeData);
                nonEntityInfosData   = NonEntityHelper.GetNonEntityInfosData(typeData);
                nonEntityInfoData = NonEntityHelper.GetNonEntityInfoData(typeData);
                nonEntityInfo = value;
                Level = nonEntityInfo.level;
            }
            get
            {
                return nonEntityInfo;
            }
        }

        public int Level
        {
            get
            {
                return nonEntityInfo.level;
            }
            set
            {
                nonEntityInfo.level = value;
                Clear();
                AddInfos();
            }
        }

        protected NonEntityController GetNonEntity(NonEntityInfo nonEntityInfo)
        {
            foreach(NonEntityController nonEntity in nonEntities)
            {
                if (nonEntityInfo.Equals(nonEntity.NonEntityInfo))
                {
                    return nonEntity;
                }
            }
            return null;
        }

        protected virtual void AddInfos()
        {
            try
            {
                Dictionary<string, NonEntityInfo[]> dic_Key_NonEntityInfos = nonEntityInfosData[nonEntityInfo];
                foreach (KeyValuePair<string, NonEntityInfo[]> keyValue in dic_Key_NonEntityInfos)
                {
                    string key = keyValue.Key;
                    AddNonEntityInfos(key, keyValue.Value, (nonEntityController) =>
                    {
                        EditNonEntityController(key, nonEntityController);
                        nonEntities.Add(nonEntityController);
                    });
                }
                Dictionary<string, NonEntityInfo> dic_Key_NonEntityInfo = nonEntityInfoData[nonEntityInfo];
                foreach (KeyValuePair<string, NonEntityInfo> keyValue in dic_Key_NonEntityInfo)
                {
                    string key = keyValue.Key;
                    AddNonEntityInfo(key, keyValue.Value, (nonEntityController) =>
                    {
                        EditNonEntityController(key, nonEntityController);
                        nonEntities.Add(nonEntityController);
                    });
                }
            }
            catch (Exception ex)
            {
                Debug.LogWarning("Error nonEntityInfoData NonEntity :" + nonEntityInfo.ToString() + " : " + ex.Message);
            }

            try
            {
                AddComponentInfo(componentData[nonEntityInfo.name][nonEntityInfo.level - 1], EditComponent);
                AddComponentInfos(componentDatas[nonEntityInfo.name][nonEntityInfo.level - 1], EditComponent);
            }
            catch (Exception ex)
            {
                Debug.LogWarning("Error nonEntityData NonEntity :" + nonEntityInfo.ToString() + " : " + ex.Message);
            }
        }

        protected void AddNonEntityInfos(string key,NonEntityInfo[] nonEntityInfos, Action<NonEntityController> editNonEntityController)
        {
            for (int i = 0; i < nonEntityInfos.Length; i++)
            {
                AddNonEntityInfo(key, nonEntityInfos[i],editNonEntityController);
            }
        }

        protected virtual void AddNonEntityInfo(string key, NonEntityInfo nonEntityInfo, Action<NonEntityController> editNonEntityController)
        {
            NonEntityHelper.AddNonEntityController(gameObject, key, nonEntityInfo, editNonEntityController);
        }

        protected virtual void EditComponent(string key, Component component)
        {
            
        }

        protected virtual void EditNonEntityController(string key, NonEntityController nonEntityController)
        {
            
        }

        protected override void Clear()
        {
            foreach (NonEntityController nonEntity in nonEntities)
            {
                if (nonEntity == null) continue;
                Destroy(nonEntity);
            }
            nonEntities.Clear();
            base.Clear();
        }

    }

}
