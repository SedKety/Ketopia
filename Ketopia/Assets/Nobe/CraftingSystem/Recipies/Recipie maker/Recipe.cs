using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Recipe")]
public class Recipe : ScriptableObject
{
    [Header("Input")]
    public Item inputItem1;
    public Item inputItem2;
    [Header("Output")]
    public GameObject outputItem;
}
