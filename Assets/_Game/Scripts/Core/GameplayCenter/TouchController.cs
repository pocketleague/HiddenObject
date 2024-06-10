using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Scripts.Core.GameplayCenter
{
	public class TouchController : MonoBehaviour
	{
		UnityEngine.Camera targetCamera;
		bool isDragging = false;
		public LayerMask layer;

		void Start()
		{
			targetCamera = UnityEngine.Camera.main;
		}

		private void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				bool isTargetClicked = false;
				Debug.Log("mouse click");

				Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
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
		}

		bool SelectClickedObjectAndCallAction(Transform colliders)
		{
			if (colliders.gameObject.layer == 6)
			{
				ClickableObject tobj = colliders.gameObject.GetComponent<ClickableObject>();

				if (tobj != null)
				{
					//tobj.OnClick();
					return true;
				}
				else
				{
					return false;
				}
			}
			return false;
		}
	}
}
