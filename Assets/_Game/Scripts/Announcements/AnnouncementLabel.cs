using TMPro;
using UnityEngine;

namespace Scripts.Announcements
{
    public class AnnouncementLabel : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _label;

        public void Show( string text, Color color )
        {
            _label.SetText( text );
            _label.color = color;
        }
    }
}
