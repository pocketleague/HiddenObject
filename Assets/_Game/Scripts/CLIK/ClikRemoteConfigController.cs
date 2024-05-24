using System;
using System.Collections;
using UnityEngine;

namespace Scripts.CLIK
{
    public class ClikRemoteConfigController : MonoBehaviour
    {
        public static Action<bool> RemoteConfigLoaded = delegate{ };

        public ClikRemoteConfig remoteConfigFile;

        private const float _timeoutInSeconds = 4f;
        private       bool  _resolved         = false;

        private void Awake()
        {
            if ( PlayerPrefs.HasKey( "overrideTestGroup" ) ) {
                string[] features = PlayerPrefs.GetString("overrideTestGroup").Split(';');

                for ( int i = 0; i < remoteConfigFile.configurableFeatures.Count; ++i ) {
                    remoteConfigFile.SetFeatureState( remoteConfigFile.configurableFeatures[i].featureId, features[i] == "1" );
                }
                Debug.Log( "RemoteConfig: overriden by App" );
                return;
            }

            StartCoroutine( CheckTimeOutCoroutine(_timeoutInSeconds) );
#if TTP_CORE
        Tabtale.TTPlugins.TTPAnalytics.OnRemoteFetchCompletedEvent += OnRemoteConfigReady;
#endif
        }

        private void OnDestroy()
        {
#if TTP_CORE
        Tabtale.TTPlugins.TTPAnalytics.OnRemoteFetchCompletedEvent -= OnRemoteConfigReady;
#endif
        }

        private void Start()
        {
            if ( PlayerPrefs.HasKey( "overrideTestGroup" ) ) {
                RemoteConfigLoaded.Invoke( false );
            }
        }

        private void OnRemoteConfigReady( bool isReady )
        {
            if ( _resolved )
                return;

            Debug.Log( "RemoteConfig: OnRemoteConfigReady - " + isReady.ToString() );
            _resolved = true;

            ApplyRemoteConfiguration();
        
            Debug.Log( "RemoteConfig: Remote Configuration applied succesfully!" );

            RemoteConfigLoaded.Invoke( true );
        }

        IEnumerator CheckTimeOutCoroutine(float remainingTime)
        {
#if TTP_CORE
        if ( Tabtale.TTPlugins.TTPAnalytics.IsRemoteFetchComplete() )
        {
            Debug.Log("RemoteConfig: Remote ready : " + Tabtale.TTPlugins.TTPAnalytics.IsRemoteFetchComplete().ToString() + " remaining time: " + remainingTime);
            OnRemoteConfigReady( true );

            yield return null;
        }
        else
        {
            if (remainingTime <= 0)
            {
                Debug.Log("RemoteConfig: OnRemoteConfigTimeout");
                _resolved = true;

                ApplyRemoteConfiguration();
                Debug.Log( "RemoteConfig: Remote Configuration applied on timeout!" );

                RemoteConfigLoaded.Invoke( false );
                yield return null;
            }
            else
            {
                yield return new WaitForSeconds(1);
                StartCoroutine(CheckTimeOutCoroutine(remainingTime - 1));
            }

        }
#endif
            yield return null;
        }

        private void ApplyRemoteConfiguration()
        {
            if ( remoteConfigFile == null ) {
                Debug.Log( "RemoteConfig: Remote Config File is missing. Please create and provide remote config file to the controller." );
                return;
            }

            for ( int i = 0; i < remoteConfigFile.configurableFeatures.Count; ++i ) {
                remoteConfigFile.SetFeatureState( remoteConfigFile.configurableFeatures[i].featureId, GetBool( remoteConfigFile.configurableFeatures[i].featureId, remoteConfigFile.configurableFeatures[i].featureState ) );
            }
        }

        private bool GetBool( string key, bool defaultVal )
        {
            Debug.Log( "SetDefaultConfig:: Enter" );
            var stringVal = string.Empty;
#if TTP_CORE
        stringVal = Tabtale.TTPlugins.TTPAnalytics.GetStringValue(key);
#endif

            if ( String.IsNullOrEmpty( stringVal ) ) {
                Debug.LogWarning( $"Can't find firebase value for key {key}, returning default value ({defaultVal})" );
                return defaultVal;
            }

            if ( bool.TryParse( stringVal, out var result ) ) {
                Debug.Log( "Returning remote bool value : " + key + " , " + result.ToString() );
                return result;
            }

            if ( stringVal == "true" ) {
                Debug.Log( "Returning remote string value : " + key + " , true" );
                return true;
            }
            if ( stringVal == "false" ) {
                Debug.Log( "Returning remote string value : " + key + " , false" );
                return false;
            }
            if ( stringVal == "1" ) {
                Debug.Log( "Returning remote string value : " + key + " , 1" );
                return true;
            }
            if ( stringVal == "0" ) {
                Debug.Log( "Returning remote string value : " + key + " , 0" );
                return false;
            }

            Debug.LogWarning( $"Can't parse firebase value {stringVal} for key {key}, returning default value ({defaultVal})" );
            Debug.Log( "SetDefaultConfig:: Exit: Key = " + key + " Value =" + defaultVal );
            return defaultVal;
        }
    }
}