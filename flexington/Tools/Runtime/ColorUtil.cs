using UnityEngine;

namespace flexington.Tools
{
    public static class ColorUtil
    {
        public static Color GetRandomColor()
        {
            return new Color(
                Random.Range(0, 255) / 255f,
                Random.Range(0, 255) / 255f,
                Random.Range(0, 255) / 255f
            );
        }
    }
}