using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

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

    [SerializeField] private GameObject gameModeLockedUI;
    // Game UI

    [SerializeField] private TextMeshProUGUI scoreIntText;
    [SerializeField] private TextMeshProUGUI coinsIntText;
    [SerializeField] private TextMeshProUGUI currentGameModeText;
    [SerializeField] private TextMeshProUGUI colorFrenzyCurrentColorText;

    // END GAME PANEL TEXTS AND UI


    [SerializeField] private TextMeshProUGUI coinsCollectedText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI xpGainedText;
    [SerializeField] private TextMeshProUGUI newHighScoreText;

    // AUDIO
    [SerializeField] private GameSounds gameSounds;
    [SerializeField] private Sprite soundOffSprite;
    [SerializeField] private Sprite soundOnSprite;
    [SerializeField] Image soundButtonImage;
    [SerializeField] Image musicButtonImage;

    public bool isMusicOn;
    public bool isSoundOn;

    void Awake()
    {
        gameData = SaveSystem.Load();
        scoreIntText.text = gameManager.currentGameScore.ToString();
        coinsIntText.text = gameManager.currentGameCoins.ToString();

        currentState = UIState.GameModeStart;

        life_1.sprite = heartRedSprite;
        life_2.sprite = heartRedSprite;
        life_3.sprite = heartRedSprite;

        gameModeLockedUI.SetActive(false);

        // Set Sound Sprites if Sound ON or OFF

        if (gameData.sound == true)
        {
            soundButtonImage.sprite = soundOnSprite;
        }else if (gameData.sound == false)
        {
            soundButtonImage.sprite = soundOffSprite;
        }

        if (gameData.music == true)
        {
            musicButtonImage.sprite = soundOnSprite;
        }else if (gameData.music == false)
        {
            musicButtonImage.sprite = soundOffSprite;
        }
    }

    private void Start()
    {
        isMusicOn = gameData.music;
        isSoundOn = gameData.sound;
    }
    // Update is called once per frame
    void Update()
    {
        scoreIntText.text = gameManager.currentGameScore.ToString();
        coinsIntText.text = gameManager.currentGameCoins.ToString();

        switch (currentState)
        {
            case UIState.GameModeStart:
                Time.timeScale = 1f;
                gameModeStartPanel.SetActive(true);
                gamePanel.SetActive(false);
                optionsPanel.SetActive(false);
                gameEndPanel.SetActive(false);
                gameManager.isGameStarted = false;
                break;
            case UIState.Game:
                Time.timeScale = 1f;
                gameModeStartPanel.SetActive(false);
                gamePanel.SetActive(true);
                optionsPanel.SetActive(false);
                gameEndPanel.SetActive(false);
                gameManager.isGameStarted = true;
                currentGameModeText.text = gameModeScript.currentModeString;
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
                Time.timeScale = 1f;
                gameModeStartPanel.SetActive(false);
                gamePanel.SetActive(false);
                optionsPanel.SetActive(false);
                gameEndPanel.SetActive(true);
                gameManager.isGamePaused = false;
                break;
        }


    }
    public void FlashGameModeLockedText()
    {
        StartCoroutine(FlashGameModeLockedTextCoroutine());
    }

    IEnumerator FlashGameModeLockedTextCoroutine()
    {
        gameModeLockedUI.SetActive(true);
        yield return new WaitForSeconds(2);
        gameModeLockedUI.SetActive(false);

    }
    public void SoundButtonPressed()
    {
        gameSounds.PlayButtonSound();
        if (isSoundOn == true)
        {
            soundButtonImage.sprite = soundOffSprite;
            isSoundOn = false;
            gameData.sound = false;
            SaveSystem.Save(gameData);
        }
        else if (isSoundOn == false)
        {
            soundButtonImage.sprite = soundOnSprite;
            isSoundOn = true;
            gameData.sound = true;
            SaveSystem.Save(gameData);
        }
    }
    public void MusicButtonPressed()
    {
        gameSounds.PlayButtonSound();
        if (isMusicOn == true)
        {
            musicButtonImage.sprite = soundOffSprite;
            isMusicOn = false;
            gameData.music = false;
            SaveSystem.Save(gameData);
        }
        else if (isMusicOn == false)
        {
            musicButtonImage.sprite = soundOnSprite;
            isMusicOn = true;
            gameData.music = true;
            SaveSystem.Save(gameData);
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
    public void StartGame()
    {
        currentState = UIState.Game;
    }

    public void OptionButtonPressed()
    {
        gameSounds.PlayButtonSound();
        currentState = UIState.Options;
    }
    public void OptionsBackButton()
    {
        gameSounds.PlayButtonSound();
        currentState = UIState.Game;
    }

    public void GameOver()
    {
        gameManager.GameOver();
    }

    public void PlayAgainPressed()
    {
        gameSounds.PlayButtonSound();
        gameManager.EndGameAndSaveVariablesAndReplay();
    }

    public void HomePressed()
    {
        gameSounds.PlayButtonSound();
        gameManager.EndGameAndSaveVariablesAndGoToMenu();
    }

    public void GoBackToMainMenuButtonPressed()
    {
        gameSounds.PlayButtonSound();
        SceneManager.LoadScene("MainMenu");
    }

    public void SetCurrentColorTextColorFrenzy()
    {
        colorFrenzyCurrentColorText.text = gameModeScript.currentColor;
    }

    public void SetColorFrenzyUI()
    {
        colorFrenzyCurrentColorText.gameObject.SetActive(true);
    }
}
