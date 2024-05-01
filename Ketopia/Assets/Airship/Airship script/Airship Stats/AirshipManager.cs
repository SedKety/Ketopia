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
    public AirshipState airshipState;
    public float currentFuel;
    public float maxFuel;

    public float fuelCost;
    public float fuelDecreaseTimer;

    public Transform camHolderAirship;
    public void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        GameObject.FindObjectOfType<IslandGenerator>().StartSpawning();
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
            if (currentFuel >= 0)
            {
                currentFuel -= fuelCost;
            }
            yield return new WaitForSeconds(fuelDecreaseTimer);
        }
    }
}
