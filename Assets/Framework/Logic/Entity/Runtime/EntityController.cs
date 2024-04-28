using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTA.NonEntity;
using System;

namespace LTA.Entity
{


    public class EntityController : NonEntityController
    {
        MutilEntityComponentData mutilEntityComponentData;

        public override NonEntityInfo NonEntityInfo { 
            get => base.NonEntityInfo;
            set
            {
                this.name = value.name;
                mutilEntityComponentData = EntityHelper.GetEntityComponentData(typeData);
                base.NonEntityInfo = value;
            }
        }

        List<EntityController> entities = new List<EntityController>();


        protected override void AddInfos()
        {
            try
            {
                AddEntityComponentInfo(mutilEntityComponentData[NonEntityInfo.name][NonEntityInfo.level - 1], EditEntity);
            }
            catch (Exception ex)
            {
                Debug.LogWarning("Error nonEntityInfoData NonEntity :" + NonEntityInfo.ToString() + " : " + ex.Message);
            }
            base.AddInfos();
        }

        protected void AddEntityComponentInfo(Dictionary<string, ComponentInfo> dic_Key_Components, Action<string, EntityController> editEntity)
        {
            foreach (KeyValuePair<string, ComponentInfo> keyValue in dic_Key_Components)
            {
                string key = keyValue.Key;
                EntityHelper.AddEntityComponent(key,gameObject, keyValue.Value, (component) =>
                {
                    entities.Add(component);
                    editEntity(key,component);
                });
            }
        }


        protected virtual void EditEntity(string key, EntityController entity)
        {
            //if (!(entity is IOnUpLevel)) return;
            //onUpLevels.Add((IOnUpLevel)entity);
        }

        protected override void Clear()
        {
            foreach(EntityController entity in entities)
            {
                if (entity == null) continue;
                Destroy(entity.gameObject);
            }
            base.Clear();
        }
    }
}
