using UnityEngine;
using Zenject;

namespace Scripts.GameplayStates
{
    public class GameplayStateFinishedParticleSystemView : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem _particleSystem;

        [Inject]
        private IGameplayStatesService _gameplayStatesService;

        private void Awake()
        {
            _gameplayStatesService.OnStateFinished += FireParticles;
        }

        private void OnDestroy()
        {
            _gameplayStatesService.OnStateFinished -= FireParticles;
        }

        private void FireParticles( EGameplayState state, bool success )
        {
            if ( !success )
                return;
            
            _particleSystem.Play( );
        }

        private void Reset()
        {
            _particleSystem = GetComponent<ParticleSystem>( );
        }
    }
}
