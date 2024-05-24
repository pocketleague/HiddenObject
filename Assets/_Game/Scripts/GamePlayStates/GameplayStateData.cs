using System;
using UnityEngine;

namespace Scripts.GameplayStates
{
    [Serializable]
    public struct GameplayStateData
    {
        public EGameplayState state;
        public Sprite         sprite;
    }
}
