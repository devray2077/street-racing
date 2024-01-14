using System.Collections.Generic;

namespace UnityEngine.UI {

    [AddComponentMenu("UI/Effects/GraphicsOffset", 15)]
    public class UIGraphicsOffset : BaseMeshEffect {
        public Vector2 offset;

        private static readonly List<UIVertex> verts = new List<UIVertex>(128);

        public override void ModifyMesh(VertexHelper vh) {
            if(!this.IsActive())
                return;

            List<UIVertex> list = new List<UIVertex>();
            vh.GetUIVertexStream(list);

            for(int i = 0; i < list.Count; i++) {
                UIVertex vert = list[i];
                vert.position += (Vector3) offset;
                list[i] = vert;
            }

            vh.Clear();
            vh.AddUIVertexTriangleStream(list);
        }
    }
}