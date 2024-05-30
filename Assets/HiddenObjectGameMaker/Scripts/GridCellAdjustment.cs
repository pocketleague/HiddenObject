/* GridCellAdjustment.cs
  version 1.0 - Feb 8, 2017

  Copyright (C) Wasabi Applications Inc.
   http://wasabi-apps.co.jp
*/

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace HOGM
{
	public class GridCellAdjustment : MonoBehaviour
	{
		
		void Start ()
		{
			GridLayoutGroup gridLayout = GetComponent<GridLayoutGroup>();
			if (gridLayout == null)
			{
				return;	
			}

			if (gridLayout.constraint != GridLayoutGroup.Constraint.FixedColumnCount)
			{
				return;
			}

			// above is precondition


			RectTransform rectTf = GetComponent<RectTransform>();

			// fit width 
			Vector2 cellSize = gridLayout.cellSize;
			Vector2 spaceSize = gridLayout.spacing;

			float baseWidth = rectTf.rect.width - (spaceSize.x * (gridLayout.constraintCount-1));
			float cellWidth = baseWidth / gridLayout.constraintCount;

			cellSize.x = cellWidth;
			gridLayout.cellSize = cellSize;

		}

	}
}
