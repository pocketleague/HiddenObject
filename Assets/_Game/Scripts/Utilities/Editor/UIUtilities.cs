using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Utilities.Editor
{
    public class UIUtilities : MonoBehaviour
    {
        [MenuItem("Utilities/Hide All UI")]
        static void HideAllUserInterface()
        {
            for ( var i = 0; i < SceneManager.sceneCount; ++i )
            {
                foreach ( var rootObject in SceneManager.GetSceneAt( i ).GetRootGameObjects( ) )
                {
                    foreach ( var canvasObject in rootObject.GetComponentsInChildren<Canvas>( ) )
                    {
                        var canvasGroupOnObject = canvasObject.GetComponent<CanvasGroup>( );
                    
                        if ( canvasGroupOnObject == null )
                        {
                            canvasGroupOnObject = canvasObject.gameObject.AddComponent<CanvasGroup>( );
                        }

                        canvasGroupOnObject.alpha = 0f;
                    }
                }
            }
        }
    }
}
