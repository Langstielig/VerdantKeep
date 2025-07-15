using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManagerMainMenu : MonoBehaviour, IUIManager
{
    public static UIManagerMainMenu Instance;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void OpenMenu(MenuUI menu)
    {
        menu.Open();
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToAboutMenu()
    {
        SceneManager.LoadScene("AboutMenu");
    }

    public void GoToSettingsMenu()
    {
        SceneManager.LoadScene("SettingsMenu");
    }

    public void GoToChooseLevelMenu()
    {
        SceneManager.LoadScene("ChooseLevelMenu");
    }

    public void GoToLevel(int levelNumber)
    {
        SceneManager.LoadScene(levelNumber);
    }

    public void CloseAllMenus()
    {
        Debug.Log("Close all menus");
    }
}
