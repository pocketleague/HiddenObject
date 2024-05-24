using Scripts.Gold;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts.Unlockables
{
    [RequireComponent(typeof(Button))]
    public class UnlockablePurchaseButtonView : MonoBehaviour
    {
        public EUnlockableType type;
        
        private IUnlockablesService _unlockablesService;
        private IGoldService        _goldService;
        
        private Button          _buttonCached;
        private TextMeshProUGUI _costLabel;

        [Inject]
        private void Construct( IUnlockablesService unlockablesService, IGoldService goldService )
        {
            _unlockablesService = unlockablesService;
            _goldService        = goldService;
            _buttonCached       = GetComponent<Button>( );
            _costLabel          = GetComponentInChildren<TextMeshProUGUI>( );
            
            _buttonCached.onClick.AddListener( DispatchPurchaseRequest );
            _goldService.OnGoldAmountChanged   += ( g ) => RefreshButton( );
            _unlockablesService.OnItemUnlocked += ( i ) => RefreshButton( );
        }

        private void Start()
        {
            RefreshButton( );
        }

        private void OnDestroy()
        {
            _buttonCached.onClick.RemoveListener( DispatchPurchaseRequest );
        }

        private void DispatchPurchaseRequest()
        {
            _unlockablesService.UnlockItem( type );
        }

        private void RefreshButton()
        {
            _costLabel.SetText( _unlockablesService.GetItemPrice( type ).ToString( ) );
            _buttonCached.interactable = _goldService.HasGold( _unlockablesService.GetItemPrice( type ) );
        }
    }
}
