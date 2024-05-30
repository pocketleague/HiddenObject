using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TouchController : MonoBehaviour
{

	//[System.Serializable]
	//public class Vector3Event : UnityEvent<Vector3>
	//{
	//}


	public class UITouchController : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
	{
		Camera targetCamera;
		bool isDragging = false;

		void Start()
		{
			targetCamera = Camera.main;
		}


		public void OnPointerClick(PointerEventData eventData)
		{
			if (!isDragging)
			{
				bool isTargetClicked = false;

				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit, Mathf.Infinity))
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

			if(colliders.gameObject.layer == 6)
            {
				ClickableObject tobj = colliders.gameObject.GetComponent<ClickableObject>();

				if (tobj != null )
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




		public void OnBeginDrag(PointerEventData eventData)
		{
			isDragging = true;
		}


		public void OnDrag(PointerEventData eventData)
		{
			isDragging = false;
		}


		public void OnEndDrag(PointerEventData eventData)
		{
			isDragging = false;
		}


	}

}
