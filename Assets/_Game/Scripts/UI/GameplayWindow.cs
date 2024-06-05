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
        //List<ObjectUiItemView> itemList;
        public int activeObjectIndex;

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
            int childPosIndex = 0;
            // Delete old items
            foreach (ObjectUiItemView item in itemList)
            {
                Destroy(item.gameObject);
            }
            itemList.Clear();

            // Find Items to spawn
            foreach (var itemData in view.levelItemDatas)
            {
                // Instantiate ObjectUiItemView
                ObjectUiItemView UiItemView = Instantiate(itemView_prefab, content);
                itemList.Add(UiItemView);

                // Find total count
                int totalCount = 0;
                foreach (var targetObjectView in itemData.item)
                {
                    totalCount++;
                }
                childPosIndex++;

                UiItemView.Setup(totalCount, itemData.id, itemData.itemSprite);
                UiItemView.SetChildIndex(childPosIndex);
                UiItemView.gameObject.SetActive(false);

            }

            for (int j = 0; j < 3; j++)
            {
                EnableTargetObjectUI(j);
            }
        }

        void EnableTargetObjectUI(int childPos)
        {
            //if (activeObjectIndex < itemList.Count)
            //{
                GameObject targetobject = itemList[0].gameObject;
                targetobject.transform.SetSiblingIndex(childPos);
                itemList[activeObjectIndex].SetChildIndex(childPos);
                targetobject.gameObject.SetActive(true);
                activeObjectIndex++;
           // }
        }

        private void OnGamePlayEnded()
        {
            _targetWindow.Close();
        }

        void OnObjectFound(ItemStateData itemStateData)
        {
            foreach (var uiItemView in itemList)
            {
                if (uiItemView.itemID == itemStateData.itemID)
                {
                    uiItemView.ReduceCount();

                    if (uiItemView.counter <= 0)
                    {
                        itemList.Remove(uiItemView);
                        Destroy(uiItemView.gameObject);
                        EnableTargetObjectUI(uiItemView.childPosIndex);
                        break;
                    }
                }
            }

            if(itemList.Count == 0)
            {
                End();
            }
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