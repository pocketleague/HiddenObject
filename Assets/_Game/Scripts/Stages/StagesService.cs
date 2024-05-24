using System;
using System.Collections.Generic;
using Scripts.GameLoop;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Scripts.Stages
{
    public class StagesService : IStagesService
    {
        public event Action<int, StageConfig> OnStageSpawned = delegate { };
        public event Action<int> OnStageStarted = delegate { };
        public event Action<float> OnStageProgressChanged = delegate { };
        public event Action<int, bool> OnStageFinished = delegate { };
        public event Action<int> OnStageSkipped = delegate { };

        private StagesSet _stagesSet;

        private bool _stageResolved;
        private bool _stageStarted;

        public string currentLoadedScene;

        private bool _resolvePending;
        private bool _resolveType;
        private float _resolvePendingTimer;

        public StageConfig CurrentStage { get; private set; }
        public int CurrentStageId { get; private set; }

        [Inject]
        private void Construct(StagesSet stagesSet, List<StageConfig> stages, IPlayerLoop playerLoop )
        {
            Application.targetFrameRate = 60;
            Input.multiTouchEnabled = false;

            _stagesSet = stagesSet;
            CurrentStageId = PlayerPrefs.GetInt(StagesPrefsStrings.CURRENT_STAGE, 0);

            new GameObject("StageSpawner").AddComponent<StageSpawner>().Initialize(this);

            playerLoop.OnUpdateTick += HandleDelayedStageFinished;
        }

        public void SpawnCurrentStage()
        {
            _stageResolved = false;
            _stageStarted = false;
            CurrentStage = _stagesSet.stages[CurrentStageId % _stagesSet.stages.Count];

            if (!string.IsNullOrEmpty(currentLoadedScene))
            {
                SceneManager.UnloadSceneAsync(currentLoadedScene);
            }

            OnStageSpawned.Invoke(CurrentStageId, CurrentStage);
            OnStageProgressChanged.Invoke(0f);

            if (_stagesSet.autostartFirstStage && CurrentStageId == 0)
            {
                StartCurrentStage();
            }

            //#if TTP_RATEUS
            //if ( CurrentStageId == 4 )
            //{
            //    Tabtale.TTPlugins.TTPRateUs.Popup( );
            //}
        }

        public void StartCurrentStage()
        {
            _stageStarted = true;
            OnStageStarted(CurrentStageId);
        }

        public void SkipCurrentStage()
        {
            OnStageSkipped.Invoke(CurrentStageId);

            CurrentStageId++;
            PlayerPrefs.SetInt(StagesPrefsStrings.CURRENT_STAGE, CurrentStageId);

            SpawnCurrentStage();
        }

        public void FinishStageSuccess(float delay = 0f)
        {
            Debug.Log("finish 1");

            if (_stageResolved || _resolvePending)
                return;

            Debug.Log("finish 2");

            if (delay > 0f)
            {
                Debug.Log("finish 3");

                _resolvePending = true;
                _resolveType = true;
                _resolvePendingTimer = delay;
                return;
            }

            Debug.Log("finish 4");

            _stageResolved = true;
            Debug.Log("finish 5");

            OnStageFinished.Invoke(CurrentStageId, true);

            CurrentStageId++;
            PlayerPrefs.SetInt(StagesPrefsStrings.CURRENT_STAGE, CurrentStageId);
        }


        public void FinishStageFailure(float delay = 0f)
        {
            if (_stageResolved || _resolvePending)
                return;

            if (delay > 0f)
            {
                _resolvePending = true;
                _resolveType = false;
                _resolvePendingTimer = delay;
                return;
            }

            _stageResolved = true;

            OnStageFinished.Invoke(CurrentStageId, false);
        }


        public void RestartPreviousStage()
        {
            CurrentStageId--;
            PlayerPrefs.SetInt(StagesPrefsStrings.CURRENT_STAGE, CurrentStageId);

            SpawnCurrentStage();
            StartCurrentStage();
        }


        private void HandleDelayedStageFinished()
        {
            if ( !_resolvePending )
                return;

            _resolvePendingTimer -= Time.deltaTime;

            if ( _resolvePendingTimer > 0f )
                return;

            _resolvePending = false;
            
            if ( _resolveType )
                FinishStageSuccess( );
            else
                FinishStageFailure( );    
        }
    }
}