using System.Collections.Generic;
using Scripts.UI;
using UnityEngine;
using Zenject;

namespace Scripts.Unlockables
{
    public class UnlockableWidgetsLayoutView : MonoBehaviour
    {
        public EUnlockableType type;

        private IUnlockablesService        _unlockablesService;
        private UnlockablesConfig          _config;
        private List<UnlockableWidgetView> _spawnedUnlockables;

        private Transform _transformCached;
        private Window    _parentWindow;
        
        [Inject]
        private void Construct( UnlockablesConfig config, IUnlockablesService unlockablesService )
        {
            _config             = config;
            _unlockablesService = unlockablesService;
            _transformCached    = transform;
            _parentWindow       = GetComponentInParent<Window>( );

            SpawnUnlockablesWidgets( );

            unlockablesService.OnItemUnlocked += RefreshAllWidgets;
            unlockablesService.OnItemSelected += RefreshAllWidgets;
            _parentWindow.Opened              += () => RefreshAllWidgets( null );
        }

        private void SpawnUnlockablesWidgets( )
        {
            _spawnedUnlockables = new List<UnlockableWidgetView>( );
            
            foreach( var item in _config.GetUnlockablesOfType( type ) )
            {
                var spawnedWidget = Instantiate( _config.widgetPrefab, _transformCached );
                spawnedWidget.Initialize( item );
                _spawnedUnlockables.Add( spawnedWidget );

                spawnedWidget.OnItemClicked += SelectItem;
            }
        }

        private void RefreshAllWidgets( UnlockableConfig selectedUnlockable )
        {
            foreach( var widget in _spawnedUnlockables )
            {
                widget.RefreshState( _unlockablesService.IsItemUnlocked( widget.item ), _unlockablesService.IsItemSelected( widget.item ) );
            }
        }

        private void SelectItem( UnlockableConfig item )
        {
            _unlockablesService.SelectItem( item );
        }
    }
}
