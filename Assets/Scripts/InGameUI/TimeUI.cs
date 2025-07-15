using TMPro;
using UnityEngine;

public class TimeUI : MonoBehaviour
{
    [SerializeField] private TMP_Text timeText;

    private void Start()
    {
        Debug.Log($"Update time text: {LevelTimer.Instance.GetFormattedTime()}");
        if (LevelTimer.Instance != null)
        {
            LevelTimer.Instance.OnTimeUpdated += UpdateText;
        }
    }

    private void OnDisable()
    {
        if(LevelTimer.Instance != null)
        {
            LevelTimer.Instance.OnTimeUpdated -= UpdateText;
        }
    }

    private void UpdateText(int minutes, int seconds)
    {
        timeText.text = $"{minutes:00}:{seconds:00}";
    }
}
