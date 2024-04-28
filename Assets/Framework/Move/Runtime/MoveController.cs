using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MoveInfo
{
    public float speed;
}

public class TypeMove
{
    public const string Move3D = "Move3D";
    public const string Move2D = "Move2D";
}

public class MoveController<T> : MonoBehaviour,ISetInfo,IMove where T : MoveInfo
{
    protected T info;
    protected bool isStop = false;

    public bool Stop
    {
        set
        {
            isStop = value;
        }
    }

    protected Vector3 direction;

    public Vector3 Direction
    {
        get
        {
            return direction;
        }
    }

    public object Info { 
        set
        {
            info = (T)value;
        }
    }

    public virtual void Move(Vector3 direction)
    {
        IOnMove[] onMoves = GetComponentsInChildren<IOnMove>();
        foreach(IOnMove onMove in onMoves)
        {
            onMove.OnMove(direction);
        }
    }

    public virtual void Stand()
    {
        this.direction = Vector3.zero;
    }
}

public interface IMove
{
    void Move(Vector3 direction);

    void Stand();
}
