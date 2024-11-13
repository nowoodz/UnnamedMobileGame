using UnityEngine;

public class MenuAudio : MonoBehaviour
{
    public GameData gameData;
    [SerializeField] MainMenuManager mainMenuManager;

    // MUSIC
    public AudioClip[] musicClips;
    private AudioSource audioSource;
    private int lastSongIndex = -1; // Store the last song index to avoid consecutive repeats

    void Start()
    {
        gameData = SaveSystem.Load();

        audioSource = GetComponent<AudioSource>();

        // Start by playing a random song if music is on
        if (musicClips.Length > 0 && mainMenuManager.isMusicOn)
        {
            PlayRandomSong();
        }
    }

    void Update()
    {
        // Check if music is enabled in the MainMenuManager
        if (mainMenuManager.isMusicOn)
        {
            // If the song has finished, play a new random one
            if (!audioSource.isPlaying)
            {
                PlayRandomSong();
            }
        }
        else if (audioSource.isPlaying)
        {
            // Stop music if turned off
            audioSource.Stop();
        }
    }

    void PlayRandomSong()
    {
        int randomIndex;

        // Ensure we don’t repeat the last song immediately
        do
        {
            randomIndex = Random.Range(0, musicClips.Length);
        } while (musicClips.Length > 1 && randomIndex == lastSongIndex);

        audioSource.clip = musicClips[randomIndex];
        audioSource.Play();

        // Update last song index
        lastSongIndex = randomIndex;
    }

}