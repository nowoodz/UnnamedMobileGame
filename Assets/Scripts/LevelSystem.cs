using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    GameData gameData;
    [SerializeField]int level;
    [SerializeField]int xp;
    [SerializeField]int xpToNextLevel;


    void Awake()
    {
        gameData = SaveSystem.Load();

        level = gameData.level;
        xp = gameData.currentXP;
        xpToNextLevel = gameData.xpToNextlevel;

    }

    private void Update()
    {

        AddExperience(0);


    }
    public void AddExperience(int xpAmount)
    {
        xp += xpAmount;

        while (xp >= xpToNextLevel) // Check if the player can level up
        {
            LevelUP();
        }

        gameData.currentXP = xp;
        SaveSystem.Save(gameData);

    }

    private void LevelUP()
    {
        int difference = xp - xpToNextLevel;

        // Reset xp to leftover after leveling up
        xp = difference;
        gameData.currentXP = xp;

        // Calculate and update XP needed for the next level
        xpToNextLevel = CalculateNextLevelXP();
        gameData.xpToNextlevel = xpToNextLevel;

        level++;
        gameData.level++;
        SaveSystem.Save(gameData);
    }

    private int CalculateNextLevelXP()
    {
            return Mathf.RoundToInt(xpToNextLevel * 1.2f);
    }

    public void ResetData()
    {
        gameData = SaveSystem.Load();

        level = gameData.level; 
        xp = gameData.currentXP;
        xpToNextLevel = gameData.xpToNextlevel;
    }
}
