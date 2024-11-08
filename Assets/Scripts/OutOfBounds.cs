using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    GameModeScript gameModeScript;
    GameManager gameManager;

    private void Awake()
    {
        gameModeScript = GameObject.Find("GameModeManager").GetComponent<GameModeScript>();

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (gameModeScript.currentMode == GameModeScript.GameMode.SpeedStorm)
        {
            if (gameManager.lifes > 0)
            {
                gameManager.DeductLife();
                Destroy(other.gameObject);
            }
            
        }
        else { Destroy(other.gameObject); }
        
    }
}
