using UnityEngine;
using Zenject;

namespace Scripts.Ads
{
    [CreateAssetMenu( menuName = ( "Installers/AdsInstaller" ), fileName = "AdsInstaller" )]
    public class AdsInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IAdsService>().To<AdsService>().AsSingle();
        }
    }
}