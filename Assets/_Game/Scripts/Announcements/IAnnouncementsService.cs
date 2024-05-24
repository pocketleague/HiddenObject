using UnityEngine;

namespace Scripts.Announcements
{
    public interface IAnnouncementsService
    {
        void RegisterLabelSpawner( AnnouncementsSpawner spawner );

        void Show( string text );
        void Show( string text, Color   color );
        void Show( string text, Vector2 position );
        void Show( string text, Color   color, Vector2 position );
    }
}
