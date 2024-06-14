using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Scripts.Camera;

namespace Video
{
    public class GameManager : MonoBehaviour
    {
        public LayerMask targetLayer;
        public static event Action<ItemConfig> OnItemClicked = delegate { };

        public CameraConfig cameraConfig;
        public CameraView CameraView;
        public CameraStateConfig defaultState;
        public CameraStateConfig CurrentState;

        private void Start()
        {
            CurrentState = defaultState;
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0)) // Check for left mouse button click
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    if (hit.collider.gameObject.layer == 6)
                        SelectObjectAndCallAction(hit.transform, hit.point);
                    else if(hit.collider.gameObject.layer == 8)
                        SelectDrawer(hit.collider.GetComponent<Opener>());

                }
            }

            UpdateCam();
        }

        void SelectObjectAndCallAction(Transform obj, Vector3 hitPoint)
        {
            ItemView item = obj.gameObject.GetComponent<ItemView>();

            if (item.IsClickable)
            {
                if (item != null)
                {
                    ItemConfig config = item.OnClick(hitPoint);
                    OnItemClicked?.Invoke(config);
                }
            }
        }

        void SelectDrawer(Opener opener)
        {
            Debug.Log("select drawer");
            opener.Toggle();
        }

        void UpdateCam()
        {

            CameraView.positionTransform.position = Vector3.Lerp(CameraView.positionTransform.position, CurrentState.movementOffset, Time.deltaTime * CurrentState.movementChangeSpeed);
            CameraView.rotationTransform.rotation = Quaternion.Lerp(CameraView.rotationTransform.rotation, Quaternion.Euler(CurrentState.rotation), Time.deltaTime * CurrentState.rotationChangeSpeed);
            CameraView.distanceTransform.localPosition = Mathf.Lerp(CameraView.distanceTransform.localPosition.z, -CurrentState.distance, Time.deltaTime * CurrentState.distanceChangeSpeed) * Vector3.forward;
            CameraView.cameraObject.fieldOfView = Mathf.Lerp(CameraView.cameraObject.fieldOfView, CurrentState.fieldOfView, Time.deltaTime * CurrentState.clippingChangeSpeed);
            CameraView.cameraObject.nearClipPlane = Mathf.Lerp(CameraView.cameraObject.nearClipPlane, CurrentState.clipping.x, Time.deltaTime * CurrentState.clippingChangeSpeed);
            CameraView.cameraObject.farClipPlane       = Mathf.Lerp     ( CameraView.cameraObject.farClipPlane        , CurrentState.clipping.y              , Time.deltaTime * CurrentState.clippingChangeSpeed );
        }

        public void ChangeCam(CameraStateConfig camConfig = null)
        {
            if (camConfig != null)
                CurrentState = camConfig;
            else
                CurrentState = defaultState;
        }
    }
}