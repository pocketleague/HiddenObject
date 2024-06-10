using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Scripts.Core.StateManager
{
    [CreateAssetMenu(menuName = ("Installers/StateManagerInstaller"), fileName = "StateManagerInstaller")]
    public class StateManagerInstaller : ScriptableObjectInstaller
    {
        public StateManagerConfig config;

        public override void InstallBindings()
        {
            Container.BindInstance(config);
            Container.Bind<IStateManagerService>().To<StateManagerService>().AsSingle().NonLazy();
        }
    }
}
