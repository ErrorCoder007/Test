using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageTree
{
    public int Hits { get; set; }
    public void Damage(int amount);
}
