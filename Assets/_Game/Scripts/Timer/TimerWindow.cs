using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Scripts.GameLoop;
using Scripts.UI;
using TMPro;
using Scripts.Core.GameplayCenter;

namespace Scripts.Timer
{
    public class TimerWindow : MonoBehaviour
    {
        private ITimerService _timerService;

        private Window _window;

        [SerializeField] private TextMeshProUGUI txt_timer;

        [Inject]
        private void Construct(IGameplayCenterService gameplayCenterService, ITimerService timerService, IPlayerLoop playerLoop)
        {
            _window = GetComponent<Window>();

            _timerService = timerService;

            gameplayCenterService.OnGamePlayStarted += Show;
            gameplayCenterService.OnGamePlayEnded += Close;

            _timerService.OnTimerChanged += DisplayTime;
        }

        void Show()
        {
            _window.Open();
        }

        void Close()
        {
            _window.Close();
        }

        void DisplayTime(float timeToDisplay)
        {
            float minutes = Mathf.FloorToInt(timeToDisplay / 60);
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);

            txt_timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}