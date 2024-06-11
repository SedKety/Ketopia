using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemHighlight : MonoBehaviour
{
    PlayerInteraction playerInteraction;
    public RaycastHit hit;

    [SerializeField]
    UnityEngine.Material highlightMat;
    [SerializeField]
    UnityEngine.Material defaultMat;

    GameObject lastItem;

    // Start is called before the first frame update
    void Start()
    {
        playerInteraction = GetComponent<PlayerInteraction>();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 10))
        {
            if (hit.collider.GetComponent<PhysicalItemScript>() != null) 
            {
                lastItem = hit.collider.gameObject;
                lastItem.GetComponent<Renderer>().material = defaultMat;

                if (hit.collider.gameObject && lastItem == hit.collider.gameObject)
                {
                    Debug.Log("test message");
                    Debug.Log(hit.collider.gameObject.GetComponent<Renderer>().material.name);

                    Debug.Log(hit.collider.gameObject.GetComponent<Renderer>().material.name);

                    hit.collider.gameObject.GetComponent<Renderer>().material = highlightMat;
                }
            }else 
            {
                Debug.Log("Looked away");
                lastItem.GetComponent<Renderer>().material = defaultMat;
            }

        }
        

    }
}
