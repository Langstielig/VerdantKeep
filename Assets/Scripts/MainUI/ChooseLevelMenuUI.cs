using UnityEngine;
using UnityEngine.UI;

public class ChooseLevelMenuUI : MenuUI
{
    [SerializeField] private LevelButtonUI[] levelButtons;
    [SerializeField] private Button backButton;

    private int sceneCount = 4;

    private void Start()
    {
        InitializeLevelButtons();
        levelButtons[0].levelButton.onClick.AddListener(() => OnLevelClicked(4));
        levelButtons[1].levelButton.onClick.AddListener(() => OnLevelClicked(5));
        //levelButtons[2].levelButton.onClick.AddListener(() => OnLevelClicked(6));
        backButton.onClick.AddListener(OnBackClicked);
    }

    public void InitializeLevelButtons()
    {
        bool isPreviosLevelCompleted = true;
        for (int i = 0; i < levelButtons.Length; i++)
        {
            bool isCompleted = GameProgressManager.IsLevelCompleted(i + 1);
            if (isCompleted)
            {
                int coins = GameProgressManager.GetLevelCoins(i + 1);
                int time = GameProgressManager.GetLevelTime(i + 1);
                levelButtons[i].SetLevelData(isPreviosLevelCompleted, isCompleted, coins, time);

                isPreviosLevelCompleted = true;
            }
            else
            {
                levelButtons[i].SetLevelData(isPreviosLevelCompleted);

                isPreviosLevelCompleted = false;
            }
        }
    }

    public void OnLevelClicked(int levelNumber)
    {
        UIManagerMainMenu.Instance.GoToLevel(levelNumber);
    }

    public void OnBackClicked()
    {
        UIManagerMainMenu.Instance.GoToMainMenu();
    }
}
