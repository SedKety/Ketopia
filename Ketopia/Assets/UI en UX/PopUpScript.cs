using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpScript : MonoBehaviour
{
    public TextMeshProUGUI displayText;
    public float dissapearTimer;

    public void OnEnable()
    {
        OnInteract();
    }
    public void OnInteract()
    {
        StopCoroutine(DisableTimer());
        StartCoroutine(DisableTimer());
    }
    public IEnumerator DisableTimer()
    {
        yield return new WaitForSeconds(dissapearTimer);
        gameObject.SetActive(false);
        
    }
}
