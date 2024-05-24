using UnityEngine;

namespace Scripts.Stages
{
    public class StageSpawner : MonoBehaviour
    {
        private IStagesService _stagesService;
        
        public void Initialize( IStagesService stagesService )
        {
            _stagesService = stagesService;
        }

        private void Start()
        {
            _stagesService.SpawnCurrentStage( );
        }
    }
}
