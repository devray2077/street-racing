
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using StreetRacing.UI;

namespace StreetRacing.Scenes
{
    public static class SceneLoader
    {
        public static void LoadScene(SceneReference scene, Action onSceneLoaded = null, Action onLoadingScreenHidden = null)
        {
            Global.UIController.ShowScreen<LoadingScreen>(OnLoadingScreenShown);

            void OnLoadingScreenShown()
            {
                Global.CoroutinesHolder.StartCoroutine(StartLoadingScene(scene, onSceneLoaded, onLoadingScreenHidden));
            }
        }

        private static IEnumerator StartLoadingScene(SceneReference scene, Action onSceneLoaded = null, Action onLoadingScreenHidden = null)
        {
            yield return UnloadCurrentScene();
            yield return SceneManager.LoadSceneAsync(scene.ScenePath, LoadSceneMode.Additive);
            SceneManager.SetActiveScene(SceneManager.GetSceneAt(1));

            onSceneLoaded?.Invoke();

            Global.UIController.HideScreen<LoadingScreen>(onLoadingScreenHidden);
        }

        private static IEnumerator UnloadCurrentScene()
        {
            if (SceneManager.sceneCount > 1)
            {
                var activeScene = SceneManager.GetSceneAt(1);
                yield return SceneManager.UnloadSceneAsync(activeScene);
            }

            Resources.UnloadUnusedAssets();
        }
    }
}
