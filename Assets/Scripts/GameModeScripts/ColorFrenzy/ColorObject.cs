using System.Collections.Generic;
using UnityEngine;

public class ColorObject : MonoBehaviour
{
    [SerializeField] ColorObjectSO colorObjectSO;
    GameModeScript gameModeScript;
    private string myColor;

    #region Rotation Variables
    private float rotationSpeed = 50f;

    private List<Vector3> randomRotationVectorList = new List<Vector3>();

    Vector3 randomRotVectorX = new Vector3(1, 0, 0);
    Vector3 randomRotVectorY = new Vector3(0, 1, 0);
    Vector3 randomRotVectorZ = new Vector3(0, 0, 1);

    Vector3 randomRotationVector;
    #endregion

    private void Awake()
    {
        myColor = colorObjectSO.myColor;
        gameModeScript = GameObject.Find("GameModeManager").GetComponent<GameModeScript>();

        #region Rotation Starts
        randomRotationVectorList.Add(randomRotVectorX);
        randomRotationVectorList.Add(randomRotVectorY);
        randomRotationVectorList.Add(randomRotVectorZ);


        int randomRotationInt = Random.Range(0, randomRotationVectorList.Count);
        randomRotationVector = randomRotationVectorList[randomRotationInt];
        #endregion
    }

    void Update()
    {
        // DO ROTATION
        transform.Rotate(randomRotationVector * rotationSpeed * Time.deltaTime);

    }

    public void GameObjectTouched() 
    {
        gameModeScript.ColorObjectTouched(myColor);
        Destroy(gameObject);
    }
}
