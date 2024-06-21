using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FpsMan : MonoBehaviour
{
    private const string FrameRateKey = "TargetFrameRate";
    private void Start()
    {
        int savedFPS = PlayerPrefs.GetInt("TargetFPS", -1);
        Application.targetFrameRate = savedFPS;
    }
    public void ChangeFPS(string target)
    {
        int fps = int.Parse(target);
        if (fps == 0)
        {
            Application.targetFrameRate = -1;
        }
        else
        {
            Application.targetFrameRate = fps;
        }
        PlayerPrefs.SetInt("TargetFPS", Application.targetFrameRate);
        PlayerPrefs.Save();
    }
}
