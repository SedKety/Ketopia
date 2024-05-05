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
    public Transform dropSpot;
    public Transform playerCamLocation;
    public Transform playerWheelLocation;

    public int playerHealth;
    public int playerFood;
    public void Start()
    {
        playerWheelLocation = GameObject.FindGameObjectWithTag("PlayerSteeringWheelPlacement").transform;
        instance = this;
        playerState = PlayerState.normal;
        player = gameObject;
    }
    public void Update()
    {
        if (playerState == PlayerState.ship & Input.GetKeyDown(KeyCode.G))
        {
            SwitchState(PlayerState.normal);
            AirshipMovement.instance.DisableMovement();
        }
        if (playerState == PlayerState.ship)
        {
            transform.position = playerWheelLocation.position;
            Camera.main.transform.position = AirshipManager.instance.camHolderAirship.position;
            Camera.main.transform.rotation = AirshipManager.instance.camHolderAirship.rotation;
        }
    }
    public void SwitchState(PlayerState state)
    {
        playerState = state;

        switch (playerState)
        {
            case PlayerState.ship:
                player.GetComponent<PlayerMovement>().canMove = false;
                player.GetComponent<PlayerCamMovement>().canILook = false;
                player.GetComponent<Collider>().enabled = false;
                player.GetComponent<Rigidbody>().useGravity = false;
                player.GetComponent<Rigidbody>().velocity = Vector3.zero;
                InventoryManager.instance.inventory.SetActive(false);
                AirshipMovement.instance.EnableMovement();
                break;


            case PlayerState.normal:
                player.GetComponent<PlayerMovement>().canMove = true;
                player.GetComponent<PlayerCamMovement>().canILook = true;
                Camera.main.transform.position = playerCamLocation.position;
                player.GetComponent<Rigidbody>().velocity = Vector3.zero;
                player.GetComponent<Collider>().enabled = true;
                player.GetComponent<Rigidbody>().useGravity = true;
                InventoryManager.instance.inventory.SetActive(false);
                break;



            case PlayerState.inventory:
                AirshipMovement.instance.DisableMovement();
                player.GetComponent<PlayerCamMovement>().canILook = false;
                break;
        }
    }
}
