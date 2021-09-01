using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//class for player damage
public class DamagePlayer : Damage
{ 
    public override float dealPhysicalDamage(float curHealth, float baseDamageTaken)
    {
        return curHealth - baseDamageTaken;
    }

    public override float dealSpecialDamage(float curHealth, float baseDamageTaken)
    {
        return curHealth - baseDamageTaken;
    }

}
