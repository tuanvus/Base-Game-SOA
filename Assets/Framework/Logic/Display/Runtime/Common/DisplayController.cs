using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTA.Entity;
using LTA.Data;
using System.Reflection.Emit;


namespace LTA.Display
{
    [ComponentType(typeof(DisplayController), typeof(DisplayInfo),"prefab")]
    public class DisplayController : BaseDisplayController<DisplayInfo>
    {
        protected GameObject instance;

        protected override GameObject Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = DisplayHelper.CreatePrefab(info.path);
                    instance.transform.SetParent(transform);
                }
                return instance;
            }
        }
    }
}
