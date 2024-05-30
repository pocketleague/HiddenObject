#if UNITY_EDITOR
namespace MyApp.HiddenObjects.Editors
{
    using UnityEngine;
    using UnityEditor;
    using UnityEditor.SceneManagement;

    [CustomEditor(typeof(HSObjectNode))]
    public class HSObjectNodeEditor : Editor
    {
        #region variable
        public int lastOptionIndex = 0;

        protected GameManager manager;
        protected HSObjectNode Target;
        #endregion
        private void OnEnable()
        {
            Target = (HSObjectNode)target;
            if (findGamaManager())
            {
                lastOptionIndex = manager.getNodeIndex(Target.referenceObjectId) + 1;
            }
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorTools.Box_Open("Parameter(s)");
            if (EditorTools.Popup(ref lastOptionIndex, getOptions(), "Options"))
            {
                if (lastOptionIndex < 1)
                {
                    Target.referenceObjectId = -1;
                }
                else
                {
                    Target.referenceObjectId = manager.nodes[lastOptionIndex - 1].id;
                }
                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            }
            EditorTools.PropertyField(serializedObject, "referenceObjectId", "Id", "Referenced object ID");

            //sprite
            if (findGamaManager())
            {
                var node = manager.getNode(Target.referenceObjectId);
                if (node != null && node.sprite != null)
                {
                    GUILayout.Label(AssetPreview.GetAssetPreview(node.sprite));
                }
            }

            EditorTools.Box_Close();
            serializedObject.ApplyModifiedProperties();
        }
        #region GameManager
        private bool findGamaManager()
        {
            if (manager == null) manager = FindObjectOfType<GameManager>();
            return manager != null;
        }
        private string[] getOptions()
        {
            const string nullSymbol = "---";
            if (!findGamaManager())
            {
                return new string[] { nullSymbol };
            }
            string[] options;
            int count = manager.getNodes_Count();
            if (count < 1)
            {
                options = new string[] { nullSymbol };
            }
            else
            {
                options = new string[++count];
                options[0] = nullSymbol;
                for (int i = 1; i < count; i++)
                {
                    var t = manager.nodes[i - 1].name;
                    if (string.IsNullOrEmpty(t)) t = "Object" + i.ToString();
                    options[i] = t;
                }
            }
            return options;
        }
        #endregion
    }
}
#endif