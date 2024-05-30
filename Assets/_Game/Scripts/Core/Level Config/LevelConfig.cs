using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Core.LevelSelection
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        public LevelData levelData;
    }
}