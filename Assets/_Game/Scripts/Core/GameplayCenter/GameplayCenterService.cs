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
        public event Action<LevelPrefabView> OnLevelSpawned = delegate { };
        public event Action<ItemStateData> OnObjectFound = delegate { };

        private GameplayCenterConfig _config;
        private ILevelSelectionService _levelSelectionService;
        private IPlayerControlService _playerControlService;

        public LevelPrefabView CurrentLevelPrefabView;

        private Camera mainCam;

        private LayerMask targetLayer;

        private DiContainer _diContainer;

        [Inject]
        private void Construct(DiContainer diContainer, GameplayCenterConfig config, IPlayerControlService playerControlService, ICameraService cameraService, ILevelSelectionService levelSelectionService)
        {
            _config                 = config;
            _levelSelectionService  = levelSelectionService;
            _playerControlService   = playerControlService;
            _diContainer = diContainer;
            mainCam = cameraService.CameraView.cameraObject;

            SetLayerMask("ClickableObject");
        }

        public void Begin()
        {
            _playerControlService.ClickModule.OnMouseDown += OnMouseDown;
            OnGamePlayStarted.Invoke();

            SpawnLevelPrefab();
        }

        void SpawnLevelPrefab()
        {
            // Spawn level
            CurrentLevelPrefabView = _diContainer.InstantiatePrefab(_levelSelectionService.CurrentLevelConfig.levelData.levelPrefabView).GetComponent<LevelPrefabView>();
            Debug.Log("On Level spawned");

            CurrentLevelPrefabView.OnSetUp(() => {
                OnLevelSpawned.Invoke(CurrentLevelPrefabView);
            });
        }

        public void End()
        {
            OnGamePlayEnded.Invoke();
            _playerControlService.ClickModule.OnMouseDown -= OnMouseDown;

        }

        void OnMouseDown(Vector3 pos)
        {
            //Vector3 worldPosition = GetWorldPositionOnPlane(pos, 0f);
            //Debug.Log("click position " + worldPosition);

            bool isTargetClicked = false;
            Debug.Log("mouse click");

            Ray ray = mainCam.ScreenPointToRay(pos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, targetLayer))
            {
                Debug.Log(hit.transform.gameObject.layer);
                Debug.Log("hit");
                Vector3 p = hit.point;
                // p.z = 10;

                if (hit.collider.gameObject.layer == 6)
                {
                    isTargetClicked = SelectClickedObjectAndCallAction(hit.transform);
                    //if (isTargetClicked)
                    //{
                    //	Instantiate(ps, p, Quaternion.identity);
                    //}
                }

                if (!isTargetClicked)
                {
                    // misclick
                    //PointerMisclick.Invoke(hit.point);
                }
            }
        }

        bool SelectClickedObjectAndCallAction(Transform colliders)
        {
            if (colliders.gameObject.layer == 6)
            {
                ClickableObject tobj = colliders.gameObject.GetComponent<ClickableObject>();

                if (tobj != null)
                {
                    tobj.OnClick();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
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

        void SetLayerMask(params string[] layerNames)
        {
            targetLayer = LayerMask.GetMask(layerNames);
        }

        public void FoundObject(ItemStateData itemStateData)
        {
            CurrentLevelPrefabView.FoundObject(itemStateData);
            OnObjectFound.Invoke(itemStateData);
        }

    }
}
