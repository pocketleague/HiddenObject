using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Announcements
{
    [CreateAssetMenu( fileName = "AnnouncementsConfig", menuName = "Configs/AnnouncementsConfig" )]
    public class AnnouncementsConfig : ScriptableObject
    {
        public AnnouncementLabel labelPrefab;
    }
}