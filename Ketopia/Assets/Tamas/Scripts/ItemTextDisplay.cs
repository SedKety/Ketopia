using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class ItemTextDisplay : MonoBehaviour
{
    public RaycastHit hit;
    [SerializeField]
    public float textHeight = .3f;
    public string textToDisplay = "F to pickup";
    public TMPro.TextMeshPro displayText;
    GameObject lastItem;
    [SerializeField]
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 10))
        {
            if (hit.collider.GetComponent<PhysicalItemScript>() != null) 
            {
                lastItem = hit.collider.gameObject;
                displayText.transform.SetParent(transform.root, false);
                displayText.gameObject.SetActive(false);
                if (hit.collider.gameObject && lastItem == hit.collider.gameObject)
                {
                    displayText.text =  textToDisplay + hit.collider.gameObject.name;
                    displayText.transform.SetParent(hit.collider.transform.parent, false);
                    textHeight = hit.collider.gameObject.GetComponent<Collider>().bounds.size.y;
                    displayText.transform.position = hit.collider.transform.position + new Vector3(0f, textHeight, 0f);
                    displayText.transform.forward = Camera.main.transform.forward;  
                    displayText.gameObject.SetActive(true);
                }
            }else
            {
                Debug.Log("Looked away");
                displayText.transform.SetParent(transform.root, false);
                displayText.gameObject.SetActive(false);
            }
        }
    }
}
