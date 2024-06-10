using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameTimer : MonoBehaviour
{
    private bool timerOn;
    public float currentTime;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text bestTimerText;
    void Start()
    {
        currentTime = 0;
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
            currentTime++;
            if(timerText != null)
            timerText.text = "time " + currentTime;

            yield return new WaitForSeconds(1);
        }
    }

    public void CalculateBestTime()
    {
        if(PlayerPrefs.GetFloat("BestTime")  != 0)
        {
            PlayerPrefs.SetFloat("BestTime", currentTime);
        }
        else if(PlayerPrefs.GetFloat("BestTime") <= currentTime)
        {
            PlayerPrefs.SetFloat("BestTime", currentTime);
        }
    }
}
