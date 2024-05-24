using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Tutorial
{
    [CreateAssetMenu( fileName = "TutorialConfig", menuName = "Configs/TutorialConfig")]
    public class TutorialConfig : ScriptableObject
    {
        public List<TutorialStateConfig> states;
    }
}
