using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Scripts.GameLoop;
using Scripts.UI;
using TMPro;
using GameplayCenter;

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

            _timerService.OnTimerChanged += OnTimerChanged;
        }

        void Show()
        {
            _window.Open();
        }

        void Close()
        {
            _window.Close();
        }

        void OnTimerChanged(float time)
        {
            txt_timer.text = "" + time;
        }
    }
}