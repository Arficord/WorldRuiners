using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage
{
    public float Value { get; private set; }
    public DamageType Type { get; private set; }

    public Damage(float value, DamageType type)
    {
        Value = value;
        Type = type;
    }
}
