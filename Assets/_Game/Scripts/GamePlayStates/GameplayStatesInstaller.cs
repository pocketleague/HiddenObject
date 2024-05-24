using UnityEngine;
using Zenject;

namespace Scripts.GameplayStates
{
    [CreateAssetMenu( menuName = ( "Installers/GameplayStatesInstaller" ), fileName = "GameplayStatesInstaller" )]
    public class GameplayStatesInstaller : ScriptableObjectInstaller
    {
        public GameplayStatesConfig config;
        
        public override void InstallBindings()
        {
            config.Initialize( );
            
            Container.BindInstance( config );
            Container.Bind<IGameplayStatesService>( ).To<GameplayStatesService>( ).AsSingle( ).NonLazy( );
        }
    }
}