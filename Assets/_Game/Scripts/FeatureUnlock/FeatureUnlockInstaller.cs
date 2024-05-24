using UnityEngine;
using Zenject;

namespace Scripts.FeatureUnlock
{
    [CreateAssetMenu( fileName = "FeatureUnlockInstaller", menuName = "Installers/FeatureUnlockInstaller" )]
    public class FeatureUnlockInstaller : ScriptableObjectInstaller
    {
        public FeaturesUnlockConfig config;

        public override void InstallBindings()
        {
            Container.BindInstance( config );
            Container.Bind<IFeatureUnlockService>( ).To<FeatureUnlockService>().AsSingle( ).NonLazy( );
        }
    }
}
