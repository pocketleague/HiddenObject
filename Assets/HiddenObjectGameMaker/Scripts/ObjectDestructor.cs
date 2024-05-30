/* ObjectDestructor.cs
  version 1.0 - Feb 8, 2017

  Copyright (C) Wasabi Applications Inc.
   http://wasabi-apps.co.jp
*/

using UnityEngine;
using System.Collections;

namespace HOGM
{
	public class ObjectDestructor : MonoBehaviour
	{		
		public void DestroyMe ()
		{
			Destroy(this.gameObject);
		}
	}
}
