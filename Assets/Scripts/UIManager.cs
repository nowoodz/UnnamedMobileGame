using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public enum UIState
    {
        GameModeStart,
        Game,
        Options
    }

    [SerializeField] private UIState currentState;
    [SerializeField] private GameObject gameModeStartPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject optionsPanel;

    [SerializeField] private Image life_1;
    [SerializeField] private Image life_2;
    [SerializeField] private Image life_3;

    [SerializeField] private Sprite heartRedSprite;
    [SerializeField] private Sprite heartBlackSprite;

    [SerializeField] private GameManager gameManager;

    [SerializeField] private TextMeshProUGUI scoreIntText;
    [SerializeField] private TextMeshProUGUI coinsIntText;
        
    void Awake()
    {
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
                break;
            case UIState.Game:
                gameModeStartPanel.SetActive(false);
                gamePanel.SetActive(true);
                optionsPanel.SetActive(false);
                break;
            case UIState.Options:
                gameModeStartPanel.SetActive(false);
                gamePanel.SetActive(false);
                optionsPanel.SetActive(true);
                break;
        }


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
}
