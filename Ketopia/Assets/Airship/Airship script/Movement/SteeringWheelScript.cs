using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringWheelScript : MonoBehaviour, IInteractable
{
    PlayerManager playerManager;

    public void Start()
    {
        playerManager = FindAnyObjectByType<PlayerManager>();
    }
    public void IInteractable()
    {
        if (playerManager.playerState != PlayerState.ship)
        {
            playerManager.SwitchState(PlayerState.ship);
            AirshipMovement.instance.EnableMovement();
        }
    }

    public void Update()
    {
        if (playerManager.playerState == PlayerState.ship & Input.GetKeyDown(KeyCode.E))
        {
            playerManager.SwitchState(PlayerState.normal);
            AirshipMovement.instance.DisableMovement();
        }
    }
}
