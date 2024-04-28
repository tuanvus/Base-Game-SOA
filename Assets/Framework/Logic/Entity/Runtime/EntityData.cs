using LTA.Data;
using LTA.NonEntity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LTA.Entity
{
    public class EntityComponentJSONData : ComponentJSONData<ComponentInfo>
    {
        protected override string FileName => "Entities";
    }

    public class MutilEntityComponentData : MutilData<EntityComponentJSONData>
    {
        public MutilEntityComponentData(string typeEntity)
        {
            DataInfo[] dataInfos = DataHelper.DataManager[typeEntity];
            LoadData(dataInfos);
        }
    }
}
