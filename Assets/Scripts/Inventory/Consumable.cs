using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/Consumable")]
public class Consumable : Item
{
    public Effect[] effectOnConsume;


    public Consumable()
    {
        type = ItemType.Consumable;
    }
}
