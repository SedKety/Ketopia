using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnaceScript : MonoBehaviour
{
    public GameObject currentTouchingItem;
    public Item item;

    public float smeltTimer;

    public float fuel;
    public float fuelDecreaseAmount;
    public float fuelSpeed;

    public Transform outputTransform;

    public ParticleSystem fireParticles;

    public void Start()
    {
        Ticker.OnTickAction += OnTick;
        fireParticles.gameObject.SetActive(false);
    }
    public void StartSmelting()
    {
        if (currentTouchingItem != null)
        {
            StartCoroutine(SmeltItem());
        }
    }

    public void OnTick()
    {
        if (currentTouchingItem)
        {
            StartCoroutine(SmeltItem());
        }
    }

    public void StartFuel()
    {
        StopCoroutine(ReduceFuel());
        StartCoroutine(ReduceFuel());
    }
    public IEnumerator ReduceFuel()
    {
        while (fuel > 0)
        {
            fireParticles.gameObject.SetActive(true);
            fuel -= fuelDecreaseAmount;
            yield return new WaitForSeconds(fuelSpeed);
        }
        if(fuel < 0)
        {
            fuel = 0;
        }
        if(fuel == 0)
        {
            fireParticles.gameObject.SetActive(false);
        }
    }

    public IEnumerator SmeltItem()
    {
        print("iniatiate smelting");
        item = currentTouchingItem.GetComponent<PhysicalItemScript>().item;
        var ore = item as Ores;
        var quantity = currentTouchingItem.GetComponent<PhysicalItemScript>().quantity;
        if (fuel > 0 & ore)
        {
            Destroy(currentTouchingItem);
            yield return new WaitForSeconds(smeltTimer);
            var bar = Instantiate(ore.bar, outputTransform.position, outputTransform.rotation);
            print(quantity);
            bar.GetComponent<PhysicalItemScript>().quantity = quantity;
        }
    }
}
