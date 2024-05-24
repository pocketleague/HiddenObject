using UnityEngine;
using Zenject;

namespace Scripts.Gold
{
    [CreateAssetMenu( menuName = ( "Installers/GoldInstaller" ), fileName = "GoldInstaller" )]
    public class GoldInstaller : ScriptableObjectInstaller
    {
        public GoldConfig config;

        public override void InstallBindings()
        {
            Container.BindInstance( config );
            Container.Bind<IGoldService>().To<GoldService>().AsSingle();
        }
    }
}