using Scripts.GameLoop;
using Scripts.Stages;
using UnityEngine;
using Zenject;
using HomeCenter;

namespace Scripts.Camera
{
    public class CameraService : ICameraService
    {
        private CameraConfig   _config;
        private IStagesService _stagesService;

        public CameraView        CameraView   { get; private set; }
        public CameraStateConfig CurrentState { get; private set; }

        [Inject]
        private void Construct( CameraConfig config, IStagesService stagesService, IPlayerLoop playerLoop, IHomeCenterService homeCenterService )
        {
            _config        = config;
            _stagesService = stagesService;
            CameraView     = Object.Instantiate( _config.cameraViewPrefab );

            _stagesService.OnStageStarted  += OnStageStarted;

            homeCenterService.OnHomeCenterStarted += HomeCenterCam;

            playerLoop.OnUpdateTick += HandleCameraState;
        }

        private void HandleCameraState()
        {
            if ( CameraView == null || CurrentState == null )
                return;

        //    CameraView.positionTransform.position      = Vector3.Lerp   ( CameraView.positionTransform.position       , CurrentState.movementOffset, Time.deltaTime * CurrentState.movementChangeSpeed );
        //    CameraView.rotationTransform.rotation      = Quaternion.Lerp( CameraView.rotationTransform.rotation       , Quaternion.Euler( CurrentState.rotation ) , Time.deltaTime * CurrentState.rotationChangeSpeed );
        //    CameraView.distanceTransform.localPosition = Mathf.Lerp     ( CameraView.distanceTransform.localPosition.z, -CurrentState.distance               , Time.deltaTime * CurrentState.distanceChangeSpeed ) * Vector3.forward;
        //    CameraView.cameraObject.fieldOfView        = Mathf.Lerp     ( CameraView.cameraObject.fieldOfView         , CurrentState.fieldOfView             , Time.deltaTime * CurrentState.clippingChangeSpeed );
        //    CameraView.cameraObject.nearClipPlane      = Mathf.Lerp     ( CameraView.cameraObject.nearClipPlane       , CurrentState.clipping.x              , Time.deltaTime * CurrentState.clippingChangeSpeed );
        //    CameraView.cameraObject.farClipPlane       = Mathf.Lerp     ( CameraView.cameraObject.farClipPlane        , CurrentState.clipping.y              , Time.deltaTime * CurrentState.clippingChangeSpeed );
        }

        private void OnStageStarted( int stageId )
        {
            ChangeCameraState( "Gameplay" );
        }

        private void HomeCenterCam()
        {
            ChangeCameraState( "MainMenu" );
        }

        public void ChangeCameraState( string stateId )
        {
            CurrentState = _config.GetCameraForState( stateId );
        }
    }
}
