using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResolutionM : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;
    private List<Resolution> resolutionList;
    private float currentRefRate;
    private int currentResolutionIndex = 0;
    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionList = new List<Resolution>();
        resolutionDropdown.ClearOptions();
        currentRefRate = Screen.currentResolution.refreshRate;
        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].refreshRate == currentRefRate)
            {
                resolutionList.Add(resolutions[i]);
            }
        }
        List<string> options = new List<string>();
        for (int i = 0; i < resolutionList.Count; i++)
        {
            string resolutionOption = resolutionList[i].width + "x" + resolutionList[i].height + " " + resolutionList[i].refreshRate + " Hz";
            options.Add(resolutionOption);
            if (resolutionList[i].width == Screen.width && resolutionList[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        LoadResolution();
    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutionList[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, true);
        PlayerPrefs.SetInt("ResolutionWidth", resolution.width);
        PlayerPrefs.SetInt("ResolutionHeight", resolution.height);
        PlayerPrefs.SetInt("RefreshRate", resolution.refreshRate);
        PlayerPrefs.SetInt("ResolutionIndex", resolutionIndex);
        PlayerPrefs.Save();
    }
    private void LoadResolution()
    {
        if (PlayerPrefs.HasKey("ResolutionWidth") && PlayerPrefs.HasKey("ResolutionHeight") && PlayerPrefs.HasKey("RefreshRate"))
        {
            int width = PlayerPrefs.GetInt("ResolutionWidth");
            int height = PlayerPrefs.GetInt("ResolutionHeight");
            int refreshRate = PlayerPrefs.GetInt("RefreshRate");
            for (int i = 0; i < resolutionList.Count; i++)
            {
                if (resolutionList[i].width == width && resolutionList[i].height == height && resolutionList[i].refreshRate == refreshRate)
                {
                    currentResolutionIndex = i;
                    Screen.SetResolution(width, height, true);
                    resolutionDropdown.value = currentResolutionIndex;
                    resolutionDropdown.RefreshShownValue();
                    break;
                }
            }
        }
    }
}
