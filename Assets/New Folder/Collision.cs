using System.Collections;
using System.Collections.Generic;
using LTA.Target;
using UnityEngine;

public class Collision : MonoBehaviour
{
    [SerializeField] private FilterTargetController _filterTargetController;
    void Start()
    {
       var log = TargetController.GetTargets(_filterTargetController, 4);
       foreach (var item in log)
       {
           Debug.Log("namememe ="+item.name);
       }
    }

    
    void Update()
    {
        
    }
}
