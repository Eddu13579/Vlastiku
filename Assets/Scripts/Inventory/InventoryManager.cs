using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventoryItem[] Inventory;
    public int inventoryMaxSize = 23;

    public int maxStackedItems = 16;
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;

    int selectedSlot = -1;

    private void Start()
    {
        ChangeSelectedSlot(0);
        Inventory = new InventoryItem[inventoryMaxSize];
    }

    public void ChangeSelectedSlot(int newValue)
    {
        if (selectedSlot >= 0)
        {
            inventorySlots[selectedSlot].Deselect();
        }
        inventorySlots[newValue].Select();
        selectedSlot = newValue;
    }

    //hier werden auch persistenEffects geaddet
    public bool AddItem(Item itemToAdd) //effizienter machen!!!! array mit verschiedenen kategorien speichern (inventoryWeapon, InventoryArmor) und durchgehen als ein Array mit allen
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (slot.category == ItemType.Everything || slot.category == itemToAdd.type)
            {
                if (itemInSlot != null && itemInSlot.item == itemToAdd && itemInSlot.count < maxStackedItems && itemInSlot.item.stackable == true)
                {
                    itemInSlot.count++;
                    itemInSlot.RefreshCount();

                    return true;
                }
            }
        }

        InventorySlot suitableSlot = findNextSuitableSlot(itemToAdd);
        if (suitableSlot != null)
        {
            SpawnNewItem(itemToAdd, suitableSlot);

            if (suitableSlot.category == itemToAdd.type)
            {
                applyItemEffects(itemToAdd);
            }

            return true;
        }
        else
        {
            return false;
        }
    }

    public void DeleteItem(InventoryItem itemToDelete)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null && itemInSlot == itemToDelete)
            {
                itemInSlot.count--;
                if (itemInSlot.count <= 0)
                {
                    Destroy(itemInSlot.gameObject);
                }
                else
                {
                    itemInSlot.RefreshCount();
                }

                if (slot.category == itemInSlot.item.type)
                {
                    disableItemEffects(itemInSlot.item);
                }

                break;
            }
        }

        for (int i = 0; i < inventoryMaxSize; i++)
        {
            if (Inventory[i] == itemToDelete)
            {
                Inventory[i] = null;
                break;
            }
        }
    }

    void SpawnNewItem(Item itemToSpawn, InventorySlot slot) //hier wird NICHT gecheckt ob der Slot die richtige Kategorie hat, das passiert in AddItem()/findNextSuitableSlot
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);

        if (slot.category == itemToSpawn.type)
        {
            applyItemEffects(itemToSpawn);
        }

        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(itemToSpawn);

        for (int i = 0; i < inventoryMaxSize; i++)
        {
            if (Inventory[i] == null)
            {
                Inventory[i] = inventoryItem;
                break;
            }
        }
    }

    public void applyItemEffects(Item itemWithEffects) //schauen wo aufgerufen
    {
        if (itemWithEffects.type == ItemType.Armor)
        {
            Armor itemToAddSword = (Armor)itemWithEffects;

            for (int i = 0; i < itemToAddSword.effectWhenWearing.Length; i++)
            {
                itemToAddSword.effectWhenWearing[i].giveEffect();
            }
        }
    }

    public void disableItemEffects(Item itemWithEffects) //schauen wo aufgerufen
    {
        if (itemWithEffects.type == ItemType.Armor)
        {
            Armor itemToAddSword = (Armor)itemWithEffects;

            for (int i = 0; i < itemToAddSword.effectWhenWearing.Length; i++)
            {
                itemToAddSword.effectWhenWearing[i].removeEffect();
            }
        }
    }

    public InventorySlot findNextSuitableSlot(Item item) //effizienter machen!!!! array mit verschiedenen kategorien speichern (inventoryWeapon, InventoryArmor) und durchgehen als ein Array mit allen
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                if (slot.category == ItemType.Everything || slot.category == item.type)
                {
                    return slot;
                }
            }
        }
        return null;
    }

    public Item GetSelectedItem(bool use)
    {
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem itemInslot = slot.GetComponentInChildren<InventoryItem>();
        if (itemInslot != null)
        {
            Item item = itemInslot.item;
            if (use == true)
            {
                itemInslot.count--;
                if (itemInslot.count <= 0)
                {
                    Destroy(itemInslot.gameObject);
                }
                else
                {
                    itemInslot.RefreshCount();
                }
            }
            return itemInslot.item;
        }
        return null;
    }
}
