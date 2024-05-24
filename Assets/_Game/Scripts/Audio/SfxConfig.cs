using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Audio
{
    [CreateAssetMenu(fileName = "SFXConfig", menuName = "Configs/SFXConfig")]
    public class SfxConfig : ScriptableObject
    {
        public List<SfxClipConfig> clips;
    }
}