using UnityEngine;
using Zenject;

namespace Scripts.PlayerControl
{
    [CreateAssetMenu( menuName = ( "Installers/PlayerControlInstaller" ), fileName = "PlayerControlInstaller" )]
    public class PlayerControlInstaller : ScriptableObjectInstaller
    {
        public PlayerControlConfig config;

        public override void InstallBindings()
        {
            Container.BindInstance( config );
            Container.Bind<IPlayerControlService>( ).To<PlayerControlService>( ).AsSingle( ).NonLazy( );
        }
    }
}