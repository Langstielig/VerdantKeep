using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuUI : MenuUI
{
    [SerializeField] private Button backButton;
    [SerializeField] private Button eraseProgressButton;

    private void Start()
    {
        backButton.onClick.RemoveAllListeners();
        eraseProgressButton.onClick.RemoveAllListeners();
        backButton.onClick.AddListener(OnBackClicked);
        eraseProgressButton.onClick.AddListener(OnEraseProgressClicked);
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
}
