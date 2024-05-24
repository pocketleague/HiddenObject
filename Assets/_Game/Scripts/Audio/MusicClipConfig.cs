using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Scripts.Audio
{
    [CreateAssetMenu(fileName = "MusicClipConfig", menuName = "Configs/MusicClipConfig")]
    public class MusicClipConfig : ScriptableObject
    {
        public string id;
        public AudioClip clip;

        [Range(0f, 1f)]
        public float volume = 1f;
    }
}
