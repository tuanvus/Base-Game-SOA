using LTA.Error;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LTA.NonEntity
{
    public class NonEntityHelper
    {
        static Dictionary<string, MutilNonEntityInfosData> dic_TypeData_NonEntityInfosData = new Dictionary<string, MutilNonEntityInfosData>();

        static Dictionary<string, MutilNonEntityInfoData> dic_TypeData_NonEntityInfoData = new Dictionary<string, MutilNonEntityInfoData>();

        public static MutilNonEntityInfosData GetNonEntityInfosData(string typeNonEntity)
        {
            try
            {
                if (!dic_TypeData_NonEntityInfosData.ContainsKey(typeNonEntity))
                {
                    dic_TypeData_NonEntityInfosData.Add(typeNonEntity, new MutilNonEntityInfosData(typeNonEntity));
                }
                return dic_TypeData_NonEntityInfosData[typeNonEntity];
            }
            catch (Exception ex)
            {
                throw new ErrorException("Error with TypeNonEntity : " + typeNonEntity + " : " + ex.Message);
            }
        }

        public static MutilNonEntityInfoData GetNonEntityInfoData(string typeNonEntity)
        {
            try
            {
                if (!dic_TypeData_NonEntityInfoData.ContainsKey(typeNonEntity))
                {
                    dic_TypeData_NonEntityInfoData.Add(typeNonEntity, new MutilNonEntityInfoData(typeNonEntity));
                }
                return dic_TypeData_NonEntityInfoData[typeNonEntity];
            }
            catch (Exception ex)
            {
                throw new ErrorException("Error with TypeNonEntity : " + typeNonEntity + " : " + ex.Message);
            }
        }

        public static NonEntityController AddNonEntityController(GameObject gameObject,string key,NonEntityInfo nonEntityInfo, Action<NonEntityController> editNonEntityController = null)
        {
            NonEntityController nonEntity = ComponentHelper.AddComponent(gameObject, nonEntityInfo) as NonEntityController;
            nonEntity.TypeData = key;
            nonEntity.NonEntityInfo = nonEntityInfo;
            if (editNonEntityController != null)
            {
                editNonEntityController(nonEntity);
            }
            return nonEntity;
        }
    }
}
