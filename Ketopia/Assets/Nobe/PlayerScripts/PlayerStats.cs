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
    public float expNeededToLevelUpMultiplier;

    [Header("Health/Hearts")]
    public float health;
    public float maxHealth;
    public Sprite fullHeart, halfHeart, brokenHeart;
    public Image[] hearts;

    [Header("Healing")]
    public float recoveryCooldown;
    public float recoveryAmount;

    [Header("Hunger")]
    public float hungerCooldown;
    public float hungerAmount;

    [Header("Food")]
    public float food;
    public float maxFood;
    public void Start()
    {
        instance = this;
        StartCoroutine(HealthRecovery());
        StartCoroutine(Hunger());
        if(expNeededToLevelUpMultiplier <= 0)
        {
            expNeededToLevelUpMultiplier = 1.25f;
        }
    }
    public void FixedUpdate()
    {
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        if (food > maxFood)
        {
            food = maxFood;
        }
        foreach (Image img in hearts)
        {
            if (img != null && brokenHeart != null)
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
    public void IDamagable(float dmgDone, NodeType type, int toolStrength)
    {
        health -= dmgDone;
    }
    public void AddExp(float expToAdd)
    {
        var leftOverExp = exp - expToAdd;
        exp += expToAdd;
        if (exp >= expNeededToLevelUp)
        {
            level++;
            expNeededToLevelUp *= expNeededToLevelUpMultiplier;
            exp = 0;
            if (leftOverExp >= 0)
            {
                AddExp(leftOverExp);
            }
        }
    }


    public IEnumerator HealthRecovery()
    {
        yield return new WaitForSeconds(recoveryCooldown);
        health += recoveryAmount;
    }
    public IEnumerator Hunger()
    {
        yield return new WaitForSeconds(hungerCooldown);
        health -= hungerAmount;
    }
}
