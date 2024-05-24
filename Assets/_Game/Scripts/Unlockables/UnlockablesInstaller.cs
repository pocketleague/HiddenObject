using UnityEngine;
using Zenject;

namespace Scripts.Unlockables
{
    [CreateAssetMenu( fileName = "UnlockablesInstaller", menuName = "Installers/UnlockablesInstaller" )]
    public class UnlockablesInstaller : ScriptableObjectInstaller
    {
        public UnlockablesConfig config;

        public override void InstallBindings()
        {
            config.Init( );
            
            Container.BindInstance( config );
            Container.Bind<IUnlockablesService>( ).To<UnlockablesService>( ).AsSingle( ).NonLazy( );
        }
    }
}
