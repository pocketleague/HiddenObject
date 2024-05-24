using UnityEngine;
using Zenject;

namespace Scripts.GameLoop
{
    [CreateAssetMenu( menuName = ( "Installers/PlayerLoopInstaller" ), fileName = "PlayerLoopInstaller" )]
    public class PlayerLoopInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IPlayerLoop>( ).To<PlayerLoop>( ).AsSingle( ).NonLazy( );
        }
    }
}