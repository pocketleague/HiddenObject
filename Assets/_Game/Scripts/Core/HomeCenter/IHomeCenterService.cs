using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace HomeCenter
{
    public interface IHomeCenterService
    {
        event Action OnHomeCenterStarted;
        event Action OnHomeCenterEnded;
    }
}
