using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using HomeCenter;
using GameplayCenter;
using RewardCenter;
using System;
using Scripts.Core.LevelSelection;

namespace Scripts.Core
{
    public class StateManagerService : IStateManagerService
    {
        public event Action<EState> OnStateStarted = delegate { };
        public event Action<EState> OnStateFinished = delegate { };

        private Dictionary<EState, IState> _states;

        public EState CurrentState { get; private set; }
        public EState PreviousState { get; private set; }

        private EState _pendingState;

        [Inject]
        private void Construct(IHomeCenterService homeCenterService, IGameplayCenterService gameplayService, IRewardCenterService rewardCenterService, ILevelSelectionService levelSelectionService)
        {
            _states = new Dictionary<EState, IState> {
                { EState.HomeCenter,        (IState)homeCenterService},
                { EState.LevelSelection,    (IState)levelSelectionService },
                { EState.GameplayCenter,    (IState)gameplayService },
                { EState.RewardCenter,      (IState)rewardCenterService }
            };

            new GameObject("StateManager").AddComponent<StateManagerSpawner>().Initialize(this);
        }

        public void SpawnStateCenter()
        {
            StartNextState(EState.HomeCenter);
        }

        public void StartNextState(EState state)
        {
            FinishCurrentState();

            CurrentState = state;

            _states[state].Begin();
            OnStateStarted.Invoke(state);
        }

        void FinishCurrentState()
        {
            if (CurrentState == EState.None)
                return;

            _states[CurrentState].End();
            OnStateFinished.Invoke(CurrentState);

            PreviousState = CurrentState;
            CurrentState = EState.None;
        }
    }
}