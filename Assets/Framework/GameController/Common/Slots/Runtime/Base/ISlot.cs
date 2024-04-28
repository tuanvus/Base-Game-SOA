using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISlot
{
    public bool IsMoving { set; get; }

    public abstract Vector3 Pos { get; }

    public void ReturnSlot();

    public bool IsFree { get; }

    public void Add(Transform target);

    public void Remove(Transform target);

    public bool Contains(Transform target);
}
