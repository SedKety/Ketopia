using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour, IDamagable
{
    public static PlayerStats instance;

    [Header("Levels/Exp")]
    public int level;
    public float exp;
    public float expNeededToLevelUp;

    [Header("Health/Hearts")]
    public float health;
    public float maxHealth;
    public Sprite fullHeart, halfHeart, brokenHeart;
    public Image[] hearts;

    [Header("Food")]
    public int food;
    public int maxFood;
    public void Start()
    {
        instance = this;
    }
    public void FixedUpdate()
    {
        if(health > maxHealth)
        {
            health = maxHealth;
        }
        if(food > maxFood)
        {
            food = maxFood;
        }
        foreach (Image img in hearts)
        {
            if(img != null && brokenHeart != null)
            {
                img.sprite = brokenHeart;
            }
        }
        for (int i = 0; i < Mathf.FloorToInt(health); i++)
        {
            if (hearts[i] != null & fullHeart != null)
            {
                hearts[i].sprite = fullHeart;
            }
        }
        if (health % 1 != 0)
        {
            hearts[Mathf.FloorToInt(health)].sprite = halfHeart;
        }
    }
    public void IDamagable(int dmgDone, NodeType type)
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
