using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private GameObject spherePrefab;
    [SerializeField] private GameObject capsulePrefab;

    [Range(1f, 15f)] public float range = 5f;
    private ObjectPool<PoolObject> cubePool;
    private ObjectPool<PoolObject> spherePool;
    private ObjectPool<PoolObject> capsulePool;
    private List<ObjectPool<PoolObject>> parentPool;
    public bool canSpawn = true;

    private void OnEnable() {
        cubePool = new ObjectPool<PoolObject>(cubePrefab);
        spherePool = new ObjectPool<PoolObject>(spherePrefab);
        capsulePool = new ObjectPool<PoolObject>(capsulePrefab);
        parentPool = new List<ObjectPool<PoolObject>> { cubePool, spherePool, capsulePool };
    }

    public void Start() {
        StartCoroutine(SpawnOverTime());

        IEnumerator SpawnOverTime() {
            while (canSpawn) {
                Spawn();
                yield return null;

                void Spawn() {
                    int random = Random.Range(0, 3);
                    Vector3 position = Random.insideUnitSphere * range + transform.position;
                    var pulledGO = parentPool[random].PullGameObject(position, Random.rotation);
                }
            }
        }
    }


}
