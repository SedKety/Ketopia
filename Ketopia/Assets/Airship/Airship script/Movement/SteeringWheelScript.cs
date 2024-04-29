using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringWheelScript : MonoBehaviour, IInteractable
{
    public void IInteractable()
    {
        if (PlayerManager.instance.playerState != PlayerState.ship)
        {
            PlayerManager.instance.SwitchState(PlayerState.ship);
        }
    }
}
