using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTree : MonoBehaviour, IDamageTree
{
    public int Hits { get; set; } = 2;

    public void Damage(int value)
    {
        Hits -= value;

        if (Hits < 0)
        {
            Hits = 0;
        }

    }
}
