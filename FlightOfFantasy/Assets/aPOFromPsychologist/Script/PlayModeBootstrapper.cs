#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace FactoryYard
{

    [InitializeOnLoad]
    public static class PlayModeBootstrapper
    {
        static PlayModeBootstrapper()
        {
            EditorApplication.playModeStateChanged += OnPlayModeChanged;
        }

        private static void OnPlayModeChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingEditMode)
            {
                if (SceneManager.GetActiveScene().name != "Boot")
                {
                    EditorSceneManager.playModeStartScene =
                        AssetDatabase.LoadAssetAtPath<SceneAsset>(
                            "Assets/aPOFromPsychologist/Scene/Boot.unity");
                }
            }
        }
    }
}
#endif