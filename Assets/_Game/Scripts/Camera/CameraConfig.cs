using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Camera
{
    [CreateAssetMenu( menuName = ( "Configs/CameraConfig" ), fileName = "CameraConfig" )]
    public class CameraConfig : ScriptableObject
    {
        public List<CameraStateConfig> cameraStates;
        public CameraView              cameraViewPrefab;

        private Dictionary<string, CameraStateConfig> _statesDictionary;

        public CameraStateConfig GetCameraForState( string stateId )
        {
            if ( _statesDictionary == null )
                GenerateStatesDictionary();

            if ( _statesDictionary.ContainsKey( stateId ) )
                return _statesDictionary[stateId];

            Debug.LogError( "[CAMERA] Missing camera state of id: " + stateId + ". Please add a config file of that state before trying to use it." );
            return null;
        }

        private void GenerateStatesDictionary()
        {
            _statesDictionary = new Dictionary<string, CameraStateConfig>();

            foreach ( var state in cameraStates ) {
                _statesDictionary.Add( state.stateId, state );
            }
        }
    }
}