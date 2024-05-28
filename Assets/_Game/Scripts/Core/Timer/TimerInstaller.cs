using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Scripts.Timer
{ 
    [CreateAssetMenu(menuName = ("Installers/TimerInstaller"), fileName = "TimerInstaller")]
    public class TimerInstaller : ScriptableObjectInstaller
    {
        public TimerConfig config;

        public override void InstallBindings()
        {
            Container.BindInstance(config);
            Container.Bind<ITimerService>().To<TimerService>().AsSingle().NonLazy();
        }
    }
}
