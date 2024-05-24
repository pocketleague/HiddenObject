using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Unlockables
{
    public class UnlockableTabsView : MonoBehaviour
    {
        private List<UnlockableTabButtonView> _tabButtons;
        private List<UnlockableTabView>       _tabWindows;

        private EUnlockableType _currentTab;

        private void Awake()
        {
            _currentTab = EUnlockableType.None;

            _tabButtons = new List<UnlockableTabButtonView>( GetComponentsInChildren<UnlockableTabButtonView>( ) );
            _tabWindows = new List<UnlockableTabView>( GetComponentsInChildren<UnlockableTabView>( ) );

            foreach( var tabButton in _tabButtons )
            {
                tabButton.OnClicked += ChangeTab;
            }
        }

        private void Start()
        {
            RefreshTabs(  );
        }

        private void ChangeTab( EUnlockableType newTab )
        {
            if ( _currentTab == newTab )
                return;
            
            _currentTab = newTab;

            RefreshTabs( );
        }

        private void RefreshTabs()
        {
            foreach( var button in _tabButtons )
            {
                button.SetState( _currentTab == button.type );
            }
            foreach( var tab in _tabWindows )
            {
                tab.SetState( _currentTab == tab.type );
            }
        }
    }
}
