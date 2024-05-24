using System;
using System.Collections.Generic;
using System.Linq;
using Scripts.Gold;
using UnityEngine;
using Zenject;

namespace Scripts.Unlockables
{
    public class UnlockablesService : IUnlockablesService
    {
        public event Action<UnlockableConfig> OnItemUnlocked = delegate { };
        public event Action<UnlockableConfig> OnItemSelected = delegate { };

        private UnlockablesConfig _config;
        private IGoldService      _goldService;
        
        private HashSet<string>                               _unlockedItems;
        private Dictionary<EUnlockableType, UnlockableConfig> _selectedItems;
        private Dictionary<EUnlockableType, int>              _unlockedItemsAmount;

        [Inject]
        private void Construct( UnlockablesConfig config, IGoldService goldService )
        {
            _config      = config;
            _goldService = goldService;
            
            LoadAllItemsStatus( );
        }
        
        public void UnlockItem( EUnlockableType type )
        {
            var nextToUnlockItem = _config.GetNextUnlockableToUnlock( type, _unlockedItemsAmount[type] );

            if ( nextToUnlockItem == null )
                return;

            if ( !_goldService.HasGold( GetItemPrice( type ) ) )
                return;
            
            _goldService.SpendGold( GetItemPrice( type ) );
            _unlockedItems.Add( nextToUnlockItem.id );
            _unlockedItemsAmount[type]++;
            
            SaveUnlockedItems( );
            
            SelectItem( nextToUnlockItem );
            
            OnItemUnlocked.Invoke( nextToUnlockItem );
        }

        public bool IsItemUnlocked( UnlockableConfig item ) => _unlockedItems.Contains( item.id );

        public int GetItemPrice( EUnlockableType type ) => _config.unlockablePriceBase + _config.unlockablePriceIncrease * (_unlockedItemsAmount[type] - 1);

        public void SelectItem( UnlockableConfig item )
        {
            if ( !IsItemUnlocked( item ) )
                return;
            
            _selectedItems[item.type] = item;

            SaveSelectedItems( );
            
            OnItemSelected.Invoke( item );
        }

        public bool IsItemSelected( UnlockableConfig item ) => _selectedItems[item.type] == item;
        public UnlockableConfig GetSelectedItem( EUnlockableType type ) => _selectedItems[type];

        public List<UnlockableConfig> GetUnlockablesOfType( EUnlockableType type ) => _config.GetUnlockablesOfType( type );

        private void SaveUnlockedItems()
        {
            PlayerPrefs.SetString( UnlockablesPrefs.UnlockedItems, _unlockedItems.Aggregate( string.Empty, ( current, item ) => current + item + ";" ) );
        }

        private void SaveSelectedItems()
        {    
            
        }

        private void LoadAllItemsStatus( )
        {
            var unlockedItemsString = PlayerPrefs.GetString( UnlockablesPrefs.UnlockedItems );

            /*if ( string.IsNullOrEmpty( unlockedItemsString ) )
            {
                
            }
            
            unlockedItemsString = unlockedItemsString.Remove( unlockedItemsString.Length - 1 );*/

            _unlockedItems = new HashSet<string>( unlockedItemsString.Split( ';' ) );
            
            _selectedItems = new Dictionary<EUnlockableType, UnlockableConfig>( ) {
                { EUnlockableType.None, null }
            };

            _unlockedItemsAmount = new Dictionary<EUnlockableType, int>( ) {
                { EUnlockableType.None, 0 }
            };

            /*foreach ( var unlockedItem in _unlockedItems )
            {
                _unlockedItemsAmount[_config.GetUnlockable( unlockedItem ).type]++;
            }*/
        }
    }
}