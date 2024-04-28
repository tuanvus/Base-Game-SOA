using LTA.Error;
using LTA.NonEntity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LTA.Entity
{
    public class EntityHelper
    {
        static Dictionary<string, MutilEntityComponentData> dic_TypeData_EntityComponentData = new Dictionary<string, MutilEntityComponentData>();
        public static MutilEntityComponentData GetEntityComponentData(string typeNonEntity)
        {
            try
            {
                if (!dic_TypeData_EntityComponentData.ContainsKey(typeNonEntity))
                {
                    dic_TypeData_EntityComponentData.Add(typeNonEntity, new MutilEntityComponentData(typeNonEntity));
                }
                return dic_TypeData_EntityComponentData[typeNonEntity];
            }
            catch (Exception ex)
            {
                throw new ErrorException("Error with TypeNonEntity : " + typeNonEntity + " : " + ex.Message);
            }
        }
        //public static EntityController AddEntityController(GameObject gameObject, string key, NonEntityInfo nonEntityInfo, Action<NonEntityController> editNonEntityController = null)
        //{
        //    GameObject newGameObject = new GameObject(key);
        //    NonEntityController nonEntity = ComponentHelper.AddComponent(gameObject, nonEntityInfo) as NonEntityController;
        //    if (editNonEntityController != null)
        //    {
        //        editNonEntityController(nonEntity);
        //    }
        //    nonEntity.TypeData = key;
        //    nonEntity.NonEntityInfo = nonEntityInfo;
        //    return nonEntity;
        //}

        public static EntityController AddEntity(string type, GameObject gameObject, NonEntityInfo nonEntityInfo, Action<EntityController> editEnitty = null)
        {
            GameObject newGameObject = new GameObject(nonEntityInfo.name);
            if (gameObject != null)
            {
                newGameObject.transform.SetParent(gameObject.transform, false);
            }
            EntityController component = NonEntityHelper.AddNonEntityController(newGameObject,type, nonEntityInfo, (component) =>
            {
                editEnitty(component as EntityController);
            }) as EntityController;
            return component;
        }

        public static EntityController AddEntityComponent(string key,GameObject gameObject, ComponentInfo componentInfo, Action<EntityController> editEnitty = null)
        {
            GameObject newGameObject = new GameObject(key);
            if (gameObject != null)
            {
                newGameObject.transform.SetParent(gameObject.transform,false);
            }
            EntityController component = ComponentHelper.AddComponent(newGameObject, componentInfo, (component) =>
            {
                editEnitty(component as EntityController);
            }) as EntityController;
            return component;
        }
    }
}
