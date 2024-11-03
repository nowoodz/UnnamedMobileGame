using NUnit.Framework.Internal;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GeneralGameObject : MonoBehaviour
{

    #region Rotation Variables
    private float rotationSpeed = 50f;

    private List<Vector3> randomRotationVectorList = new List<Vector3>();

    Vector3 randomRotVectorX = new Vector3(1, 0, 0);
    Vector3 randomRotVectorY = new Vector3(0, 1, 0);
    Vector3 randomRotVectorZ = new Vector3(0, 0, 1);

    Vector3 randomRotationVector;
    #endregion

    GameManager gameManager;


    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

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
        gameManager.currentGameScore++;

        Destroy(gameObject);
    }

}
