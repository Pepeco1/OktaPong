using System.Collections.Generic;
using UnityEngine;

public class GenericTypeQueue<T> : MonoBehaviour where T : Component
{

    [SerializeField] private T prefab = null;

    private Queue<T> inactiveObjects = new Queue<T>();

    
    public GenericTypeQueue(T prefab)
    {
        this.prefab = prefab;
    }

    public T GetInstance()
    {

        if (inactiveObjects.Count <= 0)
            AddObjToQueue();

        return inactiveObjects.Dequeue();
        
    }

    public void ReturnToPool(T obj)
    {
        obj.gameObject.SetActive(false);
        inactiveObjects.Enqueue(obj);
    }


    private void AddObjToQueue()
    {
        var obj = Instantiate(prefab);
        inactiveObjects.Enqueue(obj);
    }

}
