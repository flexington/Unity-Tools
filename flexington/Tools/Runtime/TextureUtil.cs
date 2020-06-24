using UnityEngine;

namespace flexington.Tools
{
    public static class TextureUtil
    {
        public static Texture2D FromBase64(string base64String)
        {
            Texture2D texture = new Texture2D(1, 1);
            texture.hideFlags = HideFlags.HideAndDontSave;
            texture.LoadImage(System.Convert.FromBase64String(base64String));
            return texture;
        }

        public static void SaveAsPng(Texture2D texture, string path)
        {
            byte[] bytes = texture.EncodeToPNG();
            System.IO.File.WriteAllBytes($"{path}/{texture.GetHashCode()}.png", bytes);
            Debug.Log($"Texture saved as {path}/{texture.GetHashCode()}.png");
        }
    }
}