using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] LevelSystem levelSystem;
    [SerializeField] UIManager uiManager;
    [SerializeField] GameSounds gameSounds;
   

    public int lifes;
    public int highscore;

    public int currentGameScore;
    public int currentGameCoins;
    public bool isGameStarted;
    public bool isGameOver;
    public bool isGamePaused;

    public GameData gameData;

    private void Awake()
    {

        gameData = SaveSystem.Load();

    }
    void Start()
    {
        currentGameScore = 0;
        currentGameCoins = 0;
        highscore = 0;
        lifes = 3;

        isGameOver = false;
        isGamePaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver == true)
        {
            uiManager.currentState = UIManager.UIState.GameEnd;
            uiManager.SetGameEndUI();

        }

        if (lifes == 0)
        {
            isGameOver = true;
        }
    }

    public void EndGameAndSaveVariablesAndGoToMenu()
    {
        gameData = SaveSystem.Load();

        gameData.totalCoins += currentGameCoins;

        if (currentGameScore > gameData.highscore)
        {
            gameData.highscore = currentGameScore;

        }
        SaveSystem.Save(gameData);

        levelSystem.AddExperience((int)((currentGameScore / 1.8)));

        SceneManager.LoadScene("MainMenu");
    }


    public void EndGameAndSaveVariablesAndReplay()
    {

        gameData = SaveSystem.Load();

        gameData.totalCoins += currentGameCoins;

        
        if (currentGameScore > gameData.highscore)
        {
            gameData.highscore = currentGameScore;
        }
        SaveSystem.Save(gameData);

        levelSystem.AddExperience((int)((currentGameScore / 1.8)));

        SceneManager.LoadScene("Game");
    }
    


    public void DeductLife()
    {
        gameSounds.PlayWrongSound();
        lifes--;
        uiManager.DeductLifeUI();
    }
    public void GameOver()
    {
        isGameOver = true;
    }

}
