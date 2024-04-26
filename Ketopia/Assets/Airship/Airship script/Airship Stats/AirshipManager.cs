using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirshipManager : MonoBehaviour
{
    public static AirshipManager instance;

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
