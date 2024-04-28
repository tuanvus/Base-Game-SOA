using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDir : MonoBehaviour
{
    
    void Start()
    {
        
    }

    public void MoveUpdate()
    {
        transform.position += Vector3.Lerp(transform.position, Vector3.forward, 0.4f);
    }
}
