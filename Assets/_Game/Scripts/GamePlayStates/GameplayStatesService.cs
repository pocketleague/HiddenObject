using System;
using System.Collections.Generic;
using Scripts.GameLoop;
using Scripts.Stages;
using Scripts.FluidInjection;
using UnityEngine;
using Zenject;

namespace Scripts.GameplayStates
{
    public class GameplayStatesService : IGameplayStatesService
    {
        public event Action<EGameplayState> OnStatePending = delegate { };
        public event Action<EGameplayState> OnStateStarted = delegate { };
        public event Action<EGameplayState, bool> OnStateFinished = delegate { };
        public event Action OnAllStatesFinished = delegate { };

        private GameplayStatesConfig _config;
        private IStagesService _stagesService;

        private Dictionary<EGameplayState, IGameplayState> _gameplayStates;


        public int CurrentStateId { get; private set; }
        public EGameplayState CurrentState { get; private set; }
        public EGameplayState PreviousState { get; private set; }

        private EGameplayState _pendingState;

        private int _statesInStage;
        private float _nextStateTimer;


        [Inject]
        private void Construct(GameplayStatesConfig config, IStagesService stagesService, IPlayerLoop playerLoop, IFluidInjectionService fluidInjectionService)
        {
            _config = config;
            _stagesService = stagesService;

            _gameplayStates = new Dictionary<EGameplayState, IGameplayState> {
                { EGameplayState.FluidInjection, (IGameplayState)fluidInjectionService}
            };

            //customersService.OnCustomerSatDown += StartGameplayPhases;

            playerLoop.OnUpdateTick += HandleStateTransition;
            _stagesService.OnStageStarted += StartGameplayPhases;
        }

        public void WinCurrentState()
        {
            Debug.Log("CurrentState is " + CurrentState.ToString());
            if (CurrentState == EGameplayState.None)
                return;
            if (_pendingState != EGameplayState.None)
                return;

            FinishCurrentState(true);

            if (CurrentStateId + 1 < _statesInStage)
            {
                TransitionToNextState();
            }

        }

        public void FailCurrentState()
        {
            if (CurrentState == EGameplayState.None)
                return;
            if (_pendingState != EGameplayState.None)
                return;

            FinishCurrentState(false);
        }

        public float GetEndGameDelayAfterLastState() => _config.GetEndGameDelayForState(PreviousState);

        private void StartGameplayPhases(int stageIndex)
        {
            _statesInStage = _stagesService.CurrentStage.states.Count;
            CurrentStateId = -1;
            PreviousState = EGameplayState.None;
            CurrentState = EGameplayState.None;

            TransitionToNextState();
        }

        private void FinishGameplayPhases()
        {
            CurrentState = EGameplayState.None;

            OnAllStatesFinished.Invoke();

            _stagesService.FinishStageSuccess(_config.GetEndGameDelayForState(PreviousState));
        }

        private void StartCurrentState()
        {
            Debug.Log("Gameplay state Start :  " + _stagesService.CurrentStage.states[CurrentStateId].ToString());

            CurrentState = _stagesService.CurrentStage.states[CurrentStateId];

            _gameplayStates[CurrentState].Begin();

            OnStateStarted.Invoke(CurrentState);
        }

        private void FinishCurrentState(bool success)
        {
            if (CurrentState == EGameplayState.None)
                return;

            _gameplayStates[CurrentState].End();

            OnStateFinished.Invoke(CurrentState, success);

            PreviousState = CurrentState;
            CurrentState = EGameplayState.None;

            if (!success)
            {
                _stagesService.FinishStageFailure();
            }
            else if (CurrentStateId == _statesInStage - 1)
            {
                FinishGameplayPhases();
            }
        }

        private void TransitionToNextState()
        {
            if (CurrentStateId + 1 >= _statesInStage)
                return;

            _pendingState = _stagesService.CurrentStage.states[CurrentStateId + 1];
            _nextStateTimer = CurrentStateId == -1 ? _config.enterFirstStateDelay : _config.GetEnterDelayForState(_pendingState);

            OnStatePending.Invoke(_pendingState);
        }

        private void HandleStateTransition()
        {
            if (_pendingState == EGameplayState.None)
                return;

            _nextStateTimer -= Time.deltaTime;

            if (_nextStateTimer > 0)
                return;

            _pendingState = EGameplayState.None;

            StartNextState();
        }

        private void StartNextState()
        {
            CurrentStateId++;

            StartCurrentState();
        }

        private void AddHairTieDelay()
        {
            _nextStateTimer = _config.tyingHairDelay;
        }
    }
}
