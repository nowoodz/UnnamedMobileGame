using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

[System.Serializable]
public class GameData
{

    public int totalCoins;
    public int highscore;

    public int level;
    public int currentXP;
    public int xpToNextlevel;

    public bool sound;
    public bool music;

    public bool modeFruitRushUnlocked;
    public bool modeTreasureHuntUnlocked;
    public bool modeSpeedStormUnlocked;
    public bool modeColorFrenzyUnlocked;
    public bool modePowerUpMadnessUnlocked;


    public GameData()
        {
        totalCoins = 0;
        highscore = 0;
        level = 1;
        currentXP = 0;
        xpToNextlevel = 100;
        sound = true;
        music = true;

        modeFruitRushUnlocked = true;
        modeTreasureHuntUnlocked = false;
        modeSpeedStormUnlocked = false;
        modeColorFrenzyUnlocked = false;
        modePowerUpMadnessUnlocked = false;
    }

    public void CheckUnlockModes()
    {
        if (level >= 2 && !modeTreasureHuntUnlocked)
        {
            modeTreasureHuntUnlocked = true;
            Debug.Log("Treasure Hunt mode unlocked!");
        }

        if (level >= 5 && !modeSpeedStormUnlocked)
        {
            modeSpeedStormUnlocked = true;
            Debug.Log("Speed Storm mode unlocked!");
        }

        if (level >= 10 && !modeColorFrenzyUnlocked)
        {
            modeColorFrenzyUnlocked = true;
            Debug.Log("Color Frenzy mode unlocked!");
        }

        if (level >= 15 && !modePowerUpMadnessUnlocked)
        {
            modePowerUpMadnessUnlocked = true;
            Debug.Log("PowerUp Madness mode unlocked!");
        }
    }
   
}
