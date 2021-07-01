using UnityEngine;

public class Singleton<T> : MonoBehaviour where T: Singleton<T>
{
    private static T Instance;

    public static T instance
    {
        get
        {
            return Instance;
        }    
    }

    protected virtual void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one singleton Found!");
        }
        else
        {
            Instance = (T)this;
        }
    }
}
