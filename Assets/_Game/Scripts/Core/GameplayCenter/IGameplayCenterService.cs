using System;
using Scripts.Core.LevelSelection;

namespace Scripts.Core.GameplayCenter
{
    public interface IGameplayCenterService
    {
        event Action OnGamePlayStarted;
        event Action OnGamePlayEnded;
        event Action<LevelPrefabView> OnLevelSpawned;
        event Action<ItemStateData> OnObjectFound;

        void FoundObject(ItemStateData itemStateData);
    }
}