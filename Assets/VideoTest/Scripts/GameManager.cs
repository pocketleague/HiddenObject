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

        public CameraView CameraView;
        public CameraStateConfig defaultCamState;
        private CameraStateConfig CurrentCamState;

        public WrongClickSpawner wrongClickSpawner;

        public GameObject ItemPanel;
        public Transform itemCenterPos, itemPosAtGirl;

        public ParticleSystem heartParticles;

        private void Start()
        {
            CurrentCamState = defaultCamState;

            // UI Animation
            Invoke("Delay", 2);
        }

        void Delay()
        {
            ItemPanel.SetActive(true);
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
                    else if (hit.collider.gameObject.layer == 9)
                    {

                    }
                    else
                    {
                        Debug.Log("spawn wrong image "+hit.point);

                        wrongClickSpawner.SpawnWrongImage();
                    }
                }
            }

            UpdateCam();
        }

        void ShowWrong(Vector3 pos)
        {
            // Get the mouse position in world space
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10.0f; // Set a fixed distance from the camera
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

            //worldPos = pos;

            // Instantiate the image prefab at the calculated world position
            Instantiate(wrongClickSpawner, worldPos, Quaternion.identity);
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

            CameraView.positionTransform.position = Vector3.Lerp(CameraView.positionTransform.position, CurrentCamState.movementOffset, Time.deltaTime * CurrentCamState.movementChangeSpeed);
            CameraView.rotationTransform.rotation = Quaternion.Lerp(CameraView.rotationTransform.rotation, Quaternion.Euler(CurrentCamState.rotation), Time.deltaTime * CurrentCamState.rotationChangeSpeed);
            CameraView.distanceTransform.localPosition = Mathf.Lerp(CameraView.distanceTransform.localPosition.z, -CurrentCamState.distance, Time.deltaTime * CurrentCamState.distanceChangeSpeed) * Vector3.forward;
            CameraView.cameraObject.fieldOfView = Mathf.Lerp(CameraView.cameraObject.fieldOfView, CurrentCamState.fieldOfView, Time.deltaTime * CurrentCamState.clippingChangeSpeed);
            CameraView.cameraObject.nearClipPlane = Mathf.Lerp(CameraView.cameraObject.nearClipPlane, CurrentCamState.clipping.x, Time.deltaTime * CurrentCamState.clippingChangeSpeed);
            CameraView.cameraObject.farClipPlane       = Mathf.Lerp     ( CameraView.cameraObject.farClipPlane        , CurrentCamState.clipping.y              , Time.deltaTime * CurrentCamState.clippingChangeSpeed );
        }

        public void ChangeCam(CameraStateConfig camConfig = null)
        {
            if (camConfig != null)
                CurrentCamState = camConfig;
            else
                CurrentCamState = defaultCamState;
        }

        public void PlayHeartParticles()
        {
            heartParticles.Play();
        }
    }
}