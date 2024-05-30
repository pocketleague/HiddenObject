/* SoundManager.cs
  version 1.0 - Feb 8, 2017

  Copyright (C) Wasabi Applications Inc.
   http://wasabi-apps.co.jp
*/

using UnityEngine;
using System;

namespace HOGM
{

	[RequireComponent(typeof(AudioSource))]
	public class SoundManager : MonoBehaviour
	{
		
		[SerializeField]
		AudioClip audioClipButton1;
		[SerializeField]
		AudioClip audioClipButton2;
		[SerializeField]
		AudioClip audioClipCorrect;
		[SerializeField]
		AudioClip audioClipMis;
		[SerializeField]
		AudioClip audioClipSelect;
		[SerializeField]
		AudioClip audioClipHint;
		[SerializeField]
		AudioClip audioClipTimeIsUp;
		[SerializeField]
		AudioClip audioClipGameClear;
		[SerializeField]
		AudioClip audioClipStart;
		[SerializeField]
		AudioClip audioClipStar;
		[SerializeField]
		AudioClip audioClipBest;


		public static SoundManager Instance {get; private set;}

		AudioSource audioSource;


		void Start ()
		{	
			// Singleton. (If instance is already set, destroy myself.)
			if (Instance != null)
			{
				Destroy(gameObject);
				return;
			}
			else
			{
				Instance = this;
				DontDestroyOnLoad(gameObject);
			}

			//
			audioSource = GetComponent<AudioSource>();
		}


		// ------------------------------------------------------------------------------------

		public void PlaySEButton1 ()
		{
			audioSource.PlayOneShot(audioClipButton1);
		}


		public void PlaySEButton2 ()
		{
			audioSource.PlayOneShot(audioClipButton2);
		}


		public void PlaySECorrect ()
		{
			audioSource.PlayOneShot(audioClipCorrect);
		}


		public void PlaySEMis ()
		{
			audioSource.PlayOneShot(audioClipMis);
		}


		public void PlaySESelect ()
		{
			audioSource.PlayOneShot(audioClipSelect);
		}


		public void PlaySEHint ()
		{
			audioSource.PlayOneShot(audioClipHint);
		}


		public void PlaySETimeIsUp ()
		{
			audioSource.PlayOneShot(audioClipTimeIsUp);
		}


		public void PlaySEGameClear ()
		{
			audioSource.PlayOneShot(audioClipGameClear);
		}


		public void PlaySEStart ()
		{
			audioSource.PlayOneShot(audioClipStart);
		}


		public void PlaySEStar ()
		{
			audioSource.PlayOneShot(audioClipStar);
		}


		public void PlaySEBest ()
		{
			audioSource.PlayOneShot(audioClipBest);
		}

	}
}
