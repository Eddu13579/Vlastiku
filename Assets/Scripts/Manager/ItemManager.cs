using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public int itemIDCount = 0;
    Item[] allItems;

    public Item[] Weapons;
    public Item[] Foods;
    public Item[] Armor;

    private void Start()
    {
        allItems = new Item[999];
        for(int i = 0; i<Foods.Length; i++)
        {
            if (allItems[i] == null) {
                allItems[i] = Foods[i];
            }
        }
    }

    public void getItemWithName(string itemToGet)
    {

    }

    public Item getFoodWithName(string foodToGet)
    {
        for (int i = 0; i < Foods.Length; i++)
        {
            if (Foods[i].name == foodToGet)
            {
                return Foods[i];
            }
        }
        return null;
    }
}
