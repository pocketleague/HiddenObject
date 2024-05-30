#if UNITY_EDITOR
namespace MyApp.HiddenObjects
{
    using UnityEditor;
    using UnityEngine;

    public class HOMenu : MainMenu
    {
        #region variable
        private const string HOName = "/Hidden Objects/";
        #endregion
        #region Game Manager
        [MenuItem(MyGlobals.RootName + "/" + HSGlobals.ProjectName + "/Game Manager/" + MainMenuItem.AddAndSelect)]
        public static void AddGridManager()
        {
            var item = FindObjectOfType<GameManager>();
            if (item != null)
            {
                if (item.gameObject.GetComponent<GameManager>() == null)
                {
                    Undo.AddComponent<GameManager>(item.gameObject);
                }
                Selection.objects = new Object[] { item.gameObject };
                return;
            }
            GameObject node = new GameObject("Game Manager");
            node.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
            node.AddComponent<GameManager>();

            Undo.RegisterCreatedObjectUndo(node, "Created Game Manager");
            Selection.objects = new Object[] { node };
        }
        #endregion
        #region HS Node
        [MenuItem(MyGlobals.RootName + "/" + HSGlobals.ProjectName + HOName + MainMenuItem.Attach)]
        public static void AttachHONodeScript()
        {
            AttachComponentToSelection<HSObjectNode>();
        }
        [MenuItem(MyGlobals.RootName + "/" + HSGlobals.ProjectName + HOName + MainMenuItem.RemoveAll)]
        public static void RemoveHONodeScript()
        {
            RemoveComponentFromSelection<HSObjectNode>();
        }
        [MenuItem(MyGlobals.RootName + "/" + HSGlobals.ProjectName + HOName + MainMenuItem.SelectAll)]
        public static void SelectHONodes()
        {
            SelectAllObjectsByComponent<HSObjectNode>();
        }
        #endregion
    }
}
#endif