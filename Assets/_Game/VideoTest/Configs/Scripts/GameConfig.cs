using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Video
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/Video/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        public ItemConfig [] itemConfigs;
    }
}
