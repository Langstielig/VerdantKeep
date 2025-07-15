using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreenUI : MenuUI
{
    [SerializeField] private int levelNumber;
    [SerializeField] private Button againButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private TMP_Text timeText;

    private void Start()
    {
        againButton.onClick.AddListener(OnAgainClicked);
        mainMenuButton.onClick.AddListener(OnMainMenuButton);
    }

    public void OnAgainClicked()
    {
        UIManagerMainMenu.Instance.GoToLevel(levelNumber);
    }

    public void OnMainMenuButton()
    {
        UIManagerMainMenu.Instance.GoToMainMenu();
    }

    public void SetResult(int coins)
    {
        coinsText.text = $"Собрано монет: {coins}";
        timeText.text = $"Время прохождения: {LevelTimer.Instance.GetFormattedTime()}";

        int time = (int)LevelTimer.Instance.GetTime();

        GameProgressManager.SetLevelCompleted(1, coins, time);
    }
}
