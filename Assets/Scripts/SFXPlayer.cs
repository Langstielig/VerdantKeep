using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    public static SFXPlayer Instance;

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

        float startVolume = GameProgressManager.GetSFXVolume();
        audioSource.volume = startVolume;
    }

    public void ChangeSFXVolume(float volume)
    {
        audioSource.volume = volume;
        GameProgressManager.SetSFXVolume(volume);
    }

    public float GetSFXVolume()
    {
        return audioSource.volume;
    }

    public void PlaySFX(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
}
