using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEndChangeSlot
{
    void OnEndChangeSlot(ISlot currentSlot, ISlot pastSlot);
}
