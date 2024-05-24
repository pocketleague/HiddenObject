using System;
using Scripts.Stages;
using UnityEngine;
using Zenject;

namespace Scripts.UI
{
    public class HideUIElementUntilStage : MonoBehaviour
    {
        public int showOnLevel = -1;
        
        private Transform _transformCached;

        [Inject]
        private void Construct( IStagesService stagesService )
        {
            _transformCached = transform;
            
            RefreshObject( stagesService.CurrentStageId, null );
            
            stagesService.OnStageSpawned += RefreshObject;
        }

        private void RefreshObject( int stageId, StageConfig stageConfig )
        {
            _transformCached.localScale = stageId >= showOnLevel ? Vector3.one : Vector3.zero;
        }
    }
}
