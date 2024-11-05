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


    public bool modeTreasureHuntUnlocked;
    public bool modeSpeedStormUnlocked;
    public bool modeColorFrenzyUnlocked;
    public bool modeDoubleTroubleUnlocked;
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

        modeTreasureHuntUnlocked = false;
        modeSpeedStormUnlocked = false;
        modeColorFrenzyUnlocked = false;
        modeDoubleTroubleUnlocked = false;
        modePowerUpMadnessUnlocked = false;
    }
}
