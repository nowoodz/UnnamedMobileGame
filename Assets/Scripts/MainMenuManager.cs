using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class MainMenuManager : MonoBehaviour
{
    public enum UIState
    {
        MainMenu,
        ShopMenu,
        OptionsMenu,
        GameModeMenu,
        ResetGameMenu
    }

    [SerializeField] private UIState currentState;
    [SerializeField] private TextMeshProUGUI highscoreText;
    [SerializeField] private TextMeshProUGUI coinsIntText;
    [SerializeField] private TextMeshProUGUI levelsText;
    [SerializeField] private Image xpFillBar;

    //Game Mode Unlocked Text
    [SerializeField] private TextMeshProUGUI fruitRushUnlockedText;
    [SerializeField] private TextMeshProUGUI treasureHuntUnlockedText;
    [SerializeField] private TextMeshProUGUI speedStormUnlockedText;
    [SerializeField] private TextMeshProUGUI colorFrenzyUnlockedText;


    [SerializeField] private GameObject gameModeBackgroundPage;
    [SerializeField] private GameObject fruitRushPage;
    [SerializeField] private GameObject treasureHuntPage;
    [SerializeField] private GameObject speedStormPage;
    [SerializeField] private GameObject colorFrenzyPage;



    GameObject currentGameModePage;
    [SerializeField] private ButtonAudio buttonAudio;
    private LevelSystem levelSystem;
    public GameData gameData;

    public GameObject gameModePanel;
    public GameObject mainMenuPanel;
    public GameObject optionsMenuPanel;
    public GameObject shopMenuPanel;
    public GameObject resetGameConfirmationPanel;

    public bool isMusicOn;
    public bool isSoundOn;
    [SerializeField] private Sprite soundOffSprite;
    [SerializeField] private Sprite soundOnSprite;
    [SerializeField] Image soundButtonImage;
    [SerializeField] Image musicButtonImage;

    // SHOP
    [SerializeField] private GameObject shopBackgroundPanel;
    [SerializeField] private GameObject coinsBoostConfirmationPanel;
    [SerializeField] private GameObject scoreBoostConfirmationPanel;
    [SerializeField] private ShopManager shopManager;

    void Awake()
    {
        gameData = SaveSystem.Load();       // LOAD GAME DATA
        levelSystem = GetComponent<LevelSystem>();
        currentState = UIState.MainMenu;

        #region SET SOUND ICONS
        if (gameData.sound == true)
        {
            soundButtonImage.sprite = soundOnSprite;
        }
        else if (gameData.sound == false)
        {
            soundButtonImage.sprite = soundOffSprite;
        }

        if (gameData.music == true)
        {
            musicButtonImage.sprite = soundOnSprite;
        }
        else if (gameData.music == false)
        {
            musicButtonImage.sprite = soundOffSprite;
        }
        #endregion

        Application.targetFrameRate = -1;

    }
    private void Start()
    {
        isMusicOn = gameData.music;
        isSoundOn = gameData.sound;

        UpdateXPBar();
        UpdateUITexts();

    }

    void Update()
    {
        switch (currentState)
        {
            case UIState.MainMenu:
                mainMenuPanel.SetActive(true);
                optionsMenuPanel.SetActive(false);
                shopMenuPanel.SetActive(false);
                gameModePanel.SetActive(false);
                resetGameConfirmationPanel.SetActive(false);

                if (currentGameModePage != null)
                {
                    currentGameModePage.SetActive(false);
                    currentGameModePage = null;
                }


                break;
            case UIState.ShopMenu:
                mainMenuPanel.SetActive(false);
                optionsMenuPanel.SetActive(false);
                shopMenuPanel.SetActive(true);
                gameModePanel.SetActive(false);
                resetGameConfirmationPanel.SetActive(false);
                break;
            case UIState.OptionsMenu:
                mainMenuPanel.SetActive(false);
                optionsMenuPanel.SetActive(true);
                shopMenuPanel.SetActive(false);
                gameModePanel.SetActive(false);
                resetGameConfirmationPanel.SetActive(false);
                break;
            case UIState.GameModeMenu:
                mainMenuPanel.SetActive(false);
                optionsMenuPanel.SetActive(false);
                shopMenuPanel.SetActive(false);
                gameModePanel.SetActive(true);
                resetGameConfirmationPanel.SetActive(false);
                CheckIfModeUnlocked();
                break;
            case UIState.ResetGameMenu:
                mainMenuPanel.SetActive(false);
                optionsMenuPanel.SetActive(false);
                shopMenuPanel.SetActive(false);
                gameModePanel.SetActive(false);
                resetGameConfirmationPanel.SetActive(true);
                break;
        }

    }

    private void CheckIfModeUnlocked()
    {
        if (gameData.modeFruitRushUnlocked == true)
        {
            fruitRushUnlockedText.text = "Unlocked";
        }
        else { fruitRushUnlockedText.text = "locked"; }

        if (gameData.modeTreasureHuntUnlocked == true)
        {
            treasureHuntUnlockedText.text = "Unlocked";
        }
        else { treasureHuntUnlockedText.text = "locked"; }

        if (gameData.modeSpeedStormUnlocked == true)
        {
            speedStormUnlockedText.text = "Unlocked";
        }
        else { speedStormUnlockedText.text = "locked"; }

        if (gameData.modeColorFrenzyUnlocked == true)
        {
            colorFrenzyUnlockedText.text = "Unlocked";
        }
        else { colorFrenzyUnlockedText.text = "locked"; }
    }

    public void GetOneLevelUP()
    {
        levelSystem.LevelUpManually();
        UpdateUITexts();
        buttonAudio.PlayButtonSound();
    }

    public void GoBackToOptionsPanel()
    {
        currentState = UIState.OptionsMenu;
    }
    public void ConfirmationResetGame()
    {
        buttonAudio.PlayButtonSound();
        currentState = UIState.ResetGameMenu;
    }
    public void ResetDataFile()
    {
        SaveSystem.ResetData();
        levelSystem.ResetData();
        buttonAudio.PlayButtonSound();
        SceneManager.LoadScene("MainMenu");
    }

    public void UpdateUITexts()
    {
        gameData = SaveSystem.Load();
        highscoreText.text = "Highscore : " + gameData.highscore.ToString();
        coinsIntText.text = gameData.totalCoins.ToString();
        levelsText.text = "Level : " + gameData.level;
    }


    private void UpdateXPBar()
    {
        // Calculate fill amount based on XP progress
        gameData = SaveSystem.Load();
        float fillAmount = (float)gameData.currentXP / gameData.xpToNextlevel;
        xpFillBar.fillAmount = Mathf.Clamp01(fillAmount);  // Clamp to ensure it's between 0 and 1
    }

    public void OnPlayButtonPressed()
    {
        buttonAudio.PlayButtonSound();
        SceneManager.LoadScene("Game");

    }

    public void OnShopButtonPressed()
    {
        buttonAudio.PlayButtonSound();
        currentState = UIState.ShopMenu;
        shopBackgroundPanel.SetActive(true);
    }

    public void OnOptionsButtonPressed()
    {
        buttonAudio.PlayButtonSound();
        currentState = UIState.OptionsMenu;
    }

    public void OnHomeButtonPressed()
    {
        buttonAudio.PlayButtonSound();
        currentState = UIState.MainMenu;

        shopBackgroundPanel.SetActive(false);
        coinsBoostConfirmationPanel.SetActive(false);
        scoreBoostConfirmationPanel.SetActive(false);
    }

    public void OnRateUsPressed()
    {
        buttonAudio.PlayButtonSound();
        // Implement rate us logic
    }

    public void OnShareButtonPressed()
    {
        buttonAudio.PlayButtonSound();
        // Implement share logic
    }
    
    public void OnGameModeButtonPressed()
    {
        buttonAudio.PlayButtonSound();
        currentState = UIState.GameModeMenu;
        gameModeBackgroundPage.SetActive(true);
    }


    public void BackButtonPressedInGameModePage()
    {
        buttonAudio.PlayButtonSound();
        gameModeBackgroundPage.SetActive(true);


            currentGameModePage.SetActive(false);

            currentGameModePage = null;
        
    }

    public void OnFruitRushButtonPressed()
    {
        buttonAudio.PlayButtonSound();
        gameModeBackgroundPage.SetActive(false);
        fruitRushPage.SetActive(true);
        currentGameModePage = fruitRushPage;
    }
    public void OnTreasureHuntButtonPressed()
    {
        buttonAudio.PlayButtonSound();
        gameModeBackgroundPage.SetActive(false);
        treasureHuntPage.SetActive(true);
        currentGameModePage = treasureHuntPage;
    }
    public void OnSpeedStormButtonPressed()
    {
        buttonAudio.PlayButtonSound();
        gameModeBackgroundPage.SetActive(false);
        speedStormPage.SetActive(true);
        currentGameModePage = speedStormPage;
    }
    public void OnColorFrenzyButtonPressed()
    {
        buttonAudio.PlayButtonSound();
        gameModeBackgroundPage.SetActive(false);
        colorFrenzyPage.SetActive(true);
        currentGameModePage = colorFrenzyPage;
    }

    public void MusicButtonPressed()
    {
        buttonAudio.PlayButtonSound();
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
    public void SoundButtonPressed()
    {
        buttonAudio.PlayButtonSound();
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
    
    public void GoToCoinBoostConfirmation()
    {
        buttonAudio.PlayButtonSound();
        scoreBoostConfirmationPanel.SetActive(false);
        coinsBoostConfirmationPanel.SetActive(true);
        shopBackgroundPanel.SetActive(false);
    }

    public void GoToScoreBoostConfirmation()
    {
        buttonAudio.PlayButtonSound();
        scoreBoostConfirmationPanel.SetActive(true);
        coinsBoostConfirmationPanel.SetActive(false);
        shopBackgroundPanel.SetActive(false);
    }

    public void GoBackToShopState()
    {
        buttonAudio.PlayButtonSound();
        scoreBoostConfirmationPanel.SetActive(false);
        coinsBoostConfirmationPanel.SetActive(false);
        shopBackgroundPanel.SetActive(true);
    }

    public void BuyCoins2xBoost()
    {
        shopManager.BuyCoins2xBoost();
        scoreBoostConfirmationPanel.SetActive(false);
        coinsBoostConfirmationPanel.SetActive(false);
        shopBackgroundPanel.SetActive(true);
    }

    public void BuyScore2xBoost()
    {
        shopManager.BuyScore2xBoost();
        scoreBoostConfirmationPanel.SetActive(false);
        coinsBoostConfirmationPanel.SetActive(false);
        shopBackgroundPanel.SetActive(true);
    }
}