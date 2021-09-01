using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//abstract class Damage containing damage methods
public abstract class Damage : MonoBehaviour
{
    public abstract float dealPhysicalDamage(float curHealth, float baseDamageTaken);

    public abstract float dealSpecialDamage(float curHealth, float baseDamageTaken);
}
