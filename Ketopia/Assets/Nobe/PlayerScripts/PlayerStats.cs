using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamagable
{
    public int health;
    public int food;

    public void IDamagable(int dmgDone, GameObject weaponUsed)
    {
        health -= dmgDone;
    }
}
