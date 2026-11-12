using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuUI : MenuUI
{
    [SerializeField] private Button backButton;
    [SerializeField] private Button eraseProgressButton;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        backButton.onClick.RemoveAllListeners();
        eraseProgressButton.onClick.RemoveAllListeners();
        musicSlider.onValueChanged.RemoveAllListeners();
        sfxSlider.onValueChanged.RemoveAllListeners();

        backButton.onClick.AddListener(OnBackClicked);
        eraseProgressButton.onClick.AddListener(OnEraseProgressClicked);
        musicSlider.onValueChanged.AddListener(OnMusicSliderChanged);
        sfxSlider.onValueChanged.AddListener(OnSFXChanged);

        musicSlider.SetValueWithoutNotify(MusicPlayer.Instance.GetMusicVolume());
        sfxSlider.SetValueWithoutNotify(SFXPlayer.Instance.GetSFXVolume());
    }

    public void OnBackClicked()
    {
        Debug.Log("On back clicked");
        UIManagerMainMenu.Instance.GoToMainMenu();
    }

    public void OnEraseProgressClicked()
    {
        GameProgressManager.ResetProgress();
    }

    public void OnMusicSliderChanged(float value)
    {
        MusicPlayer.Instance.ChangeMusicVolume(value);
    }

    public void OnSFXChanged(float value)
    {
        SFXPlayer.Instance.ChangeSFXVolume(value);
    }
}
