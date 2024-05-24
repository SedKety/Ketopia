using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    public SpikeScript spike;
    public Vector3 originalSpikePosition;
    public Vector3 newSpikePosition;

    public bool canBeSteppedOn;

    public void Start()
    {
        canBeSteppedOn = true;
        originalSpikePosition = spike.gameObject.transform.position;
        newSpikePosition = new Vector3(originalSpikePosition.x, originalSpikePosition.y + 0.3f, originalSpikePosition.z);
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player") & canBeSteppedOn)
        {
            StartCoroutine(OnTrap());
        }
    }
    public IEnumerator MakeTheTrapGoDown()
    {
        StopCoroutine(OnTrap());
        float upTime = 5;
        float upSpeed = 0;
        while (upTime < 1f)
        {
            upTime += Time.deltaTime * upSpeed;
            spike.gameObject.transform.position = Vector3.Lerp(spike.gameObject.transform.position, originalSpikePosition, upTime);
        }
        spike.canDealDmg = true;
        yield return null;
    }
    public IEnumerator OnTrap()
    {
        canBeSteppedOn = false;
        float upSpeed = 5;
        float upTime = 0;
        Vector3 endItemPosition = newSpikePosition;

        while (upTime < 1f)
        {
            upTime += Time.deltaTime * upSpeed;
            spike.gameObject.transform.position = Vector3.Lerp(originalSpikePosition, endItemPosition, upTime);
            yield return null;
        }
        yield return new WaitForSeconds(5);
        upTime = 0;
        upSpeed = 1;
        while (upTime < 1f)
        {
            upTime += Time.deltaTime * upSpeed;
            spike.gameObject.transform.position = Vector3.Lerp(endItemPosition, originalSpikePosition, upTime);
            yield return null;
        }
        spike.canDealDmg = true;
        canBeSteppedOn = true;
    }
}
