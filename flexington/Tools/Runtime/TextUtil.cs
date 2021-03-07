using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace flexington.Tools
{
    public static class TextUtil
    {
        public static TextMeshPro CreateText(string text, string name, Transform parent = null, Vector3 localPosition = default, int fontSize = 8, Color? color = null, TextAnchor textAnchor = TextAnchor.UpperLeft, TextAlignmentOptions textAlignment = default, int sortingOrder = 0, int layer = 0)
        {
            if (color == null) color = Color.black;
            return TextUtil.CreateText(parent, text, name, localPosition, fontSize, (Color)color, textAnchor, textAlignment, sortingOrder, layer);
        }

        public static TextMeshPro CreateText(Transform parent, string text, string name, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor, TextAlignmentOptions textAlignment, int sortingOrder, int layer)
        {
            GameObject gameObject = new GameObject(name, typeof(TextMeshPro), typeof(ContentSizeFitter));
            gameObject.layer = layer;
            Transform transform = gameObject.transform;
            transform.parent = parent;
            transform.localPosition = localPosition;

            ContentSizeFitter sizeFitter = gameObject.GetComponent<ContentSizeFitter>();
            sizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            sizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            TextMeshPro textMesh = gameObject.GetComponent<TextMeshPro>();
            textMesh.alignment = textAlignment;
            textMesh.text = text;
            textMesh.fontSize = fontSize;
            textMesh.color = color;

            return textMesh;
        }
    }
}