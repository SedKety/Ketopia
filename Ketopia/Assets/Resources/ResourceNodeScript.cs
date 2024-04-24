using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ResourceNodeScript : MonoBehaviour, IDamagable
{
    private int hp;
    public GameObject[] itemsToDrop;
    public virtual void Damagable(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Instantiate(itemsToDrop[1], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
    public virtual void Start()
    {
        StartCoroutine(CheckForGround());
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Island") && (collision.collider.transform != transform.parent))
        {
            Destroy(gameObject);
        }
    }

    public virtual IEnumerator CheckForGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1))
        {
            if (hit.collider.transform != transform.parent)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
        yield return new WaitForSeconds(5);
        StartCoroutine(CheckForGround());
    }
}
