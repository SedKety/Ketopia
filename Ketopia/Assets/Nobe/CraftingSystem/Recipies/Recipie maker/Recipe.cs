using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Recipe")]
public class Recipe : ScriptableObject
{
    [Header("Input")]
    public Item inputItem1;
    public int inputItem1Amount;
    public Item inputItem2;
    public int inputItem2Amount;
    [Header("Output")]
    public GameObject outputItem;
    public int outputItemAmount;
}
