using UnityEngine;

namespace Scripts.FeatureUnlock
{
    [CreateAssetMenu( fileName = "FeatureUnlockConfig", menuName = "Configs/FeatureUnlockConfig" )]
    public class FeatureUnlockConfig : ScriptableObject
    {
        public int    showOnLevel;
        public string featureName;
        public Sprite featureSprite;
    }
}
