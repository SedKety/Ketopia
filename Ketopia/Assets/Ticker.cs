using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Ticker : MonoBehaviour
{
    public static float tickTime = 0.1f;

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
