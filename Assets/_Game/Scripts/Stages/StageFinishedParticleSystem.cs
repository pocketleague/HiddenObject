using System;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Scripts.Stages
{
    [RequireComponent(typeof(ParticleSystem))]
    public class StageFinishedParticleSystem : MonoBehaviour
    {
        private IStagesService _stagesService;
        private ParticleSystem _particleSystem;

        private bool  _isFiring;
        private float _timer;

        private const float _delayMin = 3f;
        private const float _delayMax = 5f;

        [Inject]
        private void Construct( IStagesService stagesService )
        {
            _stagesService  = stagesService;
            
            _particleSystem = GetComponent<ParticleSystem>( );

            _stagesService.OnStageFinished += FireParticles;
            _stagesService.OnStageSpawned  += StopParticles;
        }

        private void OnDestroy()
        {
            _stagesService.OnStageFinished -= FireParticles;
            _stagesService.OnStageSpawned  -= StopParticles;
        }

        private void StopParticles( int stageId, StageConfig stageConfig )
        {
            _isFiring = false;
        }

        private void Update()
        {
            HandleFireParticles( );

            void HandleFireParticles()
            {
                if ( !_isFiring )
                    return;

                _timer -= Time.deltaTime;

                if ( _timer > 0 )
                    return;
                
                _particleSystem.Play( );
                _timer = Random.Range( _delayMin, _delayMax );
            }
        }

        private void FireParticles( int stageId, bool success )
        {
            _timer    = 0f;
            _isFiring = success;
        }
    }
}
