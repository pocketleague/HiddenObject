using TMPro;
using UnityEngine;

namespace Scripts.CLIK
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class ClikRemoteVersionLabel : MonoBehaviour
    {
        private TextMeshProUGUI _label;

        public ClikRemoteConfig configFile;

        private void Awake()
        {
            _label = GetComponent<TextMeshProUGUI>();
            _label.SetText( GenerateConfigString() );
        }

        private string GenerateConfigString()
        {
            string _configString = "v." + Application.version + " - ";
            for ( int i = 0; i < configFile.configurableFeatures.Count; ++i ) {
                _configString += configFile.configurableFeatures[i].featureState ? "1" : "0";
                if ( i != configFile.configurableFeatures.Count - 1 )
                    _configString += ".";
            }

            return _configString;
        }
    }
}
