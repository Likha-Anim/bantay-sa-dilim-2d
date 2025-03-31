using UnityEngine;

// Class that creates a singleton instance of a MonoBehaviour
// ensuring that only one instance of the class exists
// and that it persists between scenes so that it can be reused
// in every scene which must be inherited by Manager classes
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                T[] instances = FindObjectsByType<T>(FindObjectsSortMode.None);

                if (instances.Length > 0)
                {
                    _instance = instances[0];
                }

                if (instances.Length > 1)
                {
                    Debug.LogWarning($"Multiple instances of {typeof(T).Name} found! Keeping the first one.");
                }

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(T).Name);
                    _instance = singletonObject.AddComponent<T>();
                    DontDestroyOnLoad(singletonObject);
                }
            }

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }
}