/* SearchAreaSprite.cs
  version 1.0 - Feb 8, 2017
  version 1.0.1 - Apr 11, 2017

  Copyright (C) Wasabi Applications Inc.
   http://wasabi-apps.co.jp
*/

using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;


namespace HOGM
{
	public class SearchAreaSprite : MonoBehaviour
	{
		
		SpriteRenderer spriteArea;


		void Start ()
		{
			spriteArea = GetComponent<SpriteRenderer>();
		}
			

		public void Move (Vector3 delta)
		{
			transform.position += delta;
		}


		public void Scale (Vector3 centerPos, float increase, float min, float max)
		{
			float val = Mathf.Clamp(transform.localScale.x + increase, min, max);
			Vector3 newScale = new Vector3 (val, val, val);


			Vector3 offset = centerPos - transform.position;
			Vector3 offsetR = (transform.localScale.x / newScale.x) * offset;

			Vector3 newPos = transform.position + offsetR - offset;
			newPos.z = 0f;

			transform.localScale = newScale;
			transform.position = newPos;
		}


		public void MoveBackInRange (Vector2 bottomLeft, Vector2 topRight)
		{
			//
			float bgL = spriteArea.bounds.center.x - spriteArea.bounds.size.x * 0.5f;
			float bgR = spriteArea.bounds.center.x + spriteArea.bounds.size.x * 0.5f;
			float bgT = spriteArea.bounds.center.y + spriteArea.bounds.size.y * 0.5f;
			float bgB = spriteArea.bounds.center.y - spriteArea.bounds.size.y * 0.5f;
			float bgW = bgR - bgL;
			float bgH = bgT - bgB;

			//
			float areaW = topRight.x - bottomLeft.x;
			float areaH = topRight.y - bottomLeft.y;

			//
			Vector3 newPos = transform.position;

			if (bgW < areaW)
			{
				newPos.x = (bottomLeft.x + topRight.x) * 0.5f;
			}
			else
			{
				if (bottomLeft.x < bgL)
				{
					newPos.x += bottomLeft.x - bgL;
				}
				else if (bgR < topRight.x)
				{
					newPos.x += topRight.x - bgR;
				}
			}

			if (bgH < areaH)
			{
				newPos.y = (bottomLeft.y + topRight.y) * 0.5f;
			}
			else
			{
				if (bottomLeft.y < bgB)
				{
					newPos.y += bottomLeft.y - bgB;
				}
				else if (bgT < topRight.y)
				{
					newPos.y += topRight.y - bgT;
				}
			}

			transform.position = newPos;
		}
	}
}
