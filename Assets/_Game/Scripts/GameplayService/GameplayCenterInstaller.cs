using UnityEngine;
using Zenject;

namespace GameplayCenter
{
    [CreateAssetMenu(menuName = ("Installers/GameplayInstaller"), fileName = "GameplayInstaller")]
    public class GameplayCenterInstaller : ScriptableObjectInstaller
    {
        public GameplayCenterConfig config;

        public override void InstallBindings()
        {
            Container.BindInstance(config);
            Container.Bind<IGameplayCenterService>().To<GameplayCenterService>().AsSingle().NonLazy();
        }
    }
}
