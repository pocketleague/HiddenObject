using System.Collections.Generic;
using Scripts.Stages;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using UnityEngine;

namespace Scripts.Utilities.Editor
{
    public class StagesScriptableObjectsGeneratorWindow : EditorWindow
    {
        public TextAsset gldCsv;
        public string    separator;
        public string    savePath;
        public bool      generated;

        private const string _sceneToLoadProperty = "sceneToLoad";

        [MenuItem( "Utilities/Stages Scriptable Objects Generator")]
        static void Init()
        {
            
            StagesScriptableObjectsGeneratorWindow window = (StagesScriptableObjectsGeneratorWindow)EditorWindow.GetWindow(typeof(StagesScriptableObjectsGeneratorWindow));
            window.Show();
            window.separator = ";";
            window.savePath = "_Game/Config/Stages";

            window.FindDependencies( );
        }
        
        void OnGUI()
        {
            GUILayout.Label("Settings", EditorStyles.boldLabel);
            EditorGUILayout.BeginHorizontal( );
            GUILayout.Label( "GLD File:" );
            gldCsv    = (TextAsset)EditorGUILayout.ObjectField(gldCsv, typeof(Object), false);
            EditorGUILayout.EndHorizontal( );
            EditorGUILayout.BeginHorizontal( );
            GUILayout.Label( "Separator:" );
            separator = EditorGUILayout.TextField( separator );
            EditorGUILayout.EndHorizontal( );

            if ( gldCsv == null || string.IsNullOrEmpty( separator ) )
            {
                EditorGUILayout.HelpBox( "Provide the GLD.txt file", MessageType.Info );
                return;
            }
            
            EditorGUILayout.BeginHorizontal( );
            GUILayout.Label( "Save location:" );
            savePath = EditorGUILayout.TextField( savePath );
            EditorGUILayout.EndHorizontal( );
            

            if ( !generated && GUILayout.Button( "Generate Files" ) )
            {
                CreateGLDScriptableObjects( ReadGldFile( ) );
                
                generated = true;
            }

            if ( generated )
            {
                EditorGUILayout.HelpBox( "Generated successfully!", MessageType.Info );
            }
        }

        private void FindDependencies( )
        {
            // Use AssetDatabase.FindAssets( "t:ExampleType" ) ) to cache required Scriptable Objects
        }

        private List<Dictionary<string, string>> ReadGldFile()
        {
            var textByLines = gldCsv.text.Split( '\n' );
            var result      = new List<Dictionary<string, string>>( );
            var headers     = new List<string>( textByLines[0].Split( ';' ) );
            
            if ( headers[^1][^1] == '\r' )
                headers[^1] = headers[^1].Remove( headers[^1].Length - 1 );

            for ( var i = 1; i < textByLines.Length; ++i )
            {
                var entry = new Dictionary<string, string>( );

                var entriesInLine = textByLines[i].Split( ';' );
                
                if ( entriesInLine[^1][^1] == '\r' )
                    entriesInLine[^1] = entriesInLine[^1].Remove( entriesInLine[^1].Length - 1 );

                for ( var j = 0; j < entriesInLine.Length; ++j )
                {
                    entry.Add( headers[j], entriesInLine[j] );
                }
                
                result.Add( entry );
            }

            return result;
        }

        private void CreateGLDScriptableObjects( List<Dictionary<string, string>> entries )
        {
            EditorUtility.DisplayProgressBar( "Creating assets", "Please wait while assets are being created", 0f );

            StageConfig createdStageConfig = null;
            
            for ( var i = 0; i < entries.Count; ++i )
            {
                createdStageConfig = CreateStageConfigForData( entries[i] );
                
                AssetDatabase.CreateAsset( createdStageConfig, "Assets/" + savePath + "/Stage_" + i + ".asset" );
                
                EditorUtility.DisplayProgressBar( "Creating assets", "Please wait while assets are being created", ( float )i / entries.Count );
            }
            
            AssetDatabase.SaveAssets( );
            AssetDatabase.Refresh( );
            EditorUtility.FocusProjectWindow( );
            EditorUtility.ClearProgressBar( );

            Selection.activeObject = createdStageConfig;
        }

        private StageConfig CreateStageConfigForData( Dictionary<string, string> data )
        {
            var stageInstance = (StageConfig)CreateInstance( typeof( StageConfig ) );
            
            //Fill all properties here
            //stageInstance.stageScene = data[_sceneToLoadProperty];

            return stageInstance;
        }
    }
}
