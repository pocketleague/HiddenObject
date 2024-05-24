using Scripts.GameplayStates;
using UnityEngine;
using Zenject;

namespace Scripts.UI
{
    [RequireComponent(typeof(Window))]
    public class GameplayStateWindow : MonoBehaviour
    {
        [SerializeField]
        private Window _window;

        private IGameplayStatesService _gameplayStatesService;
        
        public EGameplayState state;

        [Inject]
        private void Construct( IGameplayStatesService gameplayStatesService )
        {
            _gameplayStatesService = gameplayStatesService;

            _gameplayStatesService.OnStateStarted  += CheckOpenWindow;
            _gameplayStatesService.OnStateFinished += CheckCloseWindow;
        }

        private void OnDestroy()
        {
            _gameplayStatesService.OnStateStarted  -= CheckOpenWindow;
            _gameplayStatesService.OnStateFinished -= CheckCloseWindow;
        }

        private void CheckOpenWindow( EGameplayState stateStarted )
        {
            if ( state != stateStarted )
                return;

            _window.Open( );
        }

        private void CheckCloseWindow( EGameplayState stateFinished, bool success )
        {
            if ( state != stateFinished )
                return;

            _window.Close( );
        }

        private void Reset()
        {
            _window = GetComponent<Window>( );
        }
    }
}
