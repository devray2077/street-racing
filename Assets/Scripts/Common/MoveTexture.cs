
using UnityEngine;

namespace StreetRacing
{
    public class MoveTexture : MonoBehaviour
    {
        [SerializeField]
        private string tileShaderMapName = "_MainTex";

        [SerializeField]
        private int materialIndex;

        [SerializeField]
        private Vector2 speedUV;

        private Material material;

        private void Start()
        {
            var renderer = GetComponent<Renderer>();
            var sharedMaterials = renderer.sharedMaterials;
            material = new Material(sharedMaterials[materialIndex]);
            sharedMaterials[materialIndex] = material;
            renderer.sharedMaterials = sharedMaterials;
        }

        private void Update()
        {
            var u = Mathf.Repeat(Time.unscaledTime * Mathf.Abs(speedUV.x), 1f) * Mathf.Sign(speedUV.x);
            var v = Mathf.Repeat(Time.unscaledTime * Mathf.Abs(speedUV.y), 1f) * Mathf.Sign(speedUV.y);

            var textureOffset = new Vector2(u, v);

            material.SetTextureOffset(tileShaderMapName, textureOffset);
        }
    }
}