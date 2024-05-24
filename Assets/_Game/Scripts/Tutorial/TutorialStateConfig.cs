using UnityEngine;

namespace Scripts.Tutorial
{
    [CreateAssetMenu( fileName = "TutorialStateConfig", menuName = "Configs/TutorialStateConfig")]
    public class TutorialStateConfig : ScriptableObject
    {
        public string id;
        public int    stageId;
        public float  startDelay;

        public bool pauseGame  = true;
        public bool blockInput = true;

        [Header("Tutorial Window")]
        public bool   showWindow;
        public string windowTitle;
        public string windowBody;
        public Sprite windowImage;

        [Header( "Tutorial PopUp")]
        public bool   showPopUp;
        public string popUpText;
    }
}
