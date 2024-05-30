/* UITouchController.cs
  version 1.0 - Feb 8, 2017

  Copyright (C) Wasabi Applications Inc.
   http://wasabi-apps.co.jp
*/

using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace HOGM
{
	
	[System.Serializable]
	public class Vector3Event : UnityEvent<Vector3>
	{
	}


	public class UITouchController : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
	{

		[SerializeField]
		SearchAreaSprite searchArea;

		[SerializeField]
		public GameObject ps;
		[SerializeField]
		float zoomSpeed = 0.002f;
		[SerializeField, TooltipAttribute("1.0 = initial scale")]
		float minScale = 1.0f; 
		[SerializeField, TooltipAttribute("if 3.0 then initial x 3.0")]
		float maxScale = 3.0f;

		[SerializeField]
		bool isNoMoveOutOfRange = true;
		[SerializeField]
		bool isMoveBackInRange = false;
		[SerializeField]
		bool useInterialMovement = true;

		[SerializeField]
		Vector3Event PointerMisclick;


		Camera targetCamera;
		RectTransform rectRange;

		float initialScale;

		bool isScaling = false;
		bool isDragging = false;
		bool isInterialMoving = false;

		Vector2 velocityLastMove; //for ineria
		readonly float inertiaInvalidTimeAfterScaling = 0.2f; //sec.
		float interiaInvalidTimeLeft = 0f;

		Dictionary<int, PointerEventData> draggingEventDataDict = new Dictionary<int, PointerEventData>();
		int layerMask = 1 << 6;

		public LayerMask layer;

		void Start ()
		{
			targetCamera = Camera.main;
			rectRange = GetComponent<RectTransform>();

			StartCoroutine(SetInitPositionAndScaleOne());
			//layerMask = ~layerMask;

		}


		void Update ()
		{
			// For editor operation
			int flagForScaling = 0;
			if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
			{
				flagForScaling = 1;
			}
			else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
			{
				flagForScaling = 2;
			}

			//
			if (0 < flagForScaling)
			{
				// For editor
				ScaleUpDown(Input.mousePosition, flagForScaling==1 ? 20f : -20f);
			}
			else
			{
				if (isScaling && draggingEventDataDict.Count < 2)
				{
					//End scaling
					isScaling = false;
					interiaInvalidTimeLeft = inertiaInvalidTimeAfterScaling;

					if (draggingEventDataDict.Count==0 && isMoveBackInRange)
					{
						MoveBackInRange();
					}
				}
			}

			//
			if (2 <= draggingEventDataDict.Count)
			{
				OnPinch();
			}
			else if (1 == draggingEventDataDict.Count)
			{
				foreach (KeyValuePair<int, PointerEventData> kvp in draggingEventDataDict)
				{
					MoveSearchArea(kvp.Value.position - kvp.Value.delta, kvp.Value.position);
					break;
				}
			}
			else
			{
				if (isDragging)
				{
					isDragging = false;
					if (isMoveBackInRange) MoveBackInRange();

					if (velocityLastMove != Vector2.zero)
					{
						if (useInterialMovement) StartCoroutine(MoveInertially());
					}
				}
			}

			//
			interiaInvalidTimeLeft -= Time.deltaTime;
		}


		IEnumerator SetInitPositionAndScaleOne ()
		{
			yield return new WaitForEndOfFrame();
			MoveBackInRange();
			initialScale = searchArea.transform.localScale.x; // x=y=z presupposed
		}

		void FixedUpdate()
		{

			if(Input.GetMouseButtonUp(0))
            {

				if (!isDragging)
				{
					


					//// Does the ray intersect any objects excluding the player layer
					//if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
					//{
					//	Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
					//	Debug.Log("Did Hit");
					//}
					//else
					//{
					//	Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
					//	Debug.Log("Did not Hit");
					//}
				}
			}

		}

		public void OnPointerClick (PointerEventData eventData)
		{
			if (!isDragging)
			{
				bool isTargetClicked = false;

				// Bit shift the index of the layer (8) to get a bit mask


				// This would cast rays only against colliders in layer 8.
				// But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.


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
						if(isTargetClicked)
                        {
							Instantiate(ps, p, Quaternion.identity);
						}
					}

					if (!isTargetClicked)
					{
						// misclick
						PointerMisclick.Invoke(hit.point);
					}
				}
				//bool isTargetClicked = false;
				//Vector3 pos = targetCamera.ScreenToWorldPoint(eventData.position);
				//Vector3 p = pos;
				//p.z = 10;


				//Collider2D[] colliders = Physics2D.OverlapPointAll(pos);
				//if (0 < colliders.Length)
				//{
				//	Instantiate(ps, p, Quaternion.identity);
				//	isTargetClicked = SelectClickedObjectAndCallAction(colliders);
				//}

				//if (!isTargetClicked)
				//{
				//	// misclick
				//	PointerMisclick.Invoke(pos);
				//}
			}
		}


		bool SelectClickedObjectAndCallAction (Transform colliders)
		{

			TargetObject tobj = colliders.gameObject.GetComponent<TargetObject>();
			//	tobj.OnClick();
            //if (colliders.Length == 1)
            //{
            //	TargetObject tobj = colliders[0].GetComponent<TargetObject>();
            if (tobj != null && tobj.IsTarget())
            {
				tobj.OnClick();
				return true;
			}
            else
            {
				return false;
			}
               
			// }
			//else
			//{
			//	TargetObject selectedTargetObj = null;
			//	int maxOrder = -99999;
			//	foreach (Collider2D col in colliders)
			//	{
			//		TargetObject tobj = col.GetComponent<TargetObject>();
			//		if (tobj != null && tobj.IsTarget())
			//		{
			//			int sortingOrder = tobj.GetSortingOrderInLayer();
			//			if (maxOrder < sortingOrder)
			//			{
			//				maxOrder = sortingOrder;
			//				selectedTargetObj = tobj;
			//			}
			//		}
			//	}

			//	if (selectedTargetObj)
			//		selectedTargetObj.OnClick();
			//	else	
			//		return false;
			//}

			//return true;
		}




		public void OnBeginDrag (PointerEventData eventData)
		{
			isDragging = true;
			AddOrUpdateDraggingEventData(eventData);
		}


		public void OnDrag (PointerEventData eventData)
		{
			AddOrUpdateDraggingEventData(eventData);
		}


		public void OnEndDrag (PointerEventData eventData)
		{
			RemoveDraggingEventData(eventData.pointerId);
		}




		void MoveSearchArea (Vector3 screenPrevPos, Vector3 screenPos)
		{
			Vector3 delta = targetCamera.ScreenToWorldPoint(screenPos)
				- targetCamera.ScreenToWorldPoint(screenPrevPos);
			MoveSearchArea(delta);

			velocityLastMove = delta;
		}


		void MoveSearchArea (Vector3 delta)
		{
			searchArea.Move(delta);
			if (isNoMoveOutOfRange || isInterialMoving)	MoveBackInRange();
		}


		void MoveBackInRange ()
		{
			Vector3[] rangeCorners = new Vector3[4];
			rectRange.GetWorldCorners(rangeCorners); //0:BottomLeft 1:TL 2: TR 3:BR
			searchArea.MoveBackInRange(rangeCorners[0], rangeCorners[2]);
		}




		IEnumerator MoveInertially ()
		{
			yield return new WaitForEndOfFrame();

			if (0f < interiaInvalidTimeLeft) yield break;

			//Customize sensibility if you need.
			float duringTime = 0.8f;
			float minVelocityMag = 1.0f;

			float elapsedTime = 0f;

			while (elapsedTime < duringTime)
			{
				if (isDragging) break;
				if (velocityLastMove == Vector2.zero) break;

				isInterialMoving = true;
				velocityLastMove = Vector2.Lerp(velocityLastMove, Vector2.zero, elapsedTime/duringTime);
				MoveSearchArea(velocityLastMove);

				if (velocityLastMove.magnitude < minVelocityMag)
				{
					velocityLastMove = Vector2.zero;
					break;
				}

				elapsedTime += Time.deltaTime;
				yield return new WaitForEndOfFrame();
			}

			isInterialMoving = false;
			if (isMoveBackInRange) MoveBackInRange();
		}




		void OnPinch ()
		{
			Vector2[] touchPos = new Vector2[2];
			Vector2[] touchPrevPos = new Vector2[2];

			int count = 0;
			foreach (KeyValuePair<int, PointerEventData> kvp in draggingEventDataDict)
			{
				if (2 <= count) break;
				touchPos[count] = kvp.Value.position;
				touchPrevPos[count] = kvp.Value.position - kvp.Value.delta;
				count++;
			}

			float prevTouchDeltaMag = (touchPrevPos[0] - touchPrevPos[1]).magnitude;
			float touchDeltaMag = (touchPos[0] - touchPos[1]).magnitude;

			float deltaMagnitudeDiff = touchDeltaMag - prevTouchDeltaMag;

			Vector2 touchCenter = (touchPos[0] + touchPos[1]) * 0.5f;
			Vector2 touchPrevCenter = (touchPrevPos[0] + touchPrevPos[1]) * 0.5f;

			//Scale
			ScaleUpDown(touchCenter, deltaMagnitudeDiff);

			//Move
			MoveSearchArea(touchPrevCenter, touchCenter);
		}


		void ScaleUpDown (Vector2 touchCenterPosition, float changeDistance)
		{
			isScaling = true;

			Vector3 centerPos = targetCamera.ScreenToWorldPoint(touchCenterPosition);
			searchArea.Scale(centerPos, changeDistance*zoomSpeed, minScale*initialScale, maxScale*initialScale);

			if (isNoMoveOutOfRange)	MoveBackInRange();
		}



		void AddOrUpdateDraggingEventData (PointerEventData eventData)
		{
			if (draggingEventDataDict.ContainsKey(eventData.pointerId))
			{
				draggingEventDataDict[eventData.pointerId] = eventData;
			}
			else
			{
				draggingEventDataDict.Add(eventData.pointerId, eventData);
			}
		}



		void RemoveDraggingEventData (int pointerId)
		{
			if (draggingEventDataDict.ContainsKey(pointerId))
			{
				draggingEventDataDict.Remove(pointerId);
			}
		}

	}

}
