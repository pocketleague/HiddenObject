using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Scripts.GameLoop;
using Scripts.Core.GameplayCenter;

namespace Scripts.Timer
{
    public class TimerService : ITimerService
    {
        public event Action<float> OnTimerChanged = delegate { };
        public event Action OnTimerEnded = delegate { };


        private TimerConfig _config;

        private bool timerOn;
        private float timeLeft;
        private float timeToSubtract;

        private float freezeTimeLeft;
        private bool isFreeze;

        [Inject]
        private void Construct(TimerConfig config, IPlayerLoop playerLoop, IGameplayCenterService gameplayCenterService)
        {
            _config = config;

            playerLoop.OnUpdateTick += MyUpdate;
            gameplayCenterService.OnGamePlayStarted +=  StartTimer;
            gameplayCenterService.OnGamePlayEnded   +=  EndTimer;

        }

        void MyUpdate()
        {
            if (!timerOn)
                return;

            HandleFreeze();
            HandleMainTimer();
        }

        void HandleMainTimer()
        {
            if (isFreeze)
                return;

            timeToSubtract += Time.deltaTime;

            if (timeToSubtract < 1)
                return;

            timeToSubtract = 0;
            ReduceTimer(1);
        }

        void HandleFreeze()
        {
            if (!isFreeze)
                return;

            freezeTimeLeft -= Time.deltaTime;

            if (freezeTimeLeft <= 0)
            {
                // Freeze time ended
                isFreeze = false;
            }
        }

        public void Penalty()
        {
            ReduceTimer(_config.time_penalty);
        }

        void ReduceTimer(float timeToSubtract)
        {
            timeLeft -= timeToSubtract;

            if (timeLeft <= 0)
            {
                timerOn = false;
                OnTimerEnded.Invoke();
            }

            OnTimerChanged.Invoke(timeLeft);
        }

        void StartTimer()
        {
            timeLeft = 90;
            OnTimerChanged.Invoke(timeLeft);

            timerOn = true;
        }

        void EndTimer()
        {
            timerOn = false;
        }

        public void Freeze()
        {
            isFreeze = true;
            freezeTimeLeft = 3;
        }
    }
}