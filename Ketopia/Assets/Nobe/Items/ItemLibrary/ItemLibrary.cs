using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemLibrary")]
public class ItemLibrary :  ScriptableObject
{
   
    public List<item> materials;
    public List<item> consumables;
    public List<item> fuel;
    [System.Serializable]
    public struct item
    {
        public string name;
        public int id;
        public Color color;
        public Item scriptableItem;
        public GameObject physicalItem;
    }
}
