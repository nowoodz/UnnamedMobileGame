using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public enum UIState
    {
        GameModeStart,
        Game,
        Options,
        GameEnd
    }

    private GameData gameData;

    public HandleTouchInput handleTouchInput;

    public UIState currentState;

    [SerializeField] private GameObject gameModeStartPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject gameEndPanel;
    

    [SerializeField] private Image life_1;
    [SerializeField] private Image life_2;
    [SerializeField] private Image life_3;

    [SerializeField] private Sprite heartRedSprite;
    [SerializeField] private Sprite heartBlackSprite;

    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameModeScript gameModeScript;

    [SerializeField] private TextMeshProUGUI gameModeText;
    [SerializeField] private TextMeshProUGUI scoreIntText;
    [SerializeField] private TextMeshProUGUI coinsIntText;

    // END GAME PANEL TEXTS AND UI

   
    [SerializeField] private TextMeshProUGUI coinsCollectedText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI xpGainedText;
    [SerializeField] private TextMeshProUGUI newHighScoreText;

    [SerializeField] private GameObject adRetryButton;

    void Awake()
    {
        gameData = SaveSystem.Load();
        scoreIntText.text = gameManager.currentGameScore.ToString();
        coinsIntText.text = gameManager.currentGameCoins.ToString();

        currentState = UIState.GameModeStart;

        life_1.sprite = heartRedSprite;
        life_2.sprite = heartRedSprite;
        life_3.sprite = heartRedSprite;
    }

    // Update is called once per frame
    void Update()
    {
        scoreIntText.text = gameManager.currentGameScore.ToString();
        coinsIntText.text = gameManager.currentGameCoins.ToString();

        switch (currentState)
        {
            case UIState.GameModeStart:
                gameModeStartPanel.SetActive(true);
                gamePanel.SetActive(false);
                optionsPanel.SetActive(false);
                gameEndPanel.SetActive(false);
                gameModeText.text = gameModeScript.currentMode.ToString();

                gameManager.isGameStarted = false;
                break;
            case UIState.Game:
                gameModeStartPanel.SetActive(false);
                gamePanel.SetActive(true);
                optionsPanel.SetActive(false);
                gameEndPanel.SetActive(false);
                gameManager.isGameStarted = true;

                Time.timeScale = 1f;
                gameManager.isGamePaused = false;
                break;
            case UIState.Options:
                Time.timeScale = 0f;
                gameModeStartPanel.SetActive(false);
                gamePanel.SetActive(false);
                optionsPanel.SetActive(true);
                gameEndPanel.SetActive(false);
                gameManager.isGamePaused = true;
                break;
            case UIState.GameEnd:
                gameModeStartPanel.SetActive(false);
                gamePanel.SetActive(false);
                optionsPanel.SetActive(false);
                gameEndPanel.SetActive(true);

                if (gameManager.lifes == 0)
                {
                    adRetryButton.SetActive(true);
                }else {  adRetryButton.SetActive(false); }
                break;
        }


    }

    public void SetGameEndUI()
    {
        gameData = SaveSystem.Load();

        scoreText.text = "Score \n" + gameManager.currentGameScore;
        coinsCollectedText.text = "Coins Collected \n" + gameManager.currentGameCoins;
        xpGainedText.text = "Experience Gained \n" + (int)((gameManager.currentGameScore / 1.8));

        if (gameManager.currentGameScore > gameData.highscore)
        {
            newHighScoreText.gameObject.SetActive(true);
        }else { newHighScoreText.gameObject.SetActive(false); }

    }

    public void DeductLifeUI()
    {
        if (gameManager.lifes == 3)
        {
            life_1.sprite = heartRedSprite;
            life_2.sprite = heartRedSprite;
            life_3.sprite = heartRedSprite;
        }
        else if (gameManager.lifes == 2)
        {
            life_1.sprite = heartRedSprite;
            life_2.sprite = heartRedSprite;
            life_3.sprite = heartBlackSprite;
        }
        else if (gameManager.lifes == 1)
        {
            life_1.sprite = heartRedSprite;
            life_2.sprite = heartBlackSprite;
            life_3.sprite = heartBlackSprite;
        }
        else if (gameManager.lifes == 0)
        {
            life_1.sprite = heartBlackSprite;
            life_2.sprite = heartBlackSprite;
            life_3.sprite = heartBlackSprite;
        }
    }
    public void GameModeStartPlayButton()
    {
        currentState = UIState.Game;
    }

    public void OptionButtonPressed()
    {
        currentState = UIState.Options;
    }
    public void OptionsBackButton()
    {
        currentState = UIState.Game;
    }

    public void GameOver()
    {
        gameManager.GameOver();
    }

    public void PlayAgainPressed()
    {
        gameManager.EndGameAndSaveVariablesAndReplay();
    }

    public void HomePressed()
    {
        gameManager.EndGameAndSaveVariablesAndGoToMenu();
    }
}
