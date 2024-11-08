using UnityEngine;

public class PowerUPObject : MonoBehaviour
{

    Vector3 rotationY = new Vector3(0,1,0);
    private float rotationSpeed = 50f;

    

    private void Awake()
    {
        SelectRandomPowerUP();
    }

    private void Update()
    {   
        transform.Rotate(rotationY * rotationSpeed * Time.deltaTime);
    }


    private void SelectRandomPowerUP()
    {

    }
}
