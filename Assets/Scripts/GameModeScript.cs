using System.Collections.Generic;
using UnityEngine;

public class GameModeScript : MonoBehaviour
{

    GameData gameData;

    public enum GameMode
    {
        FruitRush,
        TreasureHunt,
        SpeedStorm,
        ColorFrenzy,
        DoubleTrouble,
        PowerUpMadness
    }

    public GameMode currentMode;

  

    private void Awake()
    {
        gameData = SaveSystem.Load();
    }
    private void Start()
    {
        SelectRandomMode();

    }
    private void Update()
    {
        switch (currentMode)
        {
            case GameMode.FruitRush:
                
                break;
            case GameMode.TreasureHunt:
                
                break;
            case GameMode.SpeedStorm:
                
                break;
            case GameMode.ColorFrenzy:
                
                break;
            case GameMode.DoubleTrouble:
               
                break;
            case GameMode.PowerUpMadness:
                
                break;
        }
    }

    private void SelectRandomMode()
    {
        gameData = SaveSystem.Load();

        List<GameMode> availableModes = new List<GameMode>();

        // Check which modes are unlocked and add them to the list
        if (gameData.modeFruitRushUnlocked == true)
            availableModes.Add(GameMode.FruitRush);
        if (gameData.modeTreasureHuntUnlocked == true)
            availableModes.Add(GameMode.TreasureHunt);
        if (gameData.modeSpeedStormUnlocked == true)
            availableModes.Add(GameMode.SpeedStorm);
        if (gameData.modeColorFrenzyUnlocked == true)
            availableModes.Add(GameMode.ColorFrenzy);
        if (gameData.modeDoubleTroubleUnlocked == true)
            availableModes.Add(GameMode.DoubleTrouble);
        if (gameData.modePowerUpMadnessUnlocked == true)
            availableModes.Add(GameMode.PowerUpMadness);

        // Randomly select a mode from the available modes
        if (availableModes.Count > 0)
        {
            currentMode = availableModes[Random.Range(0, availableModes.Count)];
        }
        else
        {
            Debug.LogError("No game modes available to select!");
        }
    }
}
