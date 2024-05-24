using Scripts.Stages;
using UnityEngine;
using Zenject;

namespace Scripts.Announcements
{
    public class AnnouncementsService : IAnnouncementsService
    {
        private AnnouncementsConfig  _config;
        private AnnouncementsSpawner _spawner;

        private bool _canShow;

        [Inject]
        private void Construct( AnnouncementsConfig config, IStagesService stagesService )
        {
            _config = config;
        }

        public void RegisterLabelSpawner( AnnouncementsSpawner spawner ) => _spawner = spawner;
        
        public void Show( string text )
        {
            Show( text, Color.white, Vector2.one * 0.5f );
        }

        public void Show( string text, Color color )
        {
            Show( text, color, Vector2.one * 0.5f );
        }

        public void Show( string text, Vector2 position )
        {
            Show( text, Color.white, position );
        }

        public void Show( string text, Color color, Vector2 position )
        {
            if ( !_canShow )
                return;
            
            _spawner.SpawnAnnouncement( text, color, position );
        }
    }
}
