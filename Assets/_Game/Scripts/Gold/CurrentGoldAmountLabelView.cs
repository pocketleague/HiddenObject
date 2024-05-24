using TMPro;
using UnityEngine;
using Zenject;

namespace Scripts.Gold
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class CurrentGoldAmountLabelView : MonoBehaviour
    {
        [Inject]
        private IGoldService _goldService;
        
        private TextMeshProUGUI _label;

        private void Awake()
        {
            _label = GetComponent<TextMeshProUGUI>();

            _goldService.OnGoldAmountChanged += UpdateGoldLabel;
            
            UpdateGoldLabel( _goldService.GoldAmount );
        }

        private void OnDestroy()
        {
            _goldService.OnGoldAmountChanged -= UpdateGoldLabel;
        }

        private void UpdateGoldLabel( int goldAmount )
        {
            _label.SetText( goldAmount.ToString() );
        }
    }
}
