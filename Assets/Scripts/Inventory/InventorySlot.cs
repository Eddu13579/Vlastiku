using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public ItemType category;

    InventoryManager InventoryManager;

    public Image image;
    public Color selectedColor, notSelectedColor;

    private void Awake()
    {
        Deselect();
        InventoryManager = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventoryManager>();
    }

    public void Select()
    {
        image.color = selectedColor;
    }

    public void Deselect()
    {
        image.color = notSelectedColor;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0) //wenn leer
        {
            InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            if (category == ItemType.Everything)
            {
                inventoryItem.parentAfterDrag = transform;
                InventoryManager.disableItemEffects(inventoryItem.item);
            }
            else if (category == inventoryItem.item.type)
            {
                inventoryItem.parentAfterDrag = transform;
                InventoryManager.applyItemEffects(inventoryItem.item);
            }
            else
            {
                InventorySlot nextSuitableSlot = InventoryManager.findNextSuitableSlot(inventoryItem.item);
                inventoryItem.parentAfterDrag = nextSuitableSlot.transform;

                if (nextSuitableSlot.category == inventoryItem.item.type)
                {
                    InventoryManager.applyItemEffects(inventoryItem.item);
                }
                else
                {
                    InventoryManager.disableItemEffects(inventoryItem.item);
                }
            }
        }
    }
}
