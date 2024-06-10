using UnityEngine;
using Zenject;

namespace Scripts.Core.GameplayCenter
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
