using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CondocteurScript : NpcScript, IInteractable
{
    public override void IInteractable()
    {
        PlayerManager.instance.canInteractWithNpc = false;
        animator.SetBool("Wavey", true);
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
                UIScript.instance.DisplayText
                    ("Hello Captain!, your ship has: " + AirshipManager.instance.currentFuel + " fuel left!" + "                                " + "You are currently in an: " + ChunkSpawner.currentChunk.name + "Chunk");
            }
        }
    }
}
