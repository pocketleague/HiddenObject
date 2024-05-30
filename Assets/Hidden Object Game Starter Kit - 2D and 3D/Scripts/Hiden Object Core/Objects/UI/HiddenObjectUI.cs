namespace MyApp.HiddenObjects
{
    using UnityEngine;
    using UnityEngine.UI;
    public class HiddenObjectUI : MonoBehaviour
    {
        #region variable
        public Text text;
        public Image image;
        #region HideInInspector
        public int ID { get; set; }
        #endregion
        #endregion
        #region Functions
        #endregion
        #region functions
        public void Init(int id, string txt, Sprite sprite)
        {
            ID = id;
            setText(txt);
            setSprite(sprite);
        }
        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }
        #region text
        public void setText(string value)
        {
            if (text == null) return;
            text.text = string.IsNullOrEmpty(value) ? "" : value;
        }
        #endregion
        #region image
        public void setSprite(Sprite value)
        {
            if (image == null) return;
            image.sprite = value;
            image.gameObject.SetActive(value != null);
        }
        #endregion
        public void OnDestroy()
        {
            Object.Destroy(gameObject);
        }
        #endregion
    }
}