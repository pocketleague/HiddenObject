#if UNITY_EDITOR
namespace MyApp.HiddenObjects
{
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;
    public class MainMenu : MonoBehaviour
    {
        #region About us
        [MenuItem(MyGlobals.RootName + "/Publisher Page")]
        public static void PublisherPage()
        {
            Application.OpenURL("https://assetstore.unity.com/publishers/48757");
        }
        #endregion
        #region Support
        [System.Obsolete]
        [MenuItem(MyGlobals.RootName + "/Support")]
        public static void Support()
        {
            TextWindow window = (TextWindow)EditorWindow.GetWindow(typeof(TextWindow));
            window.title = window.Title = "Support";
            window.Descriptions = new string[] { "If you need any further assistance, please contact us", "unrealisticarts@gmail.com" };
        }
        #endregion
        #region Component
        public static void AddComponentToObject<T>(string name) where T : Component
        {
            GameObject node = new GameObject(name);
            node.transform.SetPositionAndRotation(getPosition(), Quaternion.identity);
            node.AddComponent<T>();
            Undo.RegisterCreatedObjectUndo(node, "Create object");
            Selection.objects = new Object[] { node };
        }
        public static void AttachComponentToSelection<T>() where T : Component
        {
            if (Selection.objects == null) return;
            for (int i = 0; i < Selection.objects.Length; i++)
            {
                var node = (GameObject)Selection.objects[i];
                if (node == null || node.GetComponent<T>() != null) continue;
                Undo.AddComponent<T>(node);
            }
        }
        public static void RemoveComponentFromSelection<T>() where T : Component
        {
            if (Selection.objects == null) return;
            T function;
            for (int i = 0; i < Selection.objects.Length; i++)
            {
                var node = (GameObject)Selection.objects[i];
                if (node == null || (function = node.GetComponent<T>()) == null) continue;
                Undo.DestroyObjectImmediate(function);
            }
        }
        public static void SelectAllObjectsByComponent<T>() where T : Component
        {
            var nodes = FindObjectsOfType<T>();
            List<Object> result = new List<Object>();
            if (nodes != null)
            {
                foreach (var node in nodes)
                {
                    if (node == null) continue;
                    result.Add(node.gameObject);
                }
            }
            Selection.objects = result.ToArray();
        }
        #endregion
        public class MainMenuItem
        {
            public const string AddAndSelect = "(Add and) Select";
            public const string Add = "Add component";
            public const string Attach = "Attach component";
            public const string SelectAll = "Select all";
            public const string RemoveAll = "Remove component";
            public const string SelectAndRemoveAll = "Select and Remove component";
            public const string SaveAll = "Save all";
            public const string DeleteAll = "Delete all";
            public const string Debug = "Debug";
            public const string FixProblems = "Fix problem(s)";
        }
        protected static Vector3 getPosition()
        {
            return Vector3.zero;
        }
    }
}
#endif