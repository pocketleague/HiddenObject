using System;
using UnityEngine;
using Zenject;

namespace Scripts.GameLoop
{
    public class PlayerLoop : IPlayerLoop
    {
        public event Action OnUpdateTick = delegate{ };
        public event Action OnFixedTick  = delegate{ };

        [Inject]
        private void Construct()
        {
            new GameObject("PlayerLoop").AddComponent<PlayerLoopController>( ).Initialize( this );
        }

        internal void DispatchUpdateTick()
        {
            OnUpdateTick.Invoke( );   
        }
        
        internal void DispatchFixedTick()
        {
            OnFixedTick.Invoke( );
        }
    }
}
