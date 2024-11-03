using UnityEngine;

public class BombPrefab : MonoBehaviour
{
    GameManager gameManager;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameObjectTouched()
    {
        gameManager.currentGameScore--;
        Destroy(gameObject);
    }
}
