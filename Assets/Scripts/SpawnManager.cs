using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private PrefabList prefabList;

    float maxSpawnRangeX = 3;
    float intervalSpawnRate = 1f;
    bool spawnBool;
    void Awake()
    {
        prefabList = GameObject.Find("GameManager").GetComponent<PrefabList>();

        spawnBool = false;
    }

    void Update()
    {
        if (spawnBool == false)
        {
            StartCoroutine("SpawnObject");
            
        }
        
    }

    IEnumerator SpawnObject()
    {
        GameObject randomObject = prefabList.prefabArray[Random.Range(0, prefabList.prefabArray.Length)];
        Vector3 randomSpawnPoint = new Vector3(Random.Range(-maxSpawnRangeX, maxSpawnRangeX), 10, -1.5f);
        spawnBool = true;
        Instantiate(randomObject, randomSpawnPoint, randomObject.transform.rotation);
        yield return new WaitForSeconds(intervalSpawnRate);
        spawnBool = false;
    }
}
