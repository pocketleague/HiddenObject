using System.Collections.Generic;
using UnityEngine;

namespace Scripts.GameplayStates
{
    [CreateAssetMenu( fileName = "GameplayStatesConfig", menuName = "Configs/GameplayStatesConfig")]
    public class GameplayStatesConfig : ScriptableObject
    {
        [Header( "UI" )]
        public List<GameplayStateData> states;
        public GameplayStateWidget widgetPrefab;
        public GameObject          connectorPrefab;

        [Header( "Enter State Delays" )]
        public float                    enterFirstStateDelay = 0.5f;
        public float                    defaultEnterStateDelay = 2f;
        public List<GameplayStateDelay> customEnterStateDelays;
        
        [Header( "End Game Delays")]
        public float                    defaultEndGameDelay = 2f;
        public List<GameplayStateDelay> customEndGameOnStateDelays;

        public float tyingHairDelay = 8f;

        private Dictionary<EGameplayState, float>  _delayPerState;
        private Dictionary<EGameplayState, float>  _delayGameEndPerState;
        private Dictionary<EGameplayState, Sprite> _stateIcons;

        public void Initialize()
        {
            _delayPerState        = new Dictionary<EGameplayState, float>( );
            _delayGameEndPerState = new Dictionary<EGameplayState, float>( );
            _stateIcons           = new Dictionary<EGameplayState, Sprite>( );

            foreach ( var customDelay in customEnterStateDelays )
                _delayPerState.Add( customDelay.state, customDelay.delay );
            
            foreach ( var customDelay in customEndGameOnStateDelays )
                _delayGameEndPerState.Add( customDelay.state, customDelay.delay );
            
            foreach( var state in states )
                _stateIcons.Add( state.state, state.sprite );
        }

        public float GetEnterDelayForState( EGameplayState state ) => _delayPerState.ContainsKey( state ) ? _delayPerState[state] : defaultEnterStateDelay;
        
        public float GetEndGameDelayForState( EGameplayState state ) => _delayGameEndPerState.ContainsKey( state ) ? _delayGameEndPerState[state] : defaultEndGameDelay;

        public Sprite GetSpriteForState( EGameplayState state ) => _stateIcons[state];
    }
}
