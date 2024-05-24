using System.Collections.Generic;
using Scripts.Stages;
using UnityEngine;
using Zenject;

namespace Scripts.GameplayStates
{
    public class GameplayStatesLayoutView : MonoBehaviour
    {
        [SerializeField]
        private Transform _transformCached;

        private GameplayStatesConfig   _config;
        private IStagesService         _stagesService;
        private IGameplayStatesService _gameplayStatesService;
        
        private List<GameplayStateWidget> _spawnedWidgets;
        private List<GameObject>          _spawnedConnectors;

        [Inject]
        private void Construct( GameplayStatesConfig config, IStagesService stagesService, IGameplayStatesService gameplayStatesService )
        {
            _config                = config;
            _stagesService         = stagesService;
            _gameplayStatesService = gameplayStatesService;
            _spawnedWidgets        = new List<GameplayStateWidget>( );
            _spawnedConnectors     = new List<GameObject>( );

            _stagesService.OnStageStarted          += _ => SpawnWidgets( );
            _stagesService.OnStageFinished         += ( _, _ ) => DespawnWidgets( );
            _gameplayStatesService.OnStateFinished += ( _, _ ) => RefreshStatesOnStateFinished( _gameplayStatesService.CurrentStateId );
            _gameplayStatesService.OnStateStarted  += _ => RefreshStatesOnStateStarted( _gameplayStatesService.CurrentStateId );
        }

        private void SpawnWidgets( )
        {
            DespawnWidgets( );

            for ( var i = 0; i < _stagesService.CurrentStage.states.Count; ++i )
            {
                var spawnedState = Instantiate( _config.widgetPrefab, _transformCached );
                spawnedState.Initialize( _config.GetSpriteForState( _stagesService.CurrentStage.states[i] ) );
                _spawnedWidgets.Add( spawnedState );

                if ( i + 1 == _stagesService.CurrentStage.states.Count )
                    break;
                
                _spawnedConnectors.Add( Instantiate( _config.connectorPrefab, _transformCached ) );
            }
            
            RefreshStatesOnStateStarted( 0 );
        }

        private void RefreshStatesOnStateFinished( int currentStateId )
        {
            for ( var i = 0; i < _spawnedWidgets.Count; ++i )
            {
                _spawnedWidgets[i].Refresh( currentStateId >= i, false );
            }
        }
        
        private void RefreshStatesOnStateStarted( int currentStateId )
        {
            for ( var i = 0; i < _spawnedWidgets.Count; ++i )
            {
                _spawnedWidgets[i].Refresh( currentStateId > i, currentStateId == i );
            }
        }

        private void DespawnWidgets( )
        {
            foreach ( var widget in _spawnedWidgets )
            {
                Destroy( widget.gameObject );
            }

            _spawnedWidgets.Clear( );

            foreach ( var connector in _spawnedConnectors )
            {
                Destroy( connector.gameObject );
            }
            
            _spawnedConnectors.Clear( );
        }

        private void Reset()
        {
            _transformCached = transform;
        }
    }
}
