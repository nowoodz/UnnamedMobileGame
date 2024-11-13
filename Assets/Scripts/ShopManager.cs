using UnityEngine;

public class ShopManager : MonoBehaviour
{
    private GameData gameData;
    [SerializeField] MainMenuManager mainMenuManager;
    [SerializeField] ButtonAudio buttonAudio;

    private int requiredCoinsForCoins2xBoost = 100;
    private int requiredCoinsForScore2xBoost = 200;

    private void Awake()
    {
        gameData = SaveSystem.Load();

        // Check if boosts are active without resetting them prematurely
        Debug.Log("Loaded Coins Boost Activation Time: " + gameData.coinsBoostActivatedTime);
        Debug.Log("Loaded Score Boost Activation Time: " + gameData.scoreBoostActivatedTime);

        if (gameData.IsCoinsBoostActive())
        {
            Debug.Log("Coins 2x Boost is still active.");
        }
        else
        {
            Debug.Log("No Coins Boost Active.");
        }

        if (gameData.IsScoreBoostActive())
        {
            Debug.Log("Score 2x Boost is still active.");
        }
        else
        {
            Debug.Log("No Score Boost Active");
        }
    }

    public void BuyCoins2xBoost()
    {
        gameData = SaveSystem.Load();

        if (gameData.totalCoins >= requiredCoinsForCoins2xBoost && gameData.IsCoinsBoostActive() == false) // Has enough coins to buy and boost is not active
        {
            gameData.totalCoins -= requiredCoinsForCoins2xBoost;
            gameData.ActivateCoinsBoost();
            SaveSystem.Save(gameData);
            mainMenuManager.UpdateUITexts();
            Debug.Log("Bought Coins 2x Boost!");
            buttonAudio.PlaySuccessfulPurchaseButtonSound();
        }
        else
        {
            Debug.Log("Not Enough Coins or Boost is Active");
            buttonAudio.PlayFailedPurchaseButtonSound();
        }
    }

    public void BuyScore2xBoost()
    {
        gameData = SaveSystem.Load();

        if (gameData.totalCoins >= requiredCoinsForScore2xBoost && gameData.IsScoreBoostActive() == false) // Has enough coins to buy and boost is not active
        {
            gameData.totalCoins -= requiredCoinsForScore2xBoost;
            gameData.ActivateScoreBoost();
            SaveSystem.Save(gameData);
            mainMenuManager.UpdateUITexts();
            Debug.Log("Bought Score 2x Boost!");
            buttonAudio.PlaySuccessfulPurchaseButtonSound();
        }
        else
        {
            Debug.Log("Not Enough Coins or Boost is Active");
            buttonAudio.PlayFailedPurchaseButtonSound();
        }
    }

    // Optional: Reset boost status on game load
    private void CheckBoostsOnLoad()
    {
        if (!gameData.IsCoinsBoostActive())
        {
            Debug.Log("Coins 2x Boost expired.");
        }

        if (!gameData.IsScoreBoostActive())
        {
            Debug.Log("Score 2x Boost expired.");
        }
    }
}