using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerMoveHandler
{
    public ItemType category;

    public InventoryItem inventoryItem;

    InventoryManager InventoryManager;
    ActionMenu ActionMenu;
    TooltipMenu TooltipMenu;

    public Image image;
    public Color selectedColor, notSelectedColor;

    private void Awake()
    {
        Deselect();
        InventoryManager = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventoryManager>(); ;
        ActionMenu = GameObject.FindGameObjectWithTag("OverworldUIManager").GetComponent<OverworldUIManager>().ActionMenuCanvas.GetComponent<ActionMenu>();
        TooltipMenu = GameObject.FindGameObjectWithTag("OverworldUIManager").GetComponent<OverworldUIManager>().TooltipMenu.GetComponent<TooltipMenu>();
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
        if (transform.childCount == 0) //wenn nicht leer
        {
            inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
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

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (ActionMenu.isFixed == false) //damit nur ein ActionMenu geöffnet werden kann
            {
                if (inventoryItem.item.type == ItemType.Consumable)
                {
                    Consumable itemConsumable = (Consumable)inventoryItem.item;
                    ActionMenu.addAction(new Consume(inventoryItem, itemConsumable.effectOnConsume));
                }
                ActionMenu.addAction(new Drop(inventoryItem));
                ActionMenu.setMenuActive(true);
                ActionMenu.fixScreenPosition(true);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (inventoryItem != null)
        {
            TooltipMenu.changeDisplayedItem(inventoryItem.item);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipMenu.setActive(false);
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        TooltipMenu.GetComponent<TooltipMenu>().Reposition();
    }
}
