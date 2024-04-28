using System.Collections;
using System.Collections.Generic;
using LTA.Target;
using UnityEngine;

public class UpdateDataTf : MonoBehaviour
{
    [SerializeField] private UpdateMove _updateMove;
    public List<EntityData> objData;

    public bool update = false;
    void Start()
    {
        
    }

    EntityData GetByID(int id)
    {
        return objData.Find(x => x.id == id);
    }
    
    void Update()
    {
        if (!update) return;
    
        foreach (var item in _updateMove.dicEntity)
        {

           var ob= GetByID(item.id);
           ob.transform.position = item.pos;
           foreach (var i in item.collision)
           {
               var obaaa= GetByID(i);
               
               obaaa.transform.localScale =Vector3.one/2;

           }

        }
        
    }
}
