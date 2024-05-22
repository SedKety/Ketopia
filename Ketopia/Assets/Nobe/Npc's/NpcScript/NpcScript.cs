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
    public void IInteractable()
    {
        PlayerManager.instance.canInteractWithNpc = false;
        if (UIScript.instance.currentDialogue == null) 
        {
            if (dialogue.Count > 0)
            {
                print("gushit");
                UIScript.instance.currentDialogue = dialogue[0];
                dialogue.Remove(dialogue[0]);
                UIScript.instance.DisplayDialogue(null);
                UIScript.instance.npcDropLocation = dropLocation;
                PlayerManager.instance.dialogueLocation = playerLocation;
                PlayerManager.instance.SwitchState(PlayerState.dialogue);
            }
            else
            {
                PlayerManager.instance.dialogueLocation = playerLocation;
                PlayerManager.instance.SwitchState(PlayerState.dialogue);
                UIScript.instance.DisplayText("I have nothing to say right now");
            }
        }
        else
        {
            PlayerManager.instance.dialogueLocation = playerLocation;
            PlayerManager.instance.SwitchState(PlayerState.dialogue);
            if (UIScript.instance.lastInteractedNpc)
            {
                UIScript.instance.DisplayText("You already have an quest going on, return to:" + UIScript.instance.lastInteractedNpc.gameObject.name);
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
