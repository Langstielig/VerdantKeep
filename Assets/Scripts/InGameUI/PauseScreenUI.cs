using UnityEngine;
using UnityEngine.UI;

public class PauseScreenUI : MenuUI
{
    [SerializeField] private Button continueButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button mainMenuButton;

    private void Start()
    {
        continueButton.onClick.AddListener(OnContinueClicked);
        settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
    }

    public void OnContinueClicked()
    {
        UIManagerInGame.Instance.ResumeGame();
    }

    public void OnSettingsButtonClicked()
    {
        UIManagerMainMenu.Instance.GoToSettingsMenu();
    }

    public void OnMainMenuButtonClicked()
    {
        UIManagerMainMenu.Instance.GoToMainMenu();
    }
}
