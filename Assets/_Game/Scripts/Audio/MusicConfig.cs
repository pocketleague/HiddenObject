using UnityEngine;
using System.Collections.Generic;

namespace Scripts.Audio
{
    [CreateAssetMenu(fileName = "MusicConfig", menuName = "Configs/MusicConfig")]
    public class MusicConfig : ScriptableObject
    {
        public List<MusicClipConfig> clips;
        public float volume = 1f;
    }
}