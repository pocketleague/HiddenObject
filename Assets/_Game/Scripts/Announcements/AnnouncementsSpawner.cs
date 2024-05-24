using UnityEngine;
using Zenject;

namespace Scripts.Announcements
{
    public class AnnouncementsSpawner : MonoBehaviour
    {
        private AnnouncementsConfig _config;
        private Transform           _transformCached;

        [Inject]
        private void Construct( AnnouncementsConfig config, IAnnouncementsService service )
        {
            _config          = config;
            _transformCached = transform;
            
            service.RegisterLabelSpawner( this );
        }

        public void SpawnAnnouncement( string text, Color color, Vector2 position )
        {
            var spawnPosition = new Vector2( Screen.width * position.x, Screen.height * position.y );
            
            Instantiate( _config.labelPrefab, spawnPosition, Quaternion.identity, _transformCached ).Show( text, color );
        }
    }
}
