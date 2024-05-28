using System;

namespace Scripts.Core.LevelSelection
{
    public interface ILevelSelectionService
    {
        event Action OnLevelSelectionStarted;
        event Action OnLevelSelectionEnded;

        void SelectLevel(int index);
    }
}
