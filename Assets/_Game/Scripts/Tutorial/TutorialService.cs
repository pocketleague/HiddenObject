using Scripts.GameLoop;
using Scripts.Stages;
using UnityEngine;
using Zenject;

namespace Scripts.Tutorial
{
    public class TutorialService : ITutorialService
    {
        private TutorialConfig      _config;
        private IStagesService      _stagesService;
        
        private TutorialPopUp       _popUp;
        private TutorialWindow      _window;
        private TutorialStateConfig _currentTutorial;

        private int   _currentTutorialIndex;
        private bool  _tutorialPending;
        private float _tutorialDelay;

        [Inject]
        private void Construct( TutorialConfig config, IStagesService stagesService, IPlayerLoop playerLoop )
        {
            _config        = config;
            _stagesService = stagesService;

            _stagesService.OnStageStarted += CheckTutorialForLevel;
            playerLoop.OnUpdateTick       += HandleTutorialDelay;
        }

        public void RegisterPopUp( TutorialPopUp popup ) => _popUp = popup;

        public void RegisterWindow( TutorialWindow window ) => _window = window;

        private void CheckTutorialForLevel( int stageId )
        {
            for ( var i = 0; i < _config.states.Count; ++i )
            {
                if ( _config.states[i].stageId == stageId )
                {
                    _currentTutorialIndex = i;
                    StartTutorial( _config.states[i], _config.states[i].startDelay );
                    return;
                }
            }
        }

        private void CheckForNextTutorial( )
        {
            if ( _currentTutorialIndex + 1 >= _config.states.Count )
                return;
            if ( _config.states[_currentTutorialIndex].stageId != _config.states[_currentTutorialIndex + 1].stageId )
                return;

            _currentTutorialIndex++;
            StartTutorial( _config.states[_currentTutorialIndex], _config.states[_currentTutorialIndex].startDelay - _config.states[_currentTutorialIndex - 1].startDelay );
        }

        private void HandleTutorialDelay()
        {
            if ( !_tutorialPending )
                return;

            _tutorialDelay -= Time.deltaTime;

            if ( _tutorialDelay > 0f )
                return;
            
            ShowTutorial( );
            _tutorialPending = false;
        }

        private void StartTutorial( TutorialStateConfig tutorial, float delay )
        {
            _currentTutorial = tutorial;
            _tutorialPending = true;
            _tutorialDelay   = delay;
        }

        private void ShowTutorial( )
        {
            if ( _currentTutorial.showWindow )
                _window.Show( _currentTutorial.windowTitle, _currentTutorial.windowBody, _currentTutorial.windowImage );
            if ( _currentTutorial.showPopUp )
                _popUp.Show( _currentTutorial.popUpText );
            if ( _currentTutorial.pauseGame )
                Time.timeScale = 0f;
        }

        public void EndTutorial( )
        {
            if ( _currentTutorial.showWindow )
                _window.Close( );
            if ( _currentTutorial.showPopUp )
                _popUp.Close( );
            if ( _currentTutorial.pauseGame )
                Time.timeScale = 1f;
            
            _currentTutorial = null;

            CheckForNextTutorial( );
        }
    }
}
