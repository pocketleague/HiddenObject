using System;
using System.Collections.Generic;

namespace Scripts.Unlockables
{
    public interface IUnlockablesService
    {
        event Action<UnlockableConfig> OnItemUnlocked;

        event Action<UnlockableConfig> OnItemSelected;

        void UnlockItem( EUnlockableType type );

        bool IsItemUnlocked( UnlockableConfig item );

        int GetItemPrice( EUnlockableType type );

        void SelectItem( UnlockableConfig item );

        bool IsItemSelected( UnlockableConfig item );

        UnlockableConfig GetSelectedItem( EUnlockableType type );

        List<UnlockableConfig> GetUnlockablesOfType( EUnlockableType type );
    }
}
