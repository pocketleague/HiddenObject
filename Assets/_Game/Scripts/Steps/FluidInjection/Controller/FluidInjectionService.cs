using System;
using System.Collections;
using System.Collections.Generic;
using Scripts.FluidInjection;
using Scripts.GameplayStates;
using UnityEngine;

namespace Scripts.FluidInjection
{
    public class FluidInjectionService : IFluidInjectionService, IGameplayState
    {
        public event Action OnBegin;
        public event Action OnEnd;

        public void Begin()
        {
          //  throw new NotImplementedException();
        }

        public void End()
        {
          //  throw new NotImplementedException();
        }
    }
}

