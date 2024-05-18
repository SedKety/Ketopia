using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour, IDamagable
{
    public static PlayerStats instance;
    public int level;
    public float exp;
    public float expNeededToLevelUp;


    public float health;
    public float maxHealth;
    public Sprite fullHeart, halfHeart, brokenHeart;
    public Image[] hearts;
    public int food;

    public int age;
    public void Start()
    {
        instance = this;
    }
    public void Update()
    {
        foreach (Image img in hearts)
        {
            img.sprite = brokenHeart;
        }
        for (int i = 0; i < Mathf.FloorToInt(health); i++)
        {
            hearts[i].sprite = fullHeart;
        }
        if (health % 1 != 0)
        {
            hearts[Mathf.FloorToInt(health)].sprite = halfHeart;
        }
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
