using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResolutionM : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;
    private Resolution[] resolutions;
    private List<Resolution> resolutionList;
    private float currentRefreshRate;
    private int currentResolutionIndex = 0;
    [System.Obsolete]
    void Start()
    {
        PlayerPrefs.GetInt("SavedRes", currentResolutionIndex);
        resolutions = Screen.resolutions;
        resolutionList = new List<Resolution>();
        dropdown.ClearOptions();
        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].refreshRate > currentRefreshRate)
            {
                resolutionList.Add(resolutions[i]);
            }
        }
        List<string> options = new List<string>();
        for (int i = 0;i < resolutionList.Count; i++) 
        { 
            string resolutionOption = resolutionList[i].width + "x" + resolutionList[i].height + " " + resolutionList[i].refreshRate + "Hz";
            options.Add(resolutionOption);
            if (resolutionList[i].width == Screen.width && resolutionList[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
            dropdown.AddOptions(options);
            dropdown.value = currentResolutionIndex;
            dropdown.RefreshShownValue();
        }
    }
    public void SetResolution(int resolutionIndex) 
    {
        Resolution resolution = resolutionList[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, true);
        PlayerPrefs.SetInt("SavedRes", currentResolutionIndex);
        PlayerPrefs.Save();
    }
}
