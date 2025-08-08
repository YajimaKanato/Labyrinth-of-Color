using UnityEngine;

public class DamageCalculation
{
    public float Damage(float atk, float def)
    {
        return atk - def / 10;
    }
}
