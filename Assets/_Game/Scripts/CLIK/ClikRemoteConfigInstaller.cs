using UnityEngine;
using Zenject;

namespace Scripts.CLIK
{
    [CreateAssetMenu( menuName = ( "Installers/ClikRemoteConfigInstaller" ), fileName = "ClikRemoteConfigInstaller" )]
    public class ClikRemoteConfigInstaller : ScriptableObjectInstaller
    {
        public ClikRemoteConfig config;

        public override void InstallBindings()
        {
            Container.BindInstance( config );
        }
    }
}