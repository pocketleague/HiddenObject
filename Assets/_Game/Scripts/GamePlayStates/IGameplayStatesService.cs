using System;

namespace Scripts.GameplayStates
{
    public interface IGameplayStatesService
    {
        event Action<EGameplayState>       OnStatePending;
        event Action<EGameplayState>       OnStateStarted;
        event Action<EGameplayState, bool> OnStateFinished;
        event Action                       OnAllStatesFinished;
        
        int            CurrentStateId { get; }
        EGameplayState CurrentState   { get; }
        EGameplayState PreviousState  { get; }

        void  WinCurrentState( );
        void  FailCurrentState( );
        float GetEndGameDelayAfterLastState( );
    }
}
