using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue")]
public class Dialogue : ScriptableObject
{
    public string title;
    public List<string> dialogue;
    public List<dialogueMilestone> milestones;

    [System.Serializable]
    public struct dialogueMilestone
    {
        public int milestoneNumber;
        public GameObject itemToSpawn;
        public int itemQuantity;
    }
}
