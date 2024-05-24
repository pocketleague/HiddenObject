using System;
using Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Ads
{
    public class RewardedVideoButton : MonoBehaviour
    {
        public static Action<RewardedVideoType, bool> RewardedLocation = delegate{ };

        public RewardedVideoType adType;

        private Button _button;
        private Window _parentWindow;

        private void Awake()
        {
            _button       = GetComponent<Button>();
            _parentWindow = GetComponentInParent<Window>();

            _parentWindow.Opened += OnWindowOpened;
        }

        private void OnDestroy()
        {
            _parentWindow.Opened -= OnWindowOpened;
        }

        private void OnWindowOpened()
        {
            bool isAdReady = true;
#if TTP_CORE
        isAdReady = Tabtale.TTPlugins.TTPRewardedAds.IsReady();
#endif
            _button.interactable = isAdReady;

            RewardedLocation.Invoke( adType, isAdReady );
        }
    }
}
