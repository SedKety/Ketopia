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
    public AudioClip npcTalk;
    public void Start()
    {
        if (animator)
        {
            animator.SetBool("ShouldIdle", true);
        }
    }
    public virtual void IInteractable()
    {
        PlayerManager.instance.canInteractWithNpc = false;
        if (animator)
        {
            animator.SetBool("Wavey", true);
            AudioSource.PlayClipAtPoint(npcTalk, gameObject.transform.position);
        }
        if (UIScript.instance.currentDialogue == null) 
        {
            if (dialogueCounter <= dialogue.Count - 1)
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
                UIScript.instance.DisplayText(interactionString);
            }
        }
    }
}
