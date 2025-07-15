using UnityEngine;
using UnityEngine.UI;

public class AboutMenuUI : MenuUI
{
    [SerializeField] private Button backButton;

    private void Start()
    {
        backButton.onClick.AddListener(OnBackClicked);
    }

    public void OnBackClicked()
    {
        UIManagerMainMenu.Instance.GoToMainMenu();
    }
}
