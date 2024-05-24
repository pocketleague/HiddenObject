using UnityEngine;
using Zenject;

namespace Scripts.Audio
{
    [CreateAssetMenu( fileName = "AudioInstaller", menuName = "Installers/AudioInstaller" )]
    public class AudioInstaller : ScriptableObjectInstaller
    {
        public MusicConfig musicConfig;
        public SfxConfig   sfxConfig;

        public override void InstallBindings()
        {
            Container.BindInstance( musicConfig );
            Container.BindInstance( sfxConfig );

            Container.Bind<IMusicService>( ).To<MusicService>( ).AsSingle( ).NonLazy( );
            Container.Bind<ISfxService>( ).To<SfxService>( ).AsSingle( ).NonLazy( );
        }
    }
}
