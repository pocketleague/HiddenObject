using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Scripts.Core.HomeCenter
{
    [CreateAssetMenu(menuName = ("Installers/HomeCenterInstaller"), fileName = "HomeCenterInstaller")]
    public class HomeCenterInstaller : ScriptableObjectInstaller
    {
        public HomeCenterConfig config;

        public override void InstallBindings()
        {
            Container.BindInstance(config);
            Container.Bind<IHomeCenterService>().To<HomeCenterService>().AsSingle().NonLazy();
        }
    }
}