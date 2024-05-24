using UnityEngine;
using Zenject;

namespace Scripts.Squishing
{
    [CreateAssetMenu(menuName = ("Installers/SquishingInstaller"), fileName = "SquishingInstaller")]
    public class SquishingInstaller : ScriptableObjectInstaller
    {
        public SquishingConfig config;

        public override void InstallBindings()
        {
            Container.BindInstance(config);
            Container.Bind<ISquishingService>().To<SquishingService>().AsSingle().NonLazy();
        }
    }
}
