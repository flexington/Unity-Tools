using UnityEngine;

namespace flexington.Tools
{
    public static class UnityUtil
    {
        public static void Destroy(GameObject gameObject, bool immediate = false)
        {
            if (Application.isEditor || immediate) GameObject.DestroyImmediate(gameObject);
            else GameObject.Destroy(gameObject);
        }

        public static T Instantiate<T>(T gameObject) where T : MonoBehaviour
        {
            return GameObject.Instantiate<T>(gameObject, gameObject.transform.position, gameObject.transform.rotation);
        }
    }
}