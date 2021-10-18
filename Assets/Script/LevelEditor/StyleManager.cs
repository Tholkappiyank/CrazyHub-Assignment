using System;
using UnityEngine;

namespace CrazyHub.Hyderabad.Assignment
{
    public class StyleManager : MonoBehaviour
    {
        public ButtonStyle[] buttonStyles;
    }

    [Serializable]
    public struct ButtonStyle {
        public Texture2D Icon;
        public string ButtonText;
        public GameObject PrefabObject;
        [HideInInspector]
        public GUIStyle NodeStyle;
    }
}
