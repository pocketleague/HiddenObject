using System.Collections.Generic;
using UnityEngine;

namespace Scripts.CLIK
{
    [System.Serializable]
    public struct ClikConfigurableFeature
    {
        public string featureId;
        public bool   featureState;

        public ClikConfigurableFeature( string id, bool state )
        {
            featureId    = id;
            featureState = state;
        }
    }

    [CreateAssetMenu( menuName = ( "Configs/ClikRemoteConfig" ), fileName = "ClikRemoteConfig" )]
    public class ClikRemoteConfig : ScriptableObject
    {
        public List<ClikConfigurableFeature> configurableFeatures;

        public void SetFeatureState( string featureId, bool value )
        {
            for ( int i = 0; i < configurableFeatures.Count; ++i ) {
                if ( configurableFeatures[i].featureId != featureId )
                    continue;

                configurableFeatures[i] = new ClikConfigurableFeature( featureId, value );
            }
        }

        public bool IsFeatureEnabled( string featureId )
        {
            foreach ( var feature in configurableFeatures ) {
                if ( feature.featureId != featureId )
                    continue;

                return feature.featureState;
            }

            return false;
        }
    }
}