using UnityEngine;
using UnityEngine.UI;

namespace Scripts.FeatureUnlock
{
    public class FeatureUnlockWidgetView : MonoBehaviour
    {
        [SerializeField]
        private Image _unlockedImage;
        [SerializeField]
        private Image _currentImage;
        [SerializeField]
        private Image _rewardImage;
        [SerializeField]
        private Image _connector;

        public void Initialize( bool isUnlocked, bool isCurrent, bool isReward )
        {
            _unlockedImage.enabled = isUnlocked;
            _currentImage.enabled  = isCurrent;
            _rewardImage.enabled   = isReward;
            _connector.enabled     = !isReward;
        }
    }
}
