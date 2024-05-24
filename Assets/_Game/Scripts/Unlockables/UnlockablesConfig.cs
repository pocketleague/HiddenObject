using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.Unlockables
{
    [CreateAssetMenu( fileName = "UnlockablesConfig", menuName = "Configs/UnlockablesConfig" )]
    public class UnlockablesConfig : ScriptableObject
    {
        public List<UnlockableConfig> unlockables;
        public List<UnlockableConfig> unlockablesOrder;
        public UnlockableWidgetView   widgetPrefab;

        private Dictionary<EUnlockableType, UnlockableConfig> _defaultUnlockables;

        public int unlockablePriceBase     = 1000;
        public int unlockablePriceIncrease = 500;

        private Dictionary<EUnlockableType, List<UnlockableConfig>> _cachedUnlockables;
        private Dictionary<EUnlockableType, List<UnlockableConfig>> _cachedUnlockablesOrder;
        

        public void Init()
        {
            _cachedUnlockables = new Dictionary<EUnlockableType, List<UnlockableConfig>>( ) {
                { EUnlockableType.None, new List<UnlockableConfig>( ) }
            };
            
            _cachedUnlockablesOrder = new Dictionary<EUnlockableType, List<UnlockableConfig>>( ) {
                 { EUnlockableType.None, new List<UnlockableConfig>( ) }
            };

            _defaultUnlockables = new Dictionary<EUnlockableType, UnlockableConfig>( );

            foreach ( var unlockable in unlockables )
            {
                _cachedUnlockables[unlockable.type].Add( unlockable );
            }
            
            foreach ( var unlockable in unlockablesOrder )
            {
                _cachedUnlockablesOrder[unlockable.type].Add( unlockable );
                
                if ( !_defaultUnlockables.ContainsKey( unlockable.type ) )
                    _defaultUnlockables.Add( unlockable.type, unlockable );
            }
        }

        public List<UnlockableConfig> GetUnlockablesOfType( EUnlockableType type ) => _cachedUnlockables[type];
        
        public UnlockableConfig GetUnlockable( string id ) => unlockables.FirstOrDefault( u => u.id == id );

        public UnlockableConfig GetNextUnlockableToUnlock( EUnlockableType type, int currentlyUnlocked ) => _cachedUnlockablesOrder[type][currentlyUnlocked];

        public UnlockableConfig GetDefaultUnlockableOfType( EUnlockableType type ) => _defaultUnlockables[type];
    }
}