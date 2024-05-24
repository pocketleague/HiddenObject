using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.CLIK
{
    public class ClikGameLoader : MonoBehaviour
    {
        public bool   waitForRemoteConfiguration = false;
        public string coreSceneName              = "Core";

        public List<string> additionalScenesToLoad;

        private void Awake()
        {
            ClikRemoteConfigController.RemoteConfigLoaded += OnRemoteConfigLoaded;

            SceneManager.sceneLoaded += HandleSceneLoaded;
        }

        private void OnDestroy()
        {
            ClikRemoteConfigController.RemoteConfigLoaded -= OnRemoteConfigLoaded;
            
            SceneManager.sceneLoaded -= HandleSceneLoaded;
        }

        private void Start()
        {
            if ( waitForRemoteConfiguration )
                return;

            HandleLoadScenes( );
        }

        private void OnRemoteConfigLoaded( bool success )
        {
            if ( !waitForRemoteConfiguration )
                return;

            HandleLoadScenes( );
        }

        private void HandleLoadScenes()
        {
            SceneManager.LoadSceneAsync( coreSceneName, LoadSceneMode.Additive );
        }

        private void HandleSceneLoaded( Scene loadedScene, LoadSceneMode loadMode )
        {
            if ( loadedScene.name != coreSceneName ) 
                return;
            
            foreach ( var additionalScene in additionalScenesToLoad )
            {
                SceneManager.LoadSceneAsync( additionalScene, LoadSceneMode.Additive );
            }

            SceneManager.UnloadSceneAsync( 0 );
        }
    }
}
