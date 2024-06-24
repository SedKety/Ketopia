using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Consumable")]
public class Consumable : Item
{
    public float healthValue;
    public float foodValue;

    public AudioClip clip;
    public override void OnItemUse()
    {
        PlayerStats.instance.food += foodValue;
        PlayerStats.instance.health += healthValue;

        if(clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, PlayerManager.instance.gameObject.transform.position);
        }
    }
}
