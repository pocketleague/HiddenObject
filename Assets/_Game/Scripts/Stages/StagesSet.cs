using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Stages
{
    [CreateAssetMenu(menuName = ("Configs/StagesSet"), fileName = "StagesSet")]
    public class StagesSet : ScriptableObject
    {
        public string setId = "Control";
        public bool autostartFirstStage = true;
        public List<StageConfig> stages;
    }
}
