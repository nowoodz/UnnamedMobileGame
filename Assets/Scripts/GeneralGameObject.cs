using NUnit.Framework.Internal;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GeneralGameObject : MonoBehaviour
{
    private GameSounds gameSounds;
    #region Rotation Variables
    private float rotationSpeed = 50f;

    private List<Vector3> randomRotationVectorList = new List<Vector3>();

    Vector3 randomRotVectorX = new Vector3(1, 0, 0);
    Vector3 randomRotVectorY = new Vector3(0, 1, 0);
    Vector3 randomRotVectorZ = new Vector3(0, 0, 1);

    Vector3 randomRotationVector;
    #endregion

    GameManager gameManager;

    GameModeScript gameModeScript;

    GameData gameData;



    private void Awake()
    {
        gameData = SaveSystem.Load();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameModeScript = GameObject.Find("GameModeManager").GetComponent<GameModeScript>();
        gameSounds = GameObject.Find("GameSounds").GetComponent<GameSounds>();
        #region Rotation Starts
        randomRotationVectorList.Add(randomRotVectorX);
        randomRotationVectorList.Add(randomRotVectorY);
        randomRotationVectorList.Add(randomRotVectorZ);


        int randomRotationInt = Random.Range(0, randomRotationVectorList.Count);
        randomRotationVector = randomRotationVectorList[randomRotationInt];
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        // DO ROTATION
        transform.Rotate(randomRotationVector * rotationSpeed * Time.deltaTime);

    }
    public void GameObjectTouched()
    {
        gameSounds.PlayPickupSound();
        if (gameData.IsScoreBoostActive())
        {
            gameManager.currentGameScore += 20;
        }
        else
        {
            gameManager.currentGameScore += 10;
        }

        if (gameData.IsCoinsBoostActive())
        {
            gameManager.currentGameCoins += 2;

        }
        else
        {
            gameManager.currentGameCoins++;
        }
        Destroy(gameObject);
    }

}
