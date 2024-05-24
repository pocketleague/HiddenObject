using UnityEngine;

namespace Scripts.Unlockables
{
    [CreateAssetMenu( fileName = "UnlockableConfig", menuName = "Configs/UnlockableConfig" )]
    public class UnlockableConfig : ScriptableObject
    {
        public string          id;
        public EUnlockableType type;

        public Sprite storeImage;
    }
}