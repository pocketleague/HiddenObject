using UnityEngine;
using Zenject;

namespace Scripts.Camera
{
    [CreateAssetMenu( menuName = ( "Installers/CameraInstaller" ), fileName = "CameraInstaller" )]
    public class CameraInstaller : ScriptableObjectInstaller
    {
        public CameraConfig config;

        public override void InstallBindings()
        {
            Container.BindInstance( config );
            Container.Bind<ICameraService>( ).To<CameraService>( ).AsSingle( ).NonLazy( );
        }
    }
}