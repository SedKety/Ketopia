using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcScript : MonoBehaviour, IInteractable
{
    public Animator animator;
    public List<Dialogue> dialogue;
    public Transform dropLocation;
    public Transform playerLocation;
    public int dialogueCounter;
    public string interactionString;
    public virtual void IInteractable()
    {
        PlayerManager.instance.canInteractWithNpc = false;
        if (UIScript.instance.currentDialogue == null) 
        {
            if (dialogue.Contains(dialogue[dialogueCounter]))
            {
                UIScript.instance.currentDialogue = dialogue[dialogueCounter];
                UIScript.instance.DisplayDialogue(null);
                UIScript.instance.npcDropLocation = dropLocation;
                PlayerManager.instance.dialogueLocation = playerLocation;
                PlayerManager.instance.SwitchState(PlayerState.dialogue);
                dialogueCounter += 1;
            }
            else
            {
                PlayerManager.instance.dialogueLocation = playerLocation;
                PlayerManager.instance.SwitchState(PlayerState.dialogue);
                UIScript.instance.DisplayText("I have nothing to say right now");
            }
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerManager>() != null)
        {
            print(other.gameObject.GetComponent<PlayerManager>().gameObject.name);
            animator.SetBool("ShouldIdle", true);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerManager>() != null)
        {
            animator.SetBool("ShouldIdle", false);
        }
    }
}
