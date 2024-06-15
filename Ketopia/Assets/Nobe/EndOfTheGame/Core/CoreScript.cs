using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreScript : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        print(collision.gameObject.name);
        if (collision.gameObject.GetComponent<GiantMagnet>() == true)
        {
            UIScript.instance.inventory.gameObject.SetActive(false);
            UIScript.instance.playerstats.gameObject.SetActive(false);
            UIScript.instance.settings.gameObject.SetActive(false);
            UIScript.instance.dialogue.gameObject.SetActive(false);
            UIScript.instance.cursor.gameObject.SetActive(false);
            UIScript.instance.winScreen.gameObject.SetActive(true);
            PlayerManager.instance.SwitchState(PlayerState.menu);
        }
    }
}
