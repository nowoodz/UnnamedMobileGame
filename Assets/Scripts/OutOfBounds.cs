using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    [SerializeField] private GameSounds gameSounds;
    [SerializeField] private GameModeScript gameModeScript;
    [SerializeField] private GameManager gameManager;

    private void Awake()
    {


    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (gameModeScript.currentMode == GameModeScript.GameMode.SpeedStorm)
        {
            if (gameManager.lifes > 0)
            {
                gameSounds.PlayWrongSound();
                gameManager.DeductLife();
                Destroy(other.gameObject);
            }
            
        }
        else { Destroy(other.gameObject); }
        
    }
}
