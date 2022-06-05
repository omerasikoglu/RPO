using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private GameObject spherePrefab;
    [SerializeField] private GameObject capsulePrefab;

    [Range(1f, 15f)] public float range = 5f;
    private static ObjectPool<PoolObject> cubePool;
    private static ObjectPool<PoolObject> spherePool;
    private static ObjectPool<PoolObject> capsulePool;
    public bool canSpawn = true;

    private void OnEnable() {
        cubePool = new ObjectPool<PoolObject>(cubePrefab);
        spherePool = new ObjectPool<PoolObject>(spherePrefab);
        capsulePool = new ObjectPool<PoolObject>(capsulePrefab);

        StartCoroutine(SpawnOverTime());

        IEnumerator SpawnOverTime() {
            while (canSpawn) {
                Spawn();
                yield return null;

                void Spawn() {
                    int random = Random.Range(0, 3);
                    Vector3 position = Random.insideUnitSphere * range + transform.position;

                    _ = random switch
                    {
                        0 => cubePool.PullGameObject(position, Random.rotation),
                        1 => spherePool.PullGameObject(position, Random.rotation),
                        2 => capsulePool.PullGameObject(position, Random.rotation),
                        _ => cubePool.PullGameObject(position, Random.rotation)
                    };
                }
            }
        }
    }




}
