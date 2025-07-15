using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MenuUI
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button aboutButton;
    [SerializeField] private Button quitButton;

    private void Start()
    {
        startButton.onClick.AddListener(OnStartClicked);
        continueButton.onClick.AddListener(OnContinueClicked);
        settingsButton.onClick.AddListener(OnSettingsClicked);
        aboutButton.onClick.AddListener(OnAboutClicked);
        quitButton.onClick.AddListener(OnQuitClicked);

        if(GameProgressManager.IsLevelCompleted(1))
        {
            continueButton.gameObject.SetActive(true);
            startButton.gameObject.SetActive(false);
        }
        else
        {
            startButton.gameObject.SetActive(true);
            continueButton.gameObject.SetActive(false);
        }

        StartCoroutine(DisableAllButtonsTemporary(0.2f));
    }

    IEnumerator DisableAllButtonsTemporary(float delay)
    {
        startButton.interactable = false;
        continueButton.interactable = false;
        settingsButton.interactable = false;
        aboutButton.interactable = false;
        quitButton.interactable = false;

        yield return new WaitForSeconds(delay);

        startButton.interactable = true;
        continueButton.interactable = true;
        settingsButton.interactable = true;
        aboutButton.interactable = true;
        quitButton.interactable = true;
    }

    public void OnStartClicked()
    {
        UIManagerMainMenu.Instance.GoToChooseLevelMenu();
    }

    public void OnContinueClicked()
    {
        UIManagerMainMenu.Instance.GoToChooseLevelMenu();
    }

    public void OnSettingsClicked()
    {
        UIManagerMainMenu.Instance.GoToSettingsMenu();
    }

    public void OnAboutClicked()
    {
        UIManagerMainMenu.Instance.GoToAboutMenu();
    }

    public void OnQuitClicked()
    {
        Application.Quit();
    }
}
