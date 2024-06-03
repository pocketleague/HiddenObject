using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Scripts.Stages;
using System;
using Scripts.Core;
using Scripts.PlayerControl;
using Scripts.Camera;
using Scripts.Core.LevelSelection;

namespace GameplayCenter
{
    public class GameplayCenterService : IGameplayCenterService, IState
    {
        public event Action OnGamePlayStarted = delegate { };
        public event Action OnGamePlayEnded = delegate { };

        private GameplayCenterConfig _config;
        private ILevelSelectionService _levelSelectionService;

        public LevelPrefabView CurrentLevelPrefabView;

        private Camera mainCam;

        [Inject]
        private void Construct(GameplayCenterConfig config, IPlayerControlService playerControlService, ICameraService cameraService, ILevelSelectionService levelSelectionService)
        {
            _config                 = config;
            _levelSelectionService  = levelSelectionService;

            playerControlService.ClickModule.OnMouseDown += OnMouseDown;

            mainCam = cameraService.CameraView.cameraObject;
        }

        public void Begin()
        {
            OnGamePlayStarted.Invoke();

            // Spawn level
            CurrentLevelPrefabView = GameObject.Instantiate(_levelSelectionService.CurrentLevelConfig.levelData.levelPrefabView);
            Debug.Log("On Level spawned");

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
