using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class ObjectPool<T> : IPool<T> where T : MonoBehaviour, IPoolable<T> {

    private Action<T> OnObjectPulled, OnObjectPushed;
    private Stack<T> pooledObjects = new Stack<T>();
    private GameObject prefab;

    public int PooledObjectCount => pooledObjects.Count;

    public ObjectPool(GameObject pooledObject, int numberToSpawn = 0) {
        prefab = pooledObject;
        Spawn(numberToSpawn);
    }

    public ObjectPool(GameObject pooledObject, Action<T> onObjectPulled, Action<T> onObjectPushed, int numberToSpawn = 0) {
        prefab = pooledObject;
        OnObjectPulled = onObjectPulled;
        OnObjectPushed = onObjectPushed;
    }
    private void Spawn(int numberToSpawn) {
        T t;
        for (int i = 0; i < numberToSpawn; i++) {
            t = Object.Instantiate(prefab).GetComponent<T>();
            pooledObjects.Push(t);
            t.gameObject.SetActive(false);
        }
    }

    public T Pull() {
        var t = PooledObjectCount > 0 ? pooledObjects.Pop() : Object.Instantiate(prefab).GetComponent<T>();

        t.gameObject.SetActive(true); //ensure the object is on
        t.Initialize(Push);

        //allow default behavior and turning object back on
        OnObjectPulled?.Invoke(t);

        return t;
    }

    public T Pull(Vector3 position) {
        T t = Pull();
        t.transform.position = position;
        return t;
    }

    public T Pull(Vector3 position, Quaternion rotation) {
        T t = Pull();
        t.transform.SetPositionAndRotation(position, rotation);
        return t;
    }

    public GameObject PullGameObject() {
        return Pull().gameObject;
    }

    public GameObject PullGameObject(Vector3 position) {
        GameObject go = Pull().gameObject;
        go.transform.position = position;
        return go;
    }

    public GameObject PullGameObject(Vector3 position, Quaternion rotation) {
        GameObject go = Pull().gameObject;
        go.transform.SetPositionAndRotation(position, rotation);
        return go;
    }

    public void Push(T t) {
        pooledObjects.Push(t);

        //create default behavior to turn off objects
        OnObjectPushed?.Invoke(t);

        t.gameObject.SetActive(false);
    }
}
