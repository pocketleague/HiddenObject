using TMPro;
using UnityEngine;
using Zenject;

namespace Scripts.Gold
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class EarnedGoldAmountLabelView : MonoBehaviour
    {
        [Inject]
        private IGoldService _goldService;
        
        private TextMeshProUGUI _label;

        private void Awake()
        {
            _label = GetComponent<TextMeshProUGUI>();

            _goldService.OnGoldEarned += UpdateGoldLabel;
            
            UpdateGoldLabel( _goldService.GoldAmount );
        }

        private void OnDestroy()
        {
            _goldService.OnGoldEarned -= UpdateGoldLabel;
        }

        private void UpdateGoldLabel( int goldAmount )
        {
            _label.SetText( "+" + goldAmount );
        }
    }
}
