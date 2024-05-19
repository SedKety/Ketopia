using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZuigbuisZuigerScript : MonoBehaviour
{
    public Transform succEnd;
    public AirshipManager airshipManager;
    public void Start()
    {
        airshipManager = AirshipManager.instance;
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<PhysicalItemScript>() != null & other.gameObject.GetComponent<PhysicalItemScript>().item.itemType == ItemType.fuel)
        {
           StartCoroutine(Succ(other.gameObject.transform));
        }
    }

    public IEnumerator Succ(Transform item)
    {
        float succSpeed = 1;
        float succTime = 0;
        Vector3 originalItemPosition = item.position;
        Vector3 endItemPosition = succEnd.position;

        while(succTime < 1f)
        {
            succTime += Time.deltaTime * succSpeed;
            item.position = Vector3.Lerp(originalItemPosition, endItemPosition, succTime);
            yield return null;
        }
        item.gameObject.GetComponent<PhysicalItemScript>().item.BurnItem(0f, item.gameObject.GetComponent<PhysicalItemScript>().quantity);
        Destroy(item.gameObject);
    }
}
