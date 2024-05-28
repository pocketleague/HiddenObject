using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Scripts.Stages;
using System;
using Scripts.Core;
using Scripts.PlayerControl;
using Scripts.Camera;

namespace GameplayCenter
{
    public class GameplayCenterService : IGameplayCenterService, IState
    {
        public event Action OnGamePlayStarted = delegate { };
        public event Action OnGamePlayEnded = delegate { };

        private GameplayCenterConfig _config;

        private Camera mainCam;

        [Inject]
        private void Construct(GameplayCenterConfig config, IPlayerControlService playerControlService, ICameraService cameraService)
        {
            _config             = config;

            playerControlService.ClickModule.OnMouseDown += OnMouseDown;

            mainCam = cameraService.CameraView.cameraObject;
        }

        public void Begin()
        {
            OnGamePlayStarted.Invoke();
        }

        public void End()
        {
            OnGamePlayEnded.Invoke();
        }

        void OnMouseDown(Vector3 pos)
        {
            Vector3 worldPosition = GetWorldPositionOnPlane(pos, 0f);
            Debug.Log("click position " + worldPosition);
        }

        Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z)
        {
            Ray ray = mainCam.ScreenPointToRay(screenPosition);
            Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, z));
            float distance;
            if (xy.Raycast(ray, out distance))
            {
                return ray.GetPoint(distance);
            }

            return Vector3.zero;
        }
    }
}
