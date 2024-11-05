using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private LevelSystem levelSystem;

    [SerializeField] UIManager uiManager;

    public int lifes;
    public int coins;
    public int highscore;

    public int currentGameScore;
    public int currentGameCoins;

    public bool isGameOver;


    public GameData gameData;


    private void Awake()
    {
        gameData = SaveSystem.Load();
        Application.targetFrameRate = -1;
        levelSystem = GetComponent<LevelSystem>();


    }
    void Start()
    {
        currentGameScore = 0;
        currentGameCoins = 0;
        lifes = 3;

        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver == true)
        {
            gameData = SaveSystem.Load();

            CoinsCollected(currentGameCoins);

            if (currentGameScore > gameData.highscore)
            {
                SetNewHighscore(currentGameScore);
                gameData.highscore = highscore;
            }

            levelSystem.AddExperience((int)((currentGameScore / currentGameCoins) * 2));
            gameData.totalCoins += coins;
            SaveSystem.Save(gameData);

            SceneManager.LoadScene("MainMenu");
        }

        if (lifes == 0)
        {
            isGameOver = true;
        }
    }

    public void DeductLife()
    {
        lifes--;
        uiManager.DeductLifeUI();
    }
    public void GameOver()
    {
        isGameOver = true;
    }

    public void CoinsCollected(int coinValue)
    {
        coins += coinValue;
    }

    public void SetNewHighscore(int newHighScoreValue)
    {
        highscore += newHighScoreValue;
    }
}
