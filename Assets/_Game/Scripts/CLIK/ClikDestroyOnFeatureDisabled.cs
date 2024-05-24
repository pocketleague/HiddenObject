using UnityEngine;
using Zenject;

namespace Scripts.CLIK
{
    public class ClikDestroyOnFeatureDisabled : MonoBehaviour
    {
        [Inject]
        private ClikRemoteConfig _remoteConfig;

        public string featureId;

        private void Start()
        {
            if ( !_remoteConfig.IsFeatureEnabled( featureId ) )
                Destroy( gameObject );
        }
    }
}
