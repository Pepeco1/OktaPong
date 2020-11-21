using UnityEngine;

public class SingletonMono<T> : MonoBehaviour where T : SingletonMono<T>
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                var objs = FindObjectsOfType(typeof(T)) as T[];

                if (objs.Length > 0)
                    _instance = objs[0];

                if (objs.Length > 1)
                {
                    Debug.LogWarning("[Singleton] There is more than one instance of " + typeof(T).Name + " in the scene.");
                    objs[0].DeactivateExcesiveInstances(objs);
                }

                if (_instance == null)
                {
                    Debug.LogWarning("Creating new instance of singleton");
                    GameObject obj = new GameObject();
                    obj.hideFlags = HideFlags.DontSave;
                    _instance = obj.AddComponent<T>();
                }
            }

            return _instance;

        }
    }

    private void Awake()
    {
        var objs = FindObjectsOfType(typeof(T)) as T[];
        DeactivateExcesiveInstances(objs);
    }

    public static bool isInicialized
    {
        get { return _instance != null; }
    }

    protected virtual void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }

    private void DeactivateExcesiveInstances(T[] instancesArray)
    {
        Debug.Log($"[Singleton] Deactivating excessive instances of singleton type: {typeof(T)}");
        for (int i = 1; i < instancesArray.Length; i++)
            instancesArray[i].gameObject.SetActive(false);
    }
}