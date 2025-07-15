using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VictoryScreenUI : MenuUI
{
    [SerializeField] private Button continueButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private TMP_Text timeText;

    private void Start()
    {
        continueButton.onClick.AddListener(OnContinueClicked);
        mainMenuButton.onClick.AddListener(OnMainMenuButton);
    }

    public void OnContinueClicked()
    {
        UIManagerMainMenu.Instance.GoToChooseLevelMenu();
    }

    public void OnMainMenuButton()
    {
        UIManagerMainMenu.Instance.GoToMainMenu();
    }

    public void SetResult(int levelNumber, int coins)
    {
        coinsText.text = $"Собрано монет: {coins}";
        timeText.text = $"Время прохождения: {LevelTimer.Instance.GetFormattedTime()}";

        int time = (int) LevelTimer.Instance.GetTime();

        GameProgressManager.SetLevelCompleted(levelNumber, coins, time);
    }
}
