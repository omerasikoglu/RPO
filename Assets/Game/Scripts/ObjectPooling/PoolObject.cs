using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour, IPoolable<PoolObject> {

    private Action<PoolObject> OnReturnedToPool;
    private Rigidbody objectRigidbody;

    private float lifetime = 2f, currentTimer;

    public void Awake() {
        objectRigidbody = GetComponent<Rigidbody>();
    }
    public void OnEnable() {
        currentTimer = lifetime;
        objectRigidbody.velocity = Vector3.zero;
    }
    private void OnDisable() {
        ReturnToPool();
    }

    public void Update() {
        currentTimer -= Time.deltaTime;
        if (currentTimer <= 0f) gameObject.SetActive(false);
    }

    public void Initialize(Action<PoolObject> returnAction) {
        OnReturnedToPool = returnAction;
    }

    public void ReturnToPool() {
        OnReturnedToPool?.Invoke(this);
    }
    public void OnTriggerEnter(Collider collision) {
        gameObject.SetActive(false);
    }
}
