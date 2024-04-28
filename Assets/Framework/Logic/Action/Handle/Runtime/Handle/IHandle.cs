using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace LTA.Handle
{
    public interface IHandle
    {
        void Handle();

        Action<IHandle> EndHandle { set; }
    }
}
