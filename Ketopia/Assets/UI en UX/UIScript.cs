using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public static UIScript instance;
    public GameObject inventory, playerstats, settings, dialogue, winScreen, cursor, deathscreen;

    public Transform npcDropLocation;
    public TextMeshProUGUI dialogueText;
    public int currentDialogueNumber;
    public Dialogue currentDialogue;
    public Dialogue interacteDialogue;
    public GameObject lastInteractedNpc;
    public bool hasClicked;
    public bool setMenAan;
    public Button dialogueSwapper;
    public PopUpScript popup;
    public GameObject inGameMenu;
    public GameObject playerStats;
    public GameObject inventoryClose;
    public GameObject cursorClose;
    public PlayerMovement playerMovement;
    public PlayerCamMovement playerCamMovement;
    public InventoryManager inventoryManager;
    public PlayerManager playerManager;
    public void Start()
    {
        setMenAan = false;
        instance = this;
        currentDialogueNumber = -1;
        Invoke(nameof(AssignVariables), 1);
    }
    public void AssignVariables()
    {
        playerMovement = PlayerManager.instance.gameObject.GetComponent<PlayerMovement>();
        playerCamMovement = PlayerManager.instance.gameObject.GetComponent<PlayerCamMovement>();
    }
    public void DisplayDialogue(string text)
    {
        if (interacteDialogue = currentDialogue)
        {
            if (currentDialogue != null)
            {
                currentDialogueNumber++;
                if (currentDialogueNumber <= currentDialogue.dialogue.Count - 1)
                {
                    dialogueText.text = currentDialogue.dialogue[currentDialogueNumber];
                    for (int i = 0; i < currentDialogue.milestones.Count; i++)
                    {
                        if (currentDialogue.milestones[i].milestoneNumber == currentDialogueNumber)
                        {
                            var spawnedItem = Instantiate(currentDialogue.milestones[i].itemToSpawn, npcDropLocation.position, Quaternion.identity);
                            spawnedItem.GetComponent<PhysicalItemScript>().quantity = currentDialogue.milestones[i].itemQuantity;
                        }
                    }
                }
                else
                {
                    currentDialogue = null;
                    dialogueText.text = null;
                    currentDialogueNumber = -1;
                    PlayerManager.instance.SwitchState(PlayerState.normal);
                    PlayerManager.instance.canInteractWithNpc = true;
                }
            }
            else
            {
                if (hasClicked == false)
                {
                    dialogueText.text = text;
                    hasClicked = true;
                }
                else
                {
                    hasClicked = false;
                    dialogueText.text = null;
                    PlayerManager.instance.SwitchState(PlayerState.normal);
                    PlayerManager.instance.canInteractWithNpc = true;
                }
            }
        }
        else
        {
            PlayerManager.instance.SwitchState(PlayerState.normal);
            PlayerManager.instance.canInteractWithNpc = true;
        }
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("Hernia");
    }
    public void DisplayText(string text)
    {
        dialogueText.text = text;
        PlayerManager.instance.SwitchState(PlayerState.dialogue);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public IEnumerator DialogueActive()
    {
        yield return new WaitForSeconds(1);
    }
    private void Update()
    {
        if (Input.GetButtonDown("Cancel") & setMenAan == false & PlayerManager.instance.playerState != PlayerState.ship)
        {
            inGameMenu.SetActive(true);
            inventoryClose.SetActive(false);
            playerStats.SetActive(false);
            cursorClose.SetActive(false);
            playerMovement.canMove = false;
            playerCamMovement.canILook = false;
            GetComponent<InventoryManager>().enabled = false;
            setMenAan = true;
        }
    }
    public void WeerBewegen()
    {
        playerMovement.canMove = true;
        playerCamMovement.canILook = true;
        playerStats.SetActive(true);
        cursorClose.SetActive(true);
        GetComponent<InventoryManager>().enabled = true;
        setMenAan = false;
    }
}
