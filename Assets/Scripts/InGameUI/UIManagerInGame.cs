using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManagerInGame : MonoBehaviour
{
    public static UIManagerInGame Instance;

    [SerializeField] private PauseScreenUI pauseScreen;
    [SerializeField] private VictoryScreenUI victoryScreen;
    [SerializeField] private DeathScreenUI deathScreen;

    [SerializeField] private Button pauseButton;

    [SerializeField] private List<MonoBehaviour> hudParts;

    private void Awake()
    {
        Instance = this;

        //DontDestroyOnLoad(gameObject);

        CloseAllMenus();
        SetHUDVisible(true);
    }

    private void Start()
    {
        pauseButton.onClick.AddListener(ShowPauseScreen);
    }

    public void SetHUDVisible(bool visible)
    {
        foreach(var part in hudParts)
        {
            if (part is IHUDPart hudPart)
            {
                hudPart.SetVisible(visible);
            }
        }

        pauseButton.gameObject.SetActive(visible);
    }

    public void UpdateHealth(int current)
    {
        foreach (var part in hudParts)
        {
            if(part is HealthUI healthUI)
            {
                healthUI.UpdateHealth(current);
            }
        }
    }

    public void AddCoin()
    {
        foreach (var part in hudParts)
        {
            if (part is CoinsUI coinsUI)
            {
                coinsUI.AddCoin();
            }
        }
    }

    public void ShowPauseScreen()
    {
        SetHUDVisible(false);
        pauseScreen.Open();
    }

    public void ShowVictoryScreen()
    {
        SetHUDVisible(false);

        int coins = GetCoinsCount();

        int levelNumber = SceneManager.GetActiveScene().buildIndex - 3;

        victoryScreen.SetResult(levelNumber, coins);
        victoryScreen.Open();
    }

    public void ShowDeathScreen()
    {
        SetHUDVisible(false);

        int coins = GetCoinsCount();

        deathScreen.SetResult(coins);
        deathScreen.Open();
    }

    public void CloseAllMenus()
    {
        pauseScreen.Close();
        victoryScreen.Close();
        deathScreen.Close();
    }

    public void ResumeGame()
    {
        CloseAllMenus();
        SetHUDVisible(true);
    }

    private int GetCoinsCount()
    {
        foreach (var part in hudParts)
        {
            if (part is CoinsUI coinsUI)
            {
                return coinsUI.GetCoinsCount();
            }
        }

        return 0;
    }
}
