using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Assertions.Must;

public class GameTimer : MonoBehaviour
{
    private bool timerOn;
    public int currentTimeSeconds;
    public int currentTimeMin;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text bestTimerText;
    public GameObject playerMc;

    void Start()
    {
        currentTimeSeconds = 0;
        currentTimeMin = 0;
        StartTimer();
    }

    public void StartTimer()
    {
        timerOn = true;
        StartCoroutine(Timer());
    }

    public void StopTimer()
    {
        timerOn = false;
    }

    private IEnumerator Timer()
    {
        while (timerOn)
        {
            yield return new WaitForSeconds(1);
            currentTimeSeconds++;
            if (currentTimeSeconds >= 60)
            {
                currentTimeMin++;
                currentTimeSeconds = 0;
            }

            if (timerText != null)
            {
                timerText.text = "Time: " + currentTimeMin.ToString("00") + ":" + currentTimeSeconds.ToString("00");
            }
        }
    }

    void Update()
    {
        if (PlayerManager.instance.playerState == PlayerState.menu)
        {
            StopTimer();
            CalculateBestTime();
        }
    }

    private void CalculateBestTime()
    {
        int currentTimeInSeconds = currentTimeMin * 60 + currentTimeSeconds;
        int bestTime = PlayerPrefs.GetInt("BestTime", int.MaxValue);
        if (currentTimeInSeconds < bestTime)
        {
            PlayerPrefs.SetInt("BestTime", currentTimeInSeconds);
        }
        bestTimerText.text = "Best Time: " + PlayerPrefs.GetInt("BestTime") + " Seconds";
    }
}