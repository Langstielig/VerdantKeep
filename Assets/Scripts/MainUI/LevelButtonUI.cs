using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtonUI : MonoBehaviour
{
    [SerializeField] public Button levelButton;
    [SerializeField] private GameObject emptyLevelImage;
    [SerializeField] private GameObject checkMark;
    [SerializeField] private GameObject coinsText;
    [SerializeField] private GameObject timeText;

    private string timeFormat; //?

    public void SetLevelData(bool isUnlocked, bool isCompleted = false, int coins = 0, int time = 0)
    {
        //переделать под значение переменных
        if(!isUnlocked)
        {
            levelButton.gameObject.SetActive(false);
            emptyLevelImage.SetActive(true);
            checkMark.SetActive(false);
            coinsText.gameObject.SetActive(false);
            timeText.gameObject.SetActive(false);
        }
        else
        {
            levelButton.gameObject.SetActive(true);
            emptyLevelImage.SetActive(false);
            checkMark.SetActive(false);

            if(isCompleted)
            {
                checkMark.SetActive(true);
                coinsText.gameObject.SetActive(true);
                timeText.gameObject.SetActive(true);

                coinsText.GetComponentInChildren<TMP_Text>().text = coins.ToString();

                int seconds = time % 60;
                int minutes = time / 60;
                timeText.GetComponentInChildren<TMP_Text>().text = $"{minutes:00}:{seconds:00}";
            }
        }    
    }
}
