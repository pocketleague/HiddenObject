namespace MyApp.HiddenObjects
{
    using UnityEngine;
    public class HSObjectNode : MonoBehaviour
    {
        #region variable
        [Min(-1)]
        public int referenceObjectId = -1;
        #region HideInInspector
        [HideInInspector] public Collider2D _collider2D;
        [HideInInspector] public Collider _collider;
        #endregion
        #endregion
        #region Functions
        private void Awake()
        {
            _collider2D = GetComponent<Collider2D>();
            _collider = GetComponent<Collider>();
            if(_collider2D==null && _collider==null) Object.Destroy(this);
        }
        #endregion
        #region functions
        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }
        #endregion
    }
}