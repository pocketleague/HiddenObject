using System;

namespace Scripts.Stages
{
    public interface IStagesService
    {
        event Action<int, StageConfig> OnStageSpawned;
        event Action<int>              OnStageStarted;
        event Action<float>            OnStageProgressChanged;
        event Action<int, bool>        OnStageFinished;
        event Action<int>              OnStageSkipped;

        StageConfig CurrentStage   { get; }
        int         CurrentStageId { get; }

        void SpawnCurrentStage();
        void StartCurrentStage();
        void SkipCurrentStage();
        void FinishStageSuccess( float delay = 0f );
        void FinishStageFailure( float delay = 0f );
    }
}
