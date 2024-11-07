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
        GameModeMenu
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
    [SerializeField] private TextMeshProUGUI powerUpMadnessUnlockedText;

    [SerializeField] private GameObject gameModeBackgroundPage;
    [SerializeField] private GameObject fruitRushPage;
    [SerializeField] private GameObject treasureHuntPage;
    [SerializeField] private GameObject speedStormPage;
    [SerializeField] private GameObject colorFrenzyPage;
    [SerializeField] private GameObject powerUpMadnesPage;

    [SerializeField] private TextMeshProUGUI levelTextTest;
    [SerializeField] private TextMeshProUGUI experienceTextTest;
    [SerializeField] private TextMeshProUGUI experienceToNextLevelTextTest;
    [SerializeField] private TextMeshProUGUI highscoreTextTest;

    GameObject currentGameModePage;

    private LevelSystem levelSystem;
    private GameData gameData;

    public GameObject gameModePanel;
    public GameObject mainMenuPanel;
    public GameObject optionsMenuPanel;
    public GameObject shopMenuPanel;


    void Awake()
    {
        gameData = SaveSystem.Load();       // LOAD GAME DATA
        levelSystem = GetComponent<LevelSystem>();
        currentState = UIState.MainMenu;

        

        
    }
    private void Start()
    {
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
                break;
            case UIState.OptionsMenu:
                mainMenuPanel.SetActive(false);
                optionsMenuPanel.SetActive(true);
                shopMenuPanel.SetActive(false);
                gameModePanel.SetActive(false);
                UpdateDataTestTexts();
                break;
            case UIState.GameModeMenu:
                mainMenuPanel.SetActive(false);
                optionsMenuPanel.SetActive(false);
                shopMenuPanel.SetActive(false);
                gameModePanel.SetActive(true);
                CheckIfModeUnlocked();
                break;
        }

    }
    private void UpdateDataTestTexts()
    {
        levelTextTest.text = "Level : " + gameData.level.ToString();
        experienceTextTest.text = "Experience : " + gameData.currentXP.ToString();
        experienceToNextLevelTextTest.text = "Experience To Next Level : " + gameData.xpToNextlevel.ToString();
        highscoreTextTest.text = "Highscore : " + gameData.highscore.ToString();
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

        if (gameData.modePowerUpMadnessUnlocked == true)
        {
            powerUpMadnessUnlockedText.text = "Unlocked";
        }
        else { powerUpMadnessUnlockedText.text = "locked"; }
    }

    public void GetOneLevelUP()
    {
        levelSystem.LevelUpManually();
        UpdateUITexts();
    }

    public void ResetDataFile()
    {
        SaveSystem.ResetData();
        levelSystem.ResetData();
        UpdateUITexts();
        UpdateXPBar();
    }

    private void UpdateUITexts()
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
        SceneManager.LoadScene("Game");
    }

    public void OnShopButtonPressed()
    {
        currentState = UIState.ShopMenu;
    }

    public void OnOptionsButtonPressed()
    {
        currentState = UIState.OptionsMenu;
    }

    public void OnHomeButtonPressed()
    {
        currentState = UIState.MainMenu;
        
            
    }

    public void OnRateUsPressed()
    {
        // Implement rate us logic
    }

    public void OnShareButtonPressed()
    {
        // Implement share logic
    }
    
    public void OnGameModeButtonPressed()
    {
        currentState = UIState.GameModeMenu;
        gameModeBackgroundPage.SetActive(true);
    }


    public void BackButtonPressedInGameModePage()
    {
        gameModeBackgroundPage.SetActive(true);


            currentGameModePage.SetActive(false);

            currentGameModePage = null;
        
    }

    public void OnFruitRushButtonPressed()
    {
        gameModeBackgroundPage.SetActive(false);
        fruitRushPage.SetActive(true);
        currentGameModePage = fruitRushPage;
    }
    public void OnTreasureHuntButtonPressed()
    {
        gameModeBackgroundPage.SetActive(false);
        treasureHuntPage.SetActive(true);
        currentGameModePage = treasureHuntPage;
    }
    public void OnSpeedStormButtonPressed()
    {
        gameModeBackgroundPage.SetActive(false);
        speedStormPage.SetActive(true);
        currentGameModePage = speedStormPage;
    }
    public void OnColorFrenzyButtonPressed()
    {
        gameModeBackgroundPage.SetActive(false);
        colorFrenzyPage.SetActive(true);
        currentGameModePage = colorFrenzyPage;
    }
    public void OnPowerUpMadnessButtonPressed()
    {
        gameModeBackgroundPage.SetActive(false);
        powerUpMadnesPage.SetActive(true);
        currentGameModePage = powerUpMadnesPage;
    }
}