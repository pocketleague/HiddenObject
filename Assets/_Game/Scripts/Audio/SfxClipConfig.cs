using UnityEngine;

namespace Scripts.Audio
{
    [CreateAssetMenu( fileName = "SfxClipConfig", menuName = "Configs/SfxClipConfig" )]
    public class SfxClipConfig : ScriptableObject
    {
        public string    id;
        public AudioClip clip;
        [Range(0f, 1f)]
        public float     volume = 1f;
    }
}