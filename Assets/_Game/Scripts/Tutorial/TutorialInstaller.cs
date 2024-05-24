using UnityEngine;
using Zenject;

namespace Scripts.Tutorial
{
    [CreateAssetMenu( menuName = ( "Installers/TutorialInstaller" ), fileName = "TutorialInstaller" )]
    public class TutorialInstaller : ScriptableObjectInstaller
    {
        public TutorialConfig config;

        public override void InstallBindings()
        {
            Container.BindInstance( config );
            Container.Bind<ITutorialService>().To<TutorialService>().AsSingle().NonLazy( );
        }
    }
}