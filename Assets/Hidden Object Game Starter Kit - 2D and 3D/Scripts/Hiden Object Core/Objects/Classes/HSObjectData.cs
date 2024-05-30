namespace MyApp.HiddenObjects
{
    using UnityEngine;
    [System.Serializable]
    public class HSObjectData
    {
        [Min(0)]
        public int id;
        public string name;
        public Sprite sprite;
        public int ID { get { return id; } }
    }
}