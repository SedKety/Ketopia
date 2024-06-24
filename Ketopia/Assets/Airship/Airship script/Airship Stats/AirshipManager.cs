using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum AirshipState
{
    enabled,
    disabled
}
public class AirshipManager : MonoBehaviour
{
    public static AirshipManager instance;
    public IslandGenerator islandGenerator;
    public AirshipState airshipState;

    public Transform turnOffColliderObject;
    public bool collidersOn;
    public float currentFuel;
    public float maxFuel;
    public float fuelCost;
    public float fuelDecreaseTimer;

    public Transform camHolderAirship;

    public List<GameObject> npcs;
    public List<GameObject> crafters;
    public void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void FixedUpdate()
    {
        if (currentFuel >= maxFuel)
        {
            currentFuel = maxFuel;
        }
        if (currentFuel <= 0)
        {
            currentFuel = 0;
        }
    }
    public void SwitchState(AirshipState state)
    {
        airshipState = state;
        switch (state)
        {
            case AirshipState.enabled:
                StartCoroutine(FuelConsumption());
                break;
            case AirshipState.disabled:
                StopCoroutine(FuelConsumption());
                break;
        }
    }
    public IEnumerator FuelConsumption()
    {
        while (AirshipMovement.instance.airshipMovementEnabled)
        {
            print("Womp Womp");
            if (currentFuel > 0)
            {
                currentFuel -= fuelCost;
            }
            else
            {
                AirshipMovement.instance.DisableMovement();
                airshipState = AirshipState.disabled;
            }
            yield return new WaitForSeconds(fuelDecreaseTimer);
        }
    }

    public void TurnOffColliders()
    {
        if (collidersOn)
        {
            for (int i = 0; i < turnOffColliderObject.childCount; i++)
            {
                if (turnOffColliderObject.GetChild(i).GetComponent<Collider>())
                {
                    turnOffColliderObject.GetChild(i).GetComponent<Collider>().enabled = false;
                    collidersOn = false;
                }
            }
        }
        else
        {
            for (int i = 0; i < turnOffColliderObject.childCount; i++)
            {
                if (turnOffColliderObject.GetChild(i).GetComponent<Collider>())
                {
                    turnOffColliderObject.GetChild(i).GetComponent<Collider>().enabled = true;
                    collidersOn = true;
                }
            }
        }
    }
}
