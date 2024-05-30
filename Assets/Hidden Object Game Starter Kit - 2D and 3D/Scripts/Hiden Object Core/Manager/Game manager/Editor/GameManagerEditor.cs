#if UNITY_EDITOR
namespace MyApp.HiddenObjects.Editors
{
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(GameManager))]
    public class GameManagerEditor : Editor
    {
        #region variable
        protected GameManager Target;
        #endregion
        private void OnEnable()
        {
            Target = (GameManager)target;
        }
        #region Inspector
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            Params();
            Params_canvasParameters();
            Params_hint();
            Params_pixelHunting();
            serializedObject.ApplyModifiedProperties();
            Info();
        }
        private void Params()
        {
            if (!EditorTools.Foldout(ref Target.parametersEnable, "Parameter(s)")) return;
            EditorTools.Box_Open();
            EditorTools.PropertyField(serializedObject, "mode", "Mode");
            EditorTools.Line();
            EditorTools.PropertyField(serializedObject, "onHitEventDelay", "On-Hit event delay");
            EditorTools.PropertyField(serializedObject, "onHitEvent", "On-Hit event");
            EditorTools.Line();
            EditorTools.PropertyField(serializedObject, "onGameIsOverEventDelay", "On-GameIsOver event delay");
            EditorTools.PropertyField(serializedObject, "onGameIsOverEvent", "On-GameIsOver");
            EditorTools.Line();
            EditorTools.PropertyField(serializedObject, "nodes", "Hidden Object(s)");
            if (GUILayout.Button("Auto ID"))
            {
                Target.AutoID();
            }
            EditorTools.Box_Close();
        }
        private void Params_hint()
        {
            if (!EditorTools.Foldout(ref Target.hintParametersEnable, "Hint")) return;
            EditorTools.Box_Open();
            EditorTools.PropertyField(serializedObject, "hint", "Hint");
            if (Target.hint)
            {
                EditorTools.PropertyField(serializedObject, "hintDelay", "Hint event - delay");
                EditorTools.PropertyField(serializedObject, "hintEvent", "Hint event");
                EditorTools.PropertyField(serializedObject, "hintBtn", "Hint button");
                EditorTools.PropertyField(serializedObject, "hintImage", "Hint image");
                EditorTools.PropertyField(serializedObject, "hintImageShowSecond", "Hint image show time (second)");
                if (Target.mode == GameManager.Mode.ThreeDimensional)
                {
                    EditorTools.PropertyField(serializedObject, "hintOnObject", "Hint position on object");
                    if (!Target.hintOnObject)
                    {
                        EditorTools.PropertyField(serializedObject, "hintCameraDistance", "Hint-Camera Delta distance");
                    }
                }
            }
            EditorTools.Box_Close();
        }
        private void Params_canvasParameters()
        {
            if (!EditorTools.Foldout(ref Target.canvasParametersEnable, "Canvas")) return;
            EditorTools.Box_Open();
            EditorTools.PropertyField(serializedObject, "canvasObjects", "Canvas Object(s)`s parent", "The GameObject parent in canvas.");
            EditorTools.PropertyField(serializedObject, "canvasObjectScale", "Scale");
            EditorTools.PropertyField(serializedObject, "maxCanvasObjectsCount", "Count", "The maximum canvas object(s) Count.");
            EditorTools.Box_Close();
        }
        private void Params_pixelHunting()
        {
            if (!EditorTools.Foldout(ref Target.pixelHuntParametersEnable, "Pixel hunt")) return;
            EditorTools.Box_Open();
            EditorTools.PropertyField(serializedObject, "pixelHunting", "Pixel hunting");
            if (Target.pixelHunting)
            {
                EditorTools.PropertyField(serializedObject, "pixelHuntingThresholdDelay", "Second(s) threshold", "Second time delay time between each tap.");
                EditorTools.PropertyField(serializedObject, "pixelHuntEvent", "Pixel hunt event");
                EditorTools.PropertyField(serializedObject, "pixelHuntImage", "Pixel hunt image", "Pixel hunt game object that include image.");
            }
            EditorTools.Box_Close();
        }
        private void Info()
        {
            if (!EditorTools.Foldout(ref Target.infoEnable, "Infomation")) return;
            EditorTools.Box_Open();
            EditorTools.Info("Total tap(s) count", Target.tapCount.ToString());
            EditorTools.Info("Game is Over", Target._gameIsOver.ToString());
            EditorTools.Box_Close();
        }
        #endregion
    }
}
#endif