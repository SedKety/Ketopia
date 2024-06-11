using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameTimer : MonoBehaviour
{
    private bool timerOn;
    public int currentTimeSeconds;
    public int currentTimeMin;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text bestTimerText;
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
    public IEnumerator Timer()
    {
        while(timerOn == true)
        {
            currentTimeSeconds++;
            if (timerText != null)
            {
                timerText.text = "time " + currentTimeMin + ":" +currentTimeSeconds;
            }
            if (currentTimeSeconds >= 60)
            {
                currentTimeMin++; 
                currentTimeSeconds = 0;
            }
            yield return new WaitForSeconds(1);
        }
    }
    public void CalculateBestTime()
    {
        if(PlayerPrefs.GetFloat("BestTime")  != 0)
        {
            PlayerPrefs.SetFloat("BestTime", currentTimeMin * 60 + currentTimeSeconds);
        }
        else if(PlayerPrefs.GetFloat("BestTime") <= currentTimeMin * 60 + currentTimeSeconds)
        {
            PlayerPrefs.SetFloat("BestTime", currentTimeMin * 60 + currentTimeSeconds);
        }
    }
}
