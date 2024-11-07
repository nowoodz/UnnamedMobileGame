using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    

    float maxSpawnRangeX = 3;
    float intervalSpawnRate = 1f;

    bool spawnBool;
    bool gameModeSet;

    [SerializeField] private PrefabList prefabList;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameModeScript gameModeScript;

    [SerializeField] private GameObject[] spawnPrefabsAccordingToModeArray;

    void Awake()
    {
        gameModeSet = false;
        spawnBool = false;
    }

    private void Start()
    {
       
    }
    void Update()
    {
        //SET OUR PREFABS TO SPAWN ACCORDING TO GAME MODE
        if (gameModeScript.currentModeString == "FruitRush")
        {
            spawnPrefabsAccordingToModeArray = prefabList.fruitRushPrefabsArray;
        }
        else if (gameModeScript.currentModeString == "TreasureHunt")
        {
            spawnPrefabsAccordingToModeArray = prefabList.treasureHuntPrefabsArray;
        }
        else if (gameModeScript.currentModeString == "SpeedStorm")
        {
            spawnPrefabsAccordingToModeArray = prefabList.speedStormPrefabsArray;
        }
        else if (gameModeScript.currentModeString == "ColorFrenzy")
        {
            spawnPrefabsAccordingToModeArray = prefabList.colorFrenzyPrefabsArray;
        }
        else if (gameModeScript.currentModeString == "PowerUpMadness")
        {
            spawnPrefabsAccordingToModeArray = prefabList.powerUpMadnesshPrefabsArray;
        }

        if (spawnPrefabsAccordingToModeArray != null)
        {
            gameModeSet = true;
        }

        if (spawnBool == false && gameManager.isGameStarted == true && gameManager.isGameOver == false && gameModeSet == true)
        {
            StartCoroutine("SpawnObject");

        }

    }

    IEnumerator SpawnObject()
    {
        
        GameObject randomObject = spawnPrefabsAccordingToModeArray[Random.Range(0, spawnPrefabsAccordingToModeArray.Length)];
        Vector3 randomSpawnPoint = new Vector3(Random.Range(-maxSpawnRangeX, maxSpawnRangeX), 10, -1.5f);
        spawnBool = true;
        Instantiate(randomObject, randomSpawnPoint, randomObject.transform.rotation);
        yield return new WaitForSeconds(intervalSpawnRate);
        spawnBool = false;
    }
}
