using UnityEngine;

public class ButtonAudio : MonoBehaviour
{
    GameData gameData;

    public AudioClip buttonClip;
    public AudioClip successfulPurchaseClip;
    public AudioClip failedPurchaseClip;
    private AudioSource audioSource;

    private void Awake()
    {
        gameData = SaveSystem.Load();
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        
    }

    public void PlayButtonSound()
    {
        gameData = SaveSystem.Load();

        if (gameData.sound == true)
        {
            audioSource.clip = buttonClip;
            audioSource.Play();
        }
        
    }

    public void PlaySuccessfulPurchaseButtonSound()
    {
        gameData = SaveSystem.Load();

        if (gameData.sound == true)
        {
            audioSource.clip = successfulPurchaseClip;
            audioSource.Play();
        }
    }

    public void PlayFailedPurchaseButtonSound()
    {
        gameData = SaveSystem.Load();

        if (gameData.sound == true)
        {
            audioSource.clip = failedPurchaseClip;
            audioSource.Play();
        }
    }
}
