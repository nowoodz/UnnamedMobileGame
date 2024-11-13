using UnityEngine;

public class GameSounds : MonoBehaviour
{
    private GameData gameData;
    private AudioSource audioSource;


    [SerializeField] private AudioClip buttonClip;
    [SerializeField] private AudioClip pickupClip;
    [SerializeField] private AudioClip wrongClip;


    private void Awake()
    {
        gameData = SaveSystem.Load();
        audioSource = GetComponent<AudioSource>();
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

    public void PlayPickupSound()
    {
        gameData = SaveSystem.Load();
        if (gameData.sound == true)
        {
            audioSource.clip = pickupClip;
            audioSource.Play();
        }
    }

    public void PlayWrongSound()
    {
        gameData = SaveSystem.Load();
        if (gameData.sound == true)
        {
            audioSource.clip = wrongClip;
            audioSource.Play();
        }
    }

}
