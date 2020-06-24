using UnityEngine;
namespace flexington.Tools
{
    public class SingletonComponent<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static bool _shuttingDown = false;
        private static object _lock = new object();
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_shuttingDown)
                {
                    Debug.Log($"Instance of {typeof(T).ToString()} already destryed.");
                    return null;
                }

                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = (T)FindObjectOfType(typeof(T));

                        if (_instance == null)
                        {
                            GameObject instance = new GameObject();
                            _instance = instance.AddComponent<T>();
                            instance.name = $"{typeof(T).ToString()} (Singleton)";
                            DontDestroyOnLoad(instance);
                        }
                    }
                    return _instance;
                }
            }
        }

        private void OnApplicationQuit()
        {
            _shuttingDown = true;
        }

        private void OnDestroy()
        {
            _shuttingDown = true;
        }
    }
}
// TODO: Add some kind of generic parent so the generated components are not loose