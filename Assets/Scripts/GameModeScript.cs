using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class GameModeScript : MonoBehaviour
{

    GameData gameData;

    public enum GameMode
    {
        NoGameMode,
        FruitRush,
        TreasureHunt,
        SpeedStorm,
        ColorFrenzy,
        PowerUpMadness
    }

    public GameMode currentMode;

    public string currentModeString;

    [SerializeField] UIManager uiManager;
    [SerializeField] GameManager gameManager;

    // GAME MODE VARIABLES

    private Vector3 defaultGravity = new Vector3(0, -1f, 0);
    private Vector3 speedStormGravity = new Vector3(0, -3f, 0);
    private Vector3 colorFrenzyGravity = new Vector3(0, -3f, 0);

    // Color Frenzy

    public string currentColor;
    [SerializeField] string[] gameModeColors;
    private bool resetingColor;
    private int randomInterval;

    private void Awake()
    {
       
        gameData = SaveSystem.Load();
        resetingColor = false;
    }

    private void Update()
    {
        switch (currentMode)
        {
            case GameMode.NoGameMode:

                break;
            case GameMode.FruitRush:
                currentModeString = currentMode.ToString();

                break;
            case GameMode.TreasureHunt:
                currentModeString = currentMode.ToString();
                break;
            case GameMode.SpeedStorm:
                currentModeString = currentMode.ToString();
                break;
            case GameMode.ColorFrenzy:
                currentModeString = currentMode.ToString();

                uiManager.SetColorFrenzyUI();
                if (resetingColor == false)
                {
                    StartCoroutine(GenerateColorWithTimer());
                }
                
                break;
            case GameMode.PowerUpMadness:
                currentModeString = currentMode.ToString();
                break;
        }
    }


    public void FruitRushButtonClicked()
    {
        if (gameData.modeFruitRushUnlocked == true)
        {
            currentMode = GameMode.FruitRush;
            uiManager.StartGame();

            Physics.gravity = defaultGravity;
        }
        else
        {
            uiManager.FlashGameModeLockedText();
        }
    }

    public void TreasureHuntButtonClicked()
    {
        if (gameData.modeTreasureHuntUnlocked == true)
        {
            currentMode = GameMode.TreasureHunt;
            uiManager.StartGame();

            Physics.gravity = defaultGravity;
        }
        else
        {
            uiManager.FlashGameModeLockedText();
        }
    }
    
    public void SpeedStormButtonClicked()
    {
        if (gameData.modeSpeedStormUnlocked == true)
        {
            currentMode = GameMode.SpeedStorm;
            uiManager.StartGame();

            Physics.gravity = speedStormGravity;
        }
        else
        {
            uiManager.FlashGameModeLockedText();
        }
    }

    public void ColorFrenzyButtonClicked()
    {
        if (gameData.modeColorFrenzyUnlocked == true)
        {
            currentMode = GameMode.ColorFrenzy;
            uiManager.StartGame();

            Physics.gravity = colorFrenzyGravity;
        }
        else
        {
            uiManager.FlashGameModeLockedText();
        }
    }

    public void PowerUpMadnessButtonClicked()
    {
        if (gameData.modePowerUpMadnessUnlocked == true)
        {
            currentMode = GameMode.PowerUpMadness;
            uiManager.StartGame();

            Physics.gravity = defaultGravity;
        }
        else
        {
            uiManager.FlashGameModeLockedText();
        }
    }
    IEnumerator GenerateColorWithTimer()
    {
        resetingColor = true;
        randomInterval = Random.Range(5, 10);
        currentColor = GenerateRandomColor();


        uiManager.SetCurrentColorTextColorFrenzy();
        yield return new WaitForSeconds(randomInterval);

        resetingColor = false;
    }


    public string GenerateRandomColor()
    {
        return gameModeColors[Random.Range(0, gameModeColors.Length)];
    }

    public void ColorObjectTouched(string color)
    {
        if (color == currentColor)
        {
            gameManager.currentGameCoins++;
            gameManager.currentGameScore += 10;
        }
        else if (color != currentColor)
        {
            gameManager.DeductLife();
        }
    }
}
