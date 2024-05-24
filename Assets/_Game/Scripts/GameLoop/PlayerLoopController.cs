using System;using UnityEngine;
namespace Scripts.GameLoop
{
    public class PlayerLoopController : MonoBehaviour
    {
        private PlayerLoop _playerLoop;
        
        public void Initialize( PlayerLoop playerLoop ) => _playerLoop = playerLoop;

        private void Update() => _playerLoop.DispatchUpdateTick( );

        private void FixedUpdate() => _playerLoop.DispatchFixedTick( );
        
    }
}
