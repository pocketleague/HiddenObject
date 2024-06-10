using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Scripts.Core.HomeCenter
{
    public interface IHomeCenterService
    {
        event Action OnHomeCenterStarted;
        event Action OnHomeCenterEnded;
    }
}
