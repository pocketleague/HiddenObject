using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Scripts.Stages;
using System;
using Scripts.Core;

namespace GameplayCenter
{
    public class GameplayCenterService : IGameplayCenterService, IState
    {
        public event Action OnGamePlayStarted = delegate { };
        public event Action OnGamePlayEnded = delegate { };

        private GameplayCenterConfig _config;

        [Inject]
        private void Construct(GameplayCenterConfig config)
        {
            _config             = config;
        }

        public void Begin()
        {
            OnGamePlayStarted.Invoke();
        }

        public void End()
        {
            OnGamePlayEnded.Invoke();
        }
    }
}
