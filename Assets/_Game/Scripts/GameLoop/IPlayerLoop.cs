using System;

namespace Scripts.GameLoop
{
    public interface IPlayerLoop
    {
        event Action OnUpdateTick;
        event Action OnFixedTick;
    }
}
