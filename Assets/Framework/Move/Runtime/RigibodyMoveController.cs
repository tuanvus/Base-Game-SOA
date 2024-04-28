using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RigibodyMoveInfo : MoveInfo
{
    
}

[ComponentType(typeof(RigibodyMoveController), typeof(RigibodyMoveInfo), "rigid3dMove")]
public class RigibodyMoveController : MoveController<RigibodyMoveInfo>
{
    Rigidbody rigidbody3D;


    Rigidbody Rigidbody3D
    {
        get
        {
            if (rigidbody3D == null)
            {
                rigidbody3D = GetComponent<Rigidbody>();
                if (rigidbody3D == null)
                {
                    rigidbody3D = gameObject.AddComponent<Rigidbody>();
                }
                rigidbody3D.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
                //rigidbody3D.useGravity = false;
            }
            return rigidbody3D;
        }
    }


    public override void Move(Vector3 direction)
    {
        if (isStop) return;
        this.direction = direction;
        Rigidbody3D.velocity = direction * info.speed * Time.fixedDeltaTime + new Vector3(0,Rigidbody3D.velocity.y,0);
    }

    public override void Stand()
    {
        base.Stand();
        Rigidbody3D.velocity = new Vector3(0, Rigidbody3D.velocity.y, 0);
    }

    private void OnDestroy()
    {
        Destroy(rigidbody3D);
    }
}
