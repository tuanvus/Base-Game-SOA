using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rigibody2DMoveController : MoveController<MoveInfo>
{
    Rigidbody2D rigidbody2D;

    Rigidbody2D Rigidbody2D
    {
        get
        {
            if (rigidbody2D == null)
            {
                rigidbody2D = GetComponent<Rigidbody2D>();
            }
            return rigidbody2D;
        }
    }

    public override void Move(Vector3 direction)
    {
        if (isStop) return;
        this.direction = direction;
        Rigidbody2D.velocity = direction * info.speed * Time.fixedDeltaTime;
        base.Move(direction);
    }

    public override void Stand()
    {
        base.Stand();
        Rigidbody2D.velocity = Vector2.zero;
    }
}
