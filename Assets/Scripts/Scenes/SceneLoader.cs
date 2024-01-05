
using System;
using System.Collections;
using UnityEngine.SceneManagement;

namespace StreetRacing.Scenes
{
    public static class SceneLoader
    {
        public static void LoadScene(SceneReference scene, Action onFinish = null)
        {
            Global.CoroutinesHolder.StartCoroutine(StartLoadingScene(scene, onFinish));
        }

        private static IEnumerator StartLoadingScene(SceneReference scene, Action onFinish = null)
        {
            yield return UnloadCurrentScene();

            yield return SceneManager.LoadSceneAsync(scene.ScenePath, LoadSceneMode.Additive);

            onFinish?.Invoke();
        }

        private static IEnumerator UnloadCurrentScene()
        {
            if (SceneManager.sceneCount == 1)
            {
                yield break;
            }

            var activeScene = SceneManager.GetActiveScene();
            yield return SceneManager.UnloadSceneAsync(activeScene);
        }
    }
}
