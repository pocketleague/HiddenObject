using System;
using Scripts.FluidInjection;
using Scripts.GameplayStates;

namespace Scripts.Squishing
{
    public class SquishingService : ISquishingService, IGameplayState
    {
        public event Action OnBegin;
        public event Action OnEnd;

        public void Begin()
        {
            throw new NotImplementedException();
        }

        public void End()
        {
            throw new NotImplementedException();
        }
    }
}
