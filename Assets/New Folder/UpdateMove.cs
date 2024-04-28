using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


[Serializable]
public class PosInfoEntity
{
    public int id;
    public Vector3 pos;
    public List<int> collision;

    public PosInfoEntity(int id, Vector3 p,List<int> collision)
    {
        this.id = id;
        pos = p;
        this.collision = collision;
    }
    
}


public class UpdateMove : MonoBehaviour
{

    [SerializeField] private List<Transform> objMove;
    
    public  List<PosInfoEntity> dicEntity;
    
    
    
  public  void StartThread()
    {
   
        
        Thread updateMove = new Thread(Movement);
        updateMove.Start();
        
    }

    void Movement()
    {
        while (true)
        {
            
            //movement
            foreach (var entity1 in dicEntity)
            {
                entity1.pos += new Vector3(0, 0, 0.000001f);
                
                // collision
                foreach (var entity2 in dicEntity)
                {
                    if (entity1 != entity2) // Không so sánh với chính nó
                    {
                        float distance = Vector3.Distance(entity1.pos, entity2.pos);
                        if (distance < 0.2f)
                        {
                            if (!entity1.collision.Contains(entity2.id))
                            {
                                entity1.collision.Add(entity2.id);
                            }
                        }
                        else
                        {
                            if (entity1.collision.Contains(entity2.id))
                            {
                                entity1.collision.Remove(entity2.id);
                            }
                        }
                    }
                }
            }
        
            
            
        }
    }

   
}
