using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    [SerializeField] private TextMeshProUGUI scoreIntText;
    [SerializeField] private TextMeshProUGUI coinsIntText;

    void Awake()
    {
        scoreIntText.text = gameManager.currentGameScore.ToString();
        coinsIntText.text = gameManager.currentGameCoins.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        scoreIntText.text = gameManager.currentGameScore.ToString();
        coinsIntText.text = gameManager.currentGameCoins.ToString();
    }
}
