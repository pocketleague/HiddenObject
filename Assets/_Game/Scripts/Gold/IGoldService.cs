using System;

namespace Scripts.Gold
{
    public interface IGoldService
    {
        event Action<int> OnGoldAmountChanged;
        event Action<int> OnGoldEarned;
        event Action OnGoldSpend;

        int GoldAmount { get; }
        int GoldEarnedOnStage { get; }
        
        void AddGold(int amount);
        void SpendGold(int amount);
        bool HasGold( int amount );
    }
}
