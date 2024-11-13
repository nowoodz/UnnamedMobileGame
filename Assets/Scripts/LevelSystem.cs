using System.Collections;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    GameData gameData;
    [SerializeField] int level;
    [SerializeField] int xp;
    [SerializeField] int xpToNextLevel;
    [SerializeField] int highscore;




    void Awake()
    {
        gameData = SaveSystem.Load();
        level = gameData.level;
        xp = gameData.currentXP;
        xpToNextLevel = gameData.xpToNextlevel;
        highscore = gameData.highscore;

    }

    public void AddExperience(int xpAmount)
    {
        gameData = SaveSystem.Load();

        xp += xpAmount;
        gameData.currentXP += xpAmount; 

        while (xp >= xpToNextLevel) // Check if the player can level up
        {
            LevelUP();
        }

        Debug.Log("Experience added to Data : " + xpAmount);
        SaveSystem.Save(gameData);
    }

    private void LevelUP()
    {
        gameData = SaveSystem.Load();

        int difference = xp - xpToNextLevel;

        // Reset xp to leftover after leveling up
        xp = difference;
        gameData.currentXP = xp;

        // Calculate and update XP needed for the next level
        xpToNextLevel = CalculateNextLevelXP();
        gameData.xpToNextlevel = xpToNextLevel;


        level++;
        gameData.level++;

        gameData.CheckUnlockModes();

        Debug.Log("Leveled Up to " + gameData.level.ToString());
        SaveSystem.Save(gameData);
    }


    public void LevelUpManually()
    {
        int xpDifference = xpToNextLevel - xp;

        AddExperience(xpDifference);
    }

    private int CalculateNextLevelXP()
    {
        return Mathf.RoundToInt(xpToNextLevel * 1.2f);
    }

    public void ResetData()
    {
        StartCoroutine(ResetDataAfter2Seconds());
    }


    public IEnumerator ResetDataAfter2Seconds()
    {          
        yield return new WaitForSeconds(1);
        gameData = SaveSystem.Load();

        level = gameData.level;
        xp = gameData.currentXP;
        xpToNextLevel = gameData.xpToNextlevel;
    }
}
