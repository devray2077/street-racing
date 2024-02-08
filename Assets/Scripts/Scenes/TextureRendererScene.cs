
using System.IO;
using System.Collections;
using UnityEngine;
using Sirenix.OdinInspector;

namespace StreetRacing
{
    public class TextureRendererScene : MonoBehaviour
    {
        [SerializeField] private new Camera camera;
        [SerializeField] private Vector2Int resolution = new Vector2Int(512, 512);
        [SerializeField] private string fileName;

        public RenderTexture renderTexture;

        [Button]
        private void GenerateIcon()
        {
            if (Application.isPlaying)
            {
                StartCoroutine(GenerateIconCoroutine());
            }
        }

        private IEnumerator GenerateIconCoroutine()
        {
            renderTexture = new RenderTexture(resolution.x, resolution.y, 16, RenderTextureFormat.ARGB32);
            camera.targetTexture = renderTexture;

            camera.Render();

            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();

            var texture = new Texture2D(renderTexture.width, renderTexture.height);
            RenderTexture.active = renderTexture;
            texture.ReadPixels(new Rect(0, 0, texture.width, texture.height), 0, 0);
            texture.Apply();

            var filePath = Path.Combine(Application.dataPath, $"{fileName}.png");
            File.WriteAllBytes(filePath, texture.EncodeToPNG());

            renderTexture.Release();
            camera.targetTexture = null;
        }
    }
}
