using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Scripts.Core.LevelSelection
{
    [CreateAssetMenu(menuName = ("Installers/LevelSelectionInstaller"), fileName = "LevelSelectionInstaller")]
    public class LevelSelectionInstaller : ScriptableObjectInstaller
    {
        public LevelSelectionConfig config;

        public override void InstallBindings()
        {
            Container.BindInstance(config);
            Container.Bind<ILevelSelectionService>().To<LevelSelectionService>().AsSingle().NonLazy();
        }
    }
}
