using System.Collections.Generic;
using UnityEngine;

public class GenericTypeQueue<T> : MonoBehaviour where T : Component
{
    public T Prefab { get => prefab; set => prefab = value; }

    [SerializeField] private T prefab = null;

    private Queue<T> inactiveObjects = new Queue<T>();


    public T Get()
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
