using UnityEngine;
using Zenject;

namespace Scripts.Analytics
{
    [CreateAssetMenu( menuName = ( "Installers/AnalyticsInstaller" ), fileName = "AnalyticsInstaller" )]
    public class AnalyticsInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<AnalyticsService>().AsSingle().NonLazy();
        }
    }
}