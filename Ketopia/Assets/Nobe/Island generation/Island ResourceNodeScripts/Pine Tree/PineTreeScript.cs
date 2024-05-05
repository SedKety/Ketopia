using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PineTreeScript : ResourceNode
{
    public override void IDamagable(int dmg, GameObject weaponUsed)
    {
        dmg -= nodeHp;
        if(nodeHp <= 0)
        {

        }
    }
}
