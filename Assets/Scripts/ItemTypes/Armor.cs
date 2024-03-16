using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/Armor")]
public class Armor : Item
{
    public ArmorType armorType;
    public PersistentEffect[] effectWhenWearing;

    public Armor()
    {
        stackable = false;
        type = ItemType.Armor;
    }

    public enum ArmorType
    {
        Head,
        Chest,
        Leggings,
        Boots
    }
}
