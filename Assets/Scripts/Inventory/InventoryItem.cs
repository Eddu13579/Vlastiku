using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Image image;
    public Text countText;
    TooltipMenu TooltipMenu;
    ActionMenu ActionMenu;
    ItemManager ItemManager;

    [HideInInspector] public Item item;
    [HideInInspector] public int ID;
    [HideInInspector] public int count = 1;
    [HideInInspector] public Transform parentAfterDrag;

    void Awake()
    {
        TooltipMenu = GameObject.FindGameObjectWithTag("OverworldUIManager").GetComponent<OverworldUIManager>().TooltipMenu.GetComponent<TooltipMenu>();
        ActionMenu = GameObject.FindGameObjectWithTag("OverworldUIManager").GetComponent<OverworldUIManager>().ActionMenuCanvas.GetComponent<ActionMenu>();
        ItemManager = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<ItemManager>();
    }

    public void InitialiseItem(Item newItem)
    {
        item = newItem;

        if(ItemManager == null) //sicherstellen, das Itemmanager geladen wird -> besser machen
        {
            ItemManager = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<ItemManager>();
        }

        ID = ItemManager.itemIDCount;
        ItemManager.itemIDCount++;
        image.sprite = newItem.image;
        RefreshCount();
    }

    public void RefreshCount()
    {
        countText.text = count.ToString();
        bool textActive = count > 1;
        countText.gameObject.SetActive(textActive);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData) //ÜBERPRÜFUNG OB SLOT FÜR KATEGORIE GEMACHT
    {
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (ActionMenu.isFixed == false) //damit nur ein ActionMenu geöffnet werden kann
            {
                if (item.type == ItemType.Consumable)
                {
                    Consumable itemConsumable = (Consumable)item;
                    ActionMenu.addAction(new Consume(this, itemConsumable.effectOnConsume));
                }
                ActionMenu.addAction(new Drop(this));
                ActionMenu.setMenuActive(true);
                ActionMenu.fixScreenPosition(true);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipMenu.changeDisplayedItem(item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipMenu.setActive(false);
    }
}
