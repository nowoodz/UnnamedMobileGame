using UnityEngine;
using System;

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

    public bool has2xCoinsBoost;
    public bool has2xScoreBoost;

    public string coinsBoostActivatedTime;
    public float coinsBoostDuration;

    public string scoreBoostActivatedTime;
    public float scoreBoostDuration;

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

        has2xCoinsBoost = false;
        has2xScoreBoost = false;
        coinsBoostActivatedTime = "";
        scoreBoostActivatedTime = "";
        coinsBoostDuration = 600f;
        scoreBoostDuration = 600f;
    }

    public void CheckUnlockModes()
    {
        if (level >= 2 && !modeTreasureHuntUnlocked)
        {
            modeTreasureHuntUnlocked = true;
        }

        if (level >= 5 && !modeSpeedStormUnlocked)
        {
            modeSpeedStormUnlocked = true;
        }

        if (level >= 10 && !modeColorFrenzyUnlocked)
        {
            modeColorFrenzyUnlocked = true;
        }
    }

    #region BOOSTS
    public void ActivateCoinsBoost()
    {
        coinsBoostActivatedTime = DateTime.Now.ToString(); // Store the current time as the activation time
        has2xCoinsBoost = true; // Mark the boost as active
    }

    public void ActivateScoreBoost()
    {
        scoreBoostActivatedTime = DateTime.Now.ToString(); // Store the current time as the activation time
        has2xScoreBoost = true; // Mark the boost as active
    }

    public bool IsCoinsBoostActive()
    {
        if (string.IsNullOrEmpty(coinsBoostActivatedTime)) return false; // No boost activated

        DateTime activationTime = DateTime.Parse(coinsBoostActivatedTime);
        TimeSpan timeElapsed = DateTime.Now - activationTime;

        // If the time elapsed is greater than the boost duration, reset the boost and return false
        if (timeElapsed.TotalSeconds >= coinsBoostDuration)
        {
            ResetCoinsBoost();
            return false;
        }

        return true;
    }

    public bool IsScoreBoostActive()
    {
        if (string.IsNullOrEmpty(scoreBoostActivatedTime)) return false; // No boost activated

        DateTime activationTime = DateTime.Parse(scoreBoostActivatedTime);
        TimeSpan timeElapsed = DateTime.Now - activationTime;

        // If the time elapsed is greater than the boost duration, reset the boost and return false
        if (timeElapsed.TotalSeconds >= scoreBoostDuration)
        {
            ResetScoreBoost();
            return false;
        }

        return true;
    }

    // Reset methods for each boost
    public void ResetCoinsBoost()
    {
        coinsBoostActivatedTime = ""; // Clear activation time
        has2xCoinsBoost = false; // Mark boost as inactive
        SaveSystem.Save(this);
        Debug.Log("Coins Boost Reset");
    }

    public void ResetScoreBoost()
    {
        scoreBoostActivatedTime = ""; // Clear activation time
        has2xScoreBoost = false; // Mark boost as inactive
        SaveSystem.Save(this);

        Debug.Log("Score Boost Reset");
    }

    #endregion
}