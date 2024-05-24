using System;

namespace Scripts.Ads
{
    public interface IAdsService
    {
        event Action<string> OnInterstitialImpression;
        event Action<string> OnInterstitialCompleted;

        event Action<RewardedVideoType> OnRewardedVideoImpression;
        event Action<RewardedVideoType> OnRewardedVideoCompleted;

        bool IsRewardedVideoReady();
    }
}
