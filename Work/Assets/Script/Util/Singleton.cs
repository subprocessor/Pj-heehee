using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T pInstance
    {
        get
        {
            return _instance;
        }
    }


    protected virtual void Awake()
    {
        _instance = GetComponent<T>();
    }

    protected virtual void OnDestroy()
    {
        _instance = null;
    }
}
