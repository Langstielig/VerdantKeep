using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer Instance;

    private AudioSource audioSource;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();

        float startVolume = GameProgressManager.GetMusicVolume();
        audioSource.volume = startVolume;
    }

    public void ChangeMusicVolume(float volume)
    {
        audioSource.volume = volume;
        GameProgressManager.SetMusicVolume(volume);
    }

    public float GetMusicVolume()
    {
        return audioSource.volume;
    }
}
