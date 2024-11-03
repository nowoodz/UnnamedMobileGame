using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int coins;
    public int highscore;

    public int currentGameScore;
    public int currentGameCoins;

    void Start()
    {
        currentGameScore = 0;
        currentGameCoins = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
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
