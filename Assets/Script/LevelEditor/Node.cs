namespace CrazyHub.Hyderabad.Assignment
{
    using UnityEngine;

    [System.Serializable]
    public class Node 
    {
        Rect rect;
        public GUIStyle style;

        public Node(Vector2 position, float width, float height, GUIStyle defaultStyle) {
            rect = new Rect(position.x, position.y, width, height);
            style = defaultStyle;
        }

        public void OnDrag(Vector2 delta) {
            rect.position += delta;
        }

        public void Draw() {
            GUI.Box(rect, "", style);
        }

        public void setStyle(GUIStyle nodeStyle) {
            style = nodeStyle;
        }
    }
}


