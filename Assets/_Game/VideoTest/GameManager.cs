using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Video
{
    public class GameManager : MonoBehaviour
    {
        public LayerMask targetLayer;
        public static event Action<ItemConfig> OnItemClicked = delegate { };

        void Update()
        {
            if (Input.GetMouseButtonDown(0)) // Check for left mouse button click
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, targetLayer))
                {
                    SelectObjectAndCallAction(hit.transform, hit.point);
                }
            }
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
    }
}