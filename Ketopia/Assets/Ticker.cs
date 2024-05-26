using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
public class Ticker : MonoBehaviour
{
    public float tickTime;

    public float tickerTimer;

    public delegate void TickAction();
    public static event TickAction OnTickAction;

    void Update()
    {
        tickerTimer += Time.deltaTime;
        if(tickerTimer >= tickTime)
        {
            tickerTimer = 0;
            TickEvent();
        }
    }

    void TickEvent()
    {
        OnTickAction?.Invoke();
    }
}
