using System;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    public static LevelTimer Instance;

    private float totalTime;
    private float secondTimer;
    private bool isRunning;

    public event Action<int, int> OnTimeUpdated;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        StartTimer();
    }

    private void FixedUpdate()
    {
        if(!isRunning)
        {
            return;
        }

        totalTime += Time.deltaTime;
        secondTimer += Time.deltaTime;

        if(secondTimer >= 1f)
        {
            secondTimer = 0f;

            int minutes = Mathf.FloorToInt(totalTime / 60f);
            int seconds = Mathf.FloorToInt(totalTime % 60f);

            OnTimeUpdated?.Invoke(minutes, seconds);
        }
    }

    public void StartTimer()
    {
        totalTime = 0f;
        secondTimer = 0f;
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public float GetTime() => totalTime;

    public string GetFormattedTime()
    {
        int minutes = Mathf.FloorToInt(totalTime / 60f);
        int seconds = Mathf.FloorToInt(totalTime % 60f);

        return $"{minutes:00}:{seconds:00}";
    }
}
