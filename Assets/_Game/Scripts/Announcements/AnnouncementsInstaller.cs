using UnityEngine;
using Zenject;

namespace Scripts.Announcements
{
    [CreateAssetMenu( fileName = "AnnouncementsInstaller", menuName = "Installers/AnnouncementsInstaller" )]
    public class AnnouncementsInstaller : ScriptableObjectInstaller
    {
        public AnnouncementsConfig config;

        public override void InstallBindings()
        {
            Container.BindInstance( config );
            Container.Bind<IAnnouncementsService>( ).To<AnnouncementsService>( ).AsSingle( ).NonLazy( );
        }
    }
}
