using System;
using Scripts.UI;
using UnityEngine;

namespace Scripts.Ads
{
    [RequireComponent(typeof(CanvasGroup))]
    public class RewardedVideoCanvasGroup : MonoBehaviour
    {
        public static Action<RewardedVideoType, bool> RewardedLocation = delegate{ };

        public RewardedVideoType adType;

        private CanvasGroup _canvasGroup;
        private Window      _parentWindow;

        private bool _rewardedIsReady = false;

        private void Awake()
        {
            _canvasGroup  = GetComponent<CanvasGroup>();
            _parentWindow = GetComponentInParent<Window>();

            _parentWindow.Opened += OnWindowOpened;
#if TTP_CORE
        Tabtale.TTPlugins.TTPRewardedAds.ReadyEvent += OnRewardedReady;
#endif
        }

        private void OnDestroy()
        {
            _parentWindow.Opened -= OnWindowOpened;
#if TTP_CORE
        Tabtale.TTPlugins.TTPRewardedAds.ReadyEvent -= OnRewardedReady;
#endif
        }

        private void OnRewardedReady( bool isReady )
        {
            _rewardedIsReady = isReady;
        }

        private void OnWindowOpened()
        {
            _canvasGroup.interactable   = _rewardedIsReady;
            _canvasGroup.blocksRaycasts = _rewardedIsReady;
            _canvasGroup.alpha          = _rewardedIsReady ? 1f : 0f;

            RewardedLocation.Invoke( adType, _rewardedIsReady );
        }
    }
}
