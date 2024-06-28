using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class PositionTranker : MonoBehaviour
{
    public Transform player;
    public TextMeshProUGUI textPos;
    void Update()
    {
        if(player != null)
        {
            textPos.text = player.position.ToString("0");
        }
    }
}
