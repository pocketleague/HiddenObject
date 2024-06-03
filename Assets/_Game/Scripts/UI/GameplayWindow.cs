using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Zenject;
using GameplayCenter;
using Scripts.Core;
using Scripts.Core.LevelSelection;
using Scripts.Timer;

namespace Scripts.UI
{
    [RequireComponent(typeof( Window ) )]
    public class GameplayWindow : MonoBehaviour
    {

        [Inject] private IGameplayCenterService _gameplayService;
        [Inject] private IStateManagerService   _stateManager;
        [Inject] private ITimerService          _timerService;


        private Window _targetWindow;

        [SerializeField] Button btnEnd, btnPenalty;
        [SerializeField] ObjectUiItemView itemView_prefab;

        [SerializeField] Transform content;

        List<ObjectUiItemView> itemList;

        [Inject]
        private void Construct(IGameplayCenterService service)
        {
            _targetWindow = GetComponent<Window>();

            _gameplayService.OnGamePlayStarted  += OnGamePlayStarted;
            _gameplayService.OnGamePlayEnded    += OnGamePlayEnded;

            _gameplayService.OnLevelSpawned     += SpawnItemsUI;
            _gameplayService.OnObjectFound      += OnObjectFound;



            btnEnd.onClick.AddListener(End);
            btnPenalty.onClick.AddListener(Penalty);

            itemList = new List<ObjectUiItemView>();
        }

        private void OnDestroy()
        {
            _gameplayService.OnGamePlayStarted  -= OnGamePlayStarted;
            _gameplayService.OnGamePlayEnded    -= OnGamePlayEnded;
        }

        private void OnGamePlayStarted()
        {
            _targetWindow.Open();
        }

        void SpawnItemsUI(LevelPrefabView view)
        {
            // Delete old items
            foreach (ObjectUiItemView item in itemList)
            {
                Destroy(item.gameObject);
            }
            // Find Items to spawn
            foreach (var itemData in view.levelItemDatas)
            {
                // Instantiate ObjectUiItemView
                ObjectUiItemView itemView = Instantiate(itemView_prefab, content);
                itemList.Add(itemView);

                // Find total count
                int totalCount = 0;
                foreach (var targetObjectView in itemData.item)
                {
                    totalCount++;
                }

                itemView.Setup(totalCount);
            }
        }

        private void OnGamePlayEnded()
        {
            _targetWindow.Close();
        }

        void OnObjectFound(ItemStateData itemStateData)
        {

        }

        void End()
        {
            _stateManager.ChangeState(EState.RewardCenter);
        }

        void Penalty()
        {
            _timerService.Penalty();
        }
    }
}