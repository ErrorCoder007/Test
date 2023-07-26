using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTree : MonoBehaviour
{
    private TreeControl treeControl => GetComponentInParent<TreeControl>();
    public int Hits = 2;

    public void Damage(int value)
    {
        Hits -= value;

        if (Hits <= 0)
        {
            Hits = 0;
            treeControl.DestructionOfTheTree(gameObject);
        }

        treeControl.DeckControl(gameObject);
    }
}
