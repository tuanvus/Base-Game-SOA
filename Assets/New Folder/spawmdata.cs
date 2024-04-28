using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawmdata : MonoBehaviour
{
    [SerializeField] private UpdateMove _updateMove;
    [SerializeField] private UpdateDataTf _updateDataTf;

    [SerializeField] private EntityData prefab;
    
    void Start()
    {
        _updateMove.dicEntity = new List<PosInfoEntity>();
        _updateDataTf.objData = new List<EntityData>();
 
        for (int i = 0; i < 5; i++)
        {
           var a = Instantiate(prefab);
           a.id = i + 1;
           a.transform.position = new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1));
           _updateMove.dicEntity.Add(new PosInfoEntity(i+1, a.transform.position,new List<int>()));
           _updateDataTf.objData.Add(a);
        }
        _updateMove.StartThread();
        _updateDataTf.update = true;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
