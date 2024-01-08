using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class Item : ScriptableObject
{
    public TileBase tile;
    public Sprite image;

    public ItemType type;
    public ActionType actionType;

    public bool stackable = true;

    
}

public enum ItemType
{
    Tool,
    Usable,
    Consumable
}

public enum ActionType
{
    Attack,
    Use,
    Consume
}