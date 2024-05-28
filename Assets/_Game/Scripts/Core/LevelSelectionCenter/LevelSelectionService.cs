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
            _stateManagerService.ChangeState(EState.GameplayCenter);
        }
    }
}