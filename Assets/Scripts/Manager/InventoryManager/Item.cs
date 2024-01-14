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
    public string descriptionText;

    public Effect[] effectOnConsume;

    public ItemType type;

    public bool stackable = true;

}

public enum ItemType
{
    Consumable
}