using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlayerState
{
    ship,
    normal,
    inventory,
    menu,
}
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public PlayerState playerState;
    public GameObject player;

    public Transform playerCamLocation;

    public int playerHealth;
    public int playerFood;
    public void Start()
    {
        instance = this;
        playerState = PlayerState.normal;   
        player = gameObject;
    }
    public void SwitchState(PlayerState state)
    {
        playerState = state;
        if (playerState == PlayerState.normal)
        {
            player.GetComponent<PlayerMovement>().canMove = true;
            player.GetComponent<PlayerCamMovement>().canILook = true;
            Camera.main.transform.position = playerCamLocation.position;  
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            player.GetComponent<Collider>().enabled = true;
            player.GetComponent <Rigidbody>().useGravity = true;
            InventoryManager.instance.hotbar.SetActive(true);
            InventoryManager.instance.inventory.SetActive(false);

        }
        else if (playerState == PlayerState.ship)
        {
            player.GetComponent<PlayerMovement>().canMove = false;
            player.GetComponent<PlayerCamMovement>().canILook = false;
            Camera.main.transform.position = AirshipManager.instance.camHolderAirship.position;
            Camera.main.transform.rotation = AirshipManager.instance.camHolderAirship.rotation;
            player.GetComponent<Collider>().enabled = false;
            player.GetComponent<Rigidbody>().useGravity = false;
            player.GetComponent<Rigidbody>().velocity = Vector3.zero;
            InventoryManager.instance.hotbar.SetActive(false);
            InventoryManager.instance.inventory.SetActive(false);
        }
        else if(playerState == PlayerState.inventory)
        {
            AirshipMovement.instance.DisableMovement();
            player.GetComponent<PlayerCamMovement>().canILook = false;
        }
    }
}
