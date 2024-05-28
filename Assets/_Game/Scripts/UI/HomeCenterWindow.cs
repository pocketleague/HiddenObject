using Scripts.Stages;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using HomeCenter;
using Scripts.Core;

namespace Scripts.UI
{
    [RequireComponent(typeof( Window ) )]
    public class HomeCenterWindow : MonoBehaviour
    {

        [Inject] private IHomeCenterService _homeCenterService;
        [Inject] private IStateManagerService _stateManagerService;

        private Window _targetWindow;

        [SerializeField]
        private Button _btnStartGameCenter;

        public UnityEngine.Camera _cam;

        private void Awake()
        {
            _targetWindow = GetComponent<Window>();

            _homeCenterService.OnHomeCenterStarted  += Open;
            _homeCenterService.OnHomeCenterEnded    += Close;

            _btnStartGameCenter.onClick.AddListener(StartGameCenterClicked);


        }

        void OnCamSet(UnityEngine.Camera cam)
        {
            Debug.Log("camera set ho gaya");
            _cam = cam;
        }

        private void OnDestroy()
        {
            _homeCenterService.OnHomeCenterStarted  -= Open;
            _homeCenterService.OnHomeCenterEnded    -= Close;
        }

        void Open()
        {
            _targetWindow.Open();
        }

        void Close()
        {
            _targetWindow.Close();
        }

        void StartGameCenterClicked()
        {
            _stateManagerService.ChangeState(EState.LevelSelection);
        }
    }
}