using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamagable
{
    public static PlayerStats instance;
    public int level;
    public float exp;
    public float expNeededToLevelUp;
    public int health;
    public int food;

    public void Start()
    {
        instance = this;
    }
    public void IDamagable(int dmgDone, GameObject weaponUsed)
    {
        health -= dmgDone;
    }
    public void AddExp(float expToAdd)
    {
        exp += expToAdd;
        if(exp >= expNeededToLevelUp)
        {
            level++;
            expNeededToLevelUp *= 1.25f;
            exp = 0;
        }
    }
}
