using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlayerState
{
    ship,
    normal,
    inventory,
    menu,
    dialogue,
}
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public GameObject inGameMenu;
    public PlayerState playerState;
    public GameObject player;
    public Transform dropSpot;
    public Transform playerCamLocation;
    public PlayerCamMovement playerCamMovement;
    public Transform playerWheelLocation;
    public GameObject cursor;

    public bool canInteractWithNpc;

    public Transform dialogueLocation;

    public float playerToBoatDistance;
    public float distanceDamage;
    public void Start()
    {
        playerWheelLocation = GameObject.FindGameObjectWithTag("PlayerSteeringWheelPlacement").transform;
        instance = this;
        playerState = PlayerState.normal;
        player = gameObject;

        Ticker.OnTickAction += OnTick;
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
            transform.rotation = playerWheelLocation.rotation;
        }
    }

    public void OnTick()
    {
        if(Vector3.Distance(transform.position, AirshipManager.instance.transform.position) >= playerToBoatDistance)
        {
            PlayerStats.instance.IDamagable(distanceDamage, NodeType.everything, 0);
        }
    }
    public void SwitchState(PlayerState newState)
    {
        playerState = newState;

        switch (newState)
        {
            case PlayerState.ship:
                transform.rotation = playerWheelLocation.rotation;
                player.GetComponent<PlayerMovement>().canMove = false;
                player.GetComponent<PlayerCamMovement>().canILook = false;
                player.GetComponent<Collider>().enabled = false;
                player.GetComponent<Rigidbody>().useGravity = false;
                player.GetComponent<Rigidbody>().velocity = Vector3.zero;
                UIScript.instance.inventory.SetActive(false);
                UIScript.instance.dialogue.SetActive(false);
                AirshipMovement.instance.EnableMovement();
                Camera.main.transform.position = AirshipManager.instance.camHolderAirship.position;
                Camera.main.transform.rotation = AirshipManager.instance.camHolderAirship.rotation;
                playerCamMovement.shouldFollow = true;
                Camera.main.transform.parent = null;
                cursor.SetActive(false);
                break;

            case PlayerState.normal:
                player.GetComponent<PlayerMovement>().canMove = true;
                player.GetComponent<PlayerCamMovement>().canILook = true;
                player.GetComponent<Rigidbody>().velocity = Vector3.zero;
                player.GetComponent<Collider>().enabled = true;
                player.GetComponent<Rigidbody>().useGravity = true;
                UIScript.instance.dialogue.SetActive(false);
                UIScript.instance.inventory.SetActive(false);
                playerCamMovement.shouldFollow = false;
                Camera.main.transform.parent = playerCamLocation;
                Camera.main.transform.position = playerCamLocation.position;
                cursor.SetActive(true);
                Cursor.lockState = CursorLockMode.Locked;
                break;

            case PlayerState.inventory:
                AirshipMovement.instance.DisableMovement();
                player.GetComponent<PlayerCamMovement>().canILook = false;
                break;
            case PlayerState.dialogue:
                player.GetComponent<PlayerMovement>().canMove = false;
                player.GetComponent<PlayerCamMovement>().canILook = false;
                player.GetComponent<Rigidbody>().velocity = Vector3.zero;
                player.transform.position = dialogueLocation.position;
                player.transform.rotation = dialogueLocation.rotation;
                player.GetComponentInChildren<Camera>().gameObject.transform.rotation = dialogueLocation.transform.rotation;
                UIScript.instance.dialogue.SetActive(true);
                UIScript.instance.inventory.SetActive(false);
                break;

        }
    }
}
