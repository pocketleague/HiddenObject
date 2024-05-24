using UnityEngine;
using System.Collections.Generic;
using Scripts.GameplayStates;

namespace Scripts.Stages
{
    [CreateAssetMenu(menuName = ("Configs/StageConfig"), fileName = "StageConfig")]
    public class StageConfig : ScriptableObject
    {
        public List<EGameplayState> states;
    }
}