using UnityEngine;

namespace Scripts.Gold
{
    [CreateAssetMenu(fileName = "GoldConfig", menuName = "Configs/GoldConfig")]
    public class GoldConfig : ScriptableObject
    {
        public int startingGold     = 0;
        public int stageSuccessGold = 100;
        public int stageFailureGold = 20;
    }
}
