using UnityEngine;
using Zenject;

namespace Scripts.FluidInjection
{
    [CreateAssetMenu(menuName = ("Installers/FluidInjectionInstaller"), fileName = "FluidInjectionInstaller")]
    public class FluidInjectionInstaller : ScriptableObjectInstaller
    {
        public FluidInjectionConfig config;

        public override void InstallBindings()
        {
            Container.BindInstance(config);
            Container.Bind<IFluidInjectionService>().To<FluidInjectionService>().AsSingle().NonLazy();
        }
    }
}
