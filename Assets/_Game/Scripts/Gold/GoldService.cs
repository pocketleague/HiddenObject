using System;
using Scripts.Stages;
using UnityEngine;
using Zenject;

namespace Scripts.Gold
{
    public class GoldService : IGoldService
    {
        public event Action<int> OnGoldAmountChanged = delegate{ };
        public event Action<int> OnGoldEarned        = delegate{ };
        public event Action OnGoldSpend         = delegate { };

        private GoldConfig      _config;
        private IStagesService  _stagesService;
        
        public int GoldAmount        { get; private set; }
        public int GoldEarnedOnStage { get; private set; }

        [Inject]
        private void Construct(GoldConfig config, IStagesService stagesService )
        {
            _config         = config;
            _stagesService  = stagesService;

            GoldAmount = PlayerPrefs.GetInt( GoldPrefsStrings.GOLD_AMOUNT, _config.startingGold );

            _stagesService.OnStageFinished += CalculateGoldForStageFinished;
            _stagesService.OnStageSpawned  += ResetEarnedGold;

            OnGoldAmountChanged.Invoke( GoldAmount );
        }

        public void AddGold( int amount )
        {
            GoldAmount += amount;
            
            SaveGoldAmount();
            
            OnGoldAmountChanged.Invoke( GoldAmount );
        }

        public void SpendGold( int amount )
        {
            GoldAmount -= amount;
            
            SaveGoldAmount();
            
            OnGoldSpend.Invoke();
            OnGoldAmountChanged.Invoke( GoldAmount );
        }

        public bool HasGold( int amount ) => GoldAmount >= amount;

        private void SaveGoldAmount()
        {
            PlayerPrefs.SetInt( GoldPrefsStrings.GOLD_AMOUNT, GoldAmount );
        }

        private void CalculateGoldForStageFinished( int stageId, bool success )
        {
            GoldEarnedOnStage += ( success ? _config.stageSuccessGold : _config.stageFailureGold );
            
            AddGold( GoldEarnedOnStage );

            OnGoldEarned.Invoke( GoldEarnedOnStage );
        }

        private void ResetEarnedGold( int stageId, StageConfig stageConfig )
        {
            GoldEarnedOnStage = 0;
        }
    }
}
