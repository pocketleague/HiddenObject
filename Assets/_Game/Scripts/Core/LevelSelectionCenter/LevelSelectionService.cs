using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Scripts.Core.LevelSelection
{
    public class LevelSelectionService : ILevelSelectionService, IState
    {
        public event Action OnLevelSelectionStarted = delegate { };
        public event Action OnLevelSelectionEnded = delegate { };

        [Inject] private IStateManagerService _stateManagerService;

        private LevelSelectionConfig _config;
        public  int currentLevel { get; private set; }

        public LevelConfig CurrentLevelConfig { get; private set; }

        [Inject]
        private void Construct(LevelSelectionConfig config)
        {
            _config = config;
        }

        public void Begin()
        {
            OnLevelSelectionStarted.Invoke();
        }

        public void End()
        {
            OnLevelSelectionEnded.Invoke();
        }

        public void SelectLevel(int index)
        {
            if(index >= _config.levelConfigs.Length)
            {
                index = 0;
            }

            CurrentLevelConfig = GetLevelAtIndex(index);
            currentLevel = index;
            _stateManagerService.ChangeState(EState.GameplayCenter);
        }

        LevelConfig GetLevelAtIndex(int index)
        {
            return _config.levelConfigs[index];
        }
    }
}