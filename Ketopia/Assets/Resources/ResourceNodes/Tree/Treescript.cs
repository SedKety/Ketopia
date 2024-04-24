using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treescript : ResourceNodeScript
{
    public override void Damagable(int damage)
    {
        base.Damagable(damage);
        transform.localScale -= new Vector3(0.25f, 0.25f, 0.25f);
    }
}
