using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class Item : ScriptableObject
{
    public Sprite image;
    public string descriptionText;

    public bool stackable = true;

    public ItemType type;
}

public enum ItemType
{
    Weapon,
    Armor,
    Equipment,
    Consumable,
    Coin,
    Ammunition,
    Everything
}