using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Scripts.GameLoop;
using GameplayCenter;

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

            timeToSubtract += Time.deltaTime;

            if (timeToSubtract < 1)
                return;

            timeToSubtract = 0;
            ReduceTimer(1);
        }

        public void Penalty()
        {
            ReduceTimer(3);
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
            timeLeft = 30;
            OnTimerChanged.Invoke(timeLeft);

            timerOn = true;
        }

        void EndTimer()
        {
            timerOn = false;
        }
    }
}