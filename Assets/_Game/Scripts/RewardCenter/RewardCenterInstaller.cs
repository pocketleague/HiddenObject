using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace RewardCenter
{
    [CreateAssetMenu(menuName = ("Installers/RewardCenterInstaller"), fileName = "RewardCenterInstaller")]
    public class RewardCenterInstaller : ScriptableObjectInstaller
    {
        public RewardCenterConfig config;

        public override void InstallBindings()
        {
            Container.BindInstance(config);
            Container.Bind<IRewardCenterService>().To<RewardCenterService>().AsSingle().NonLazy();
        }
    }
}
