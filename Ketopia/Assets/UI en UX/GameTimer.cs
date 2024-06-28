using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Assertions.Must;

public class GameTimer : MonoBehaviour
{
    public static GameTimer Instance;
    public TMP_Text currentTimeText;
    public TMP_Text bestTimeText;
    public TMP_Text customBestTimeText; // Optional: For use in DisplayBestTime

    private float currentTime = 0f;
    private int minutes = 0;
    private float seconds = 0f;
    private float bestTime = float.MaxValue;
    private bool isTimerRunning = true;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        // Load the best time from PlayerPrefs
        LoadBestTime();
        UpdateBestTimeText();
    }

    void Update()
    {
        if (isTimerRunning)
        {
            // Update the current time
            currentTime += Time.deltaTime;
            minutes = Mathf.FloorToInt(currentTime / 60F);
            seconds = currentTime % 60F;
            UpdateCurrentTimeText();
        }
    }

    void UpdateCurrentTimeText()
    {
        if (currentTimeText != null)
        {
            currentTimeText.text = $"Time: {minutes:00}:{seconds:00.00}";
        }
    }

    public void UpdateBestTimeText()
    {
        if (bestTime == float.MaxValue)
        {
            bestTimeText.text = "Best Time: N/A";
        }
        else
        {
            int bestMinutes = Mathf.FloorToInt(bestTime / 60F);
            float bestSeconds = bestTime % 60F;
            bestTimeText.text = $"Best Time: {bestMinutes:00}:{bestSeconds:00.00}";
        }
    }

    public void WinGame()
    {
        isTimerRunning = false;

        // Check if the current time is better than the stored best time
        if (currentTime < bestTime)
        {
            bestTime = currentTime;
            PlayerPrefs.SetFloat("BestTime", bestTime);
            PlayerPrefs.Save();
            UpdateBestTimeText(); // Update the UI to display the new best time
            Debug.Log("New best time!");
        }
        else
        {
            int bestMinutes = Mathf.FloorToInt(bestTime / 60F);
            float bestSeconds = bestTime % 60F;
            bestTimeText.text = $"Best Time: {bestMinutes:00}:{bestSeconds:00.00}";
        }
    }

    void SaveBestTime()
    {
        float totalTime = currentTime;
        Debug.Log($"Saving best time: {totalTime} seconds");

        if (totalTime < bestTime)
        {
            Debug.Log("New best time!");
            bestTime = totalTime;
            PlayerPrefs.SetFloat("BestTime", bestTime);
            PlayerPrefs.Save();
            UpdateBestTimeText(); // Update the UI to display the new best time
        }
        else
        {
            Debug.Log("Current time is not better than best time.");
        }
    }

    void LoadBestTime()
    {
        bestTime = PlayerPrefs.GetFloat("BestTime", float.MaxValue);
        Debug.Log($"Loaded best time: {bestTime}");
    }

    // Unified method to display the best time on a specific TMP_Text
    public void DisplayBestTimeOnText(TMP_Text targetText)
    {
        if (bestTime == float.MaxValue)
        {
            targetText.text = "Best Time: N/A";
        }
        else
        {
            int bestMinutes = Mathf.FloorToInt(bestTime / 60F);
            float bestSeconds = bestTime % 60F;
            targetText.text = $"Best Time: {bestMinutes:00}:{bestSeconds:00.00}";
        }
    }

    // Optional: Method to reset the best time
    public void ResetBestTime()
    {
        bestTime = float.MaxValue;
        PlayerPrefs.DeleteKey("BestTime");
        PlayerPrefs.Save();
        UpdateBestTimeText();
    }
}