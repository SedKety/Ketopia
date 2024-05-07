using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemLibrary")]
public class ItemLibrary :  ScriptableObject
{
   
    public List<item> items;
    [System.Serializable]
    public struct item
    {
        public string name;
        public Item scriptableItem;
        public GameObject physicalItem;
    }
}
