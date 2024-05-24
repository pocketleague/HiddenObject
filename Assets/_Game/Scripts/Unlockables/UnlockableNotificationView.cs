using Scripts.Gold;
using Scripts.Unlockables;
using UnityEngine;
using Zenject;

namespace Scripts.GiftShop
{
    public class UnlockableNotificationView : MonoBehaviour
    {
        public EUnlockableType type;
        
        private Transform           _transformCached;
        private IUnlockablesService _unlockablesService;
        private IGoldService        _goldService;

        [SerializeField]
        private Transform _animatedTransform;

        private bool _isVisible;

        private const float _animationMagnitude = 8f;
        private const float _animationFrequency = 5f;
        
        [Inject]
        private void Construct( IUnlockablesService service, IGoldService goldService )
        {
            _transformCached    = transform;
            _unlockablesService = service;
            _goldService        = goldService;

            _goldService.OnGoldAmountChanged += ( g ) => RefreshNotification(  );
        }

        private void Start()
        {
            RefreshNotification( );
        }

        private void Update()
        {
            AnimateMovement( );
        }

        private void RefreshNotification()
        {
            _isVisible = _goldService.HasGold( _unlockablesService.GetItemPrice( type ) );

            _transformCached.localScale = _isVisible ? Vector3.one : Vector3.zero;
        }

        private void AnimateMovement( )
        {
            if ( !_isVisible )
                return;
            
            _animatedTransform.localPosition = Vector3.up * Mathf.Abs( Mathf.Sin( Time.time * _animationFrequency ) * _animationMagnitude );
        }
    }
}
