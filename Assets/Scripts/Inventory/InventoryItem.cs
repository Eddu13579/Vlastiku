using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
{
    public Image image;
    public Text countText;
    public InventorySlot slot;
    ItemManager ItemManager;
    TooltipMenu TooltipMenu;

    [HideInInspector] public Item item;
    [HideInInspector] public int ID;
    [HideInInspector] public int count = 1;
    [HideInInspector] public Transform parentAfterDrag;

    void Awake()
    {
        ItemManager = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<ItemManager>();
        TooltipMenu = GameObject.FindGameObjectWithTag("OverworldUIManager").GetComponent<OverworldUIManager>().TooltipMenu.GetComponent<TooltipMenu>();
    }

    public void InitialiseItem(Item newItem, InventorySlot newSlot)
    {
        item = newItem;
        slot = newSlot;

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

    public void OnEndDrag(PointerEventData eventData) //ÜBERPRÜFUNG OB SLOT FÜR KATEGORIE GEMACHT -< in inventoryslot
    {
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
        {
            TooltipMenu.changeDisplayedItem(item);
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
