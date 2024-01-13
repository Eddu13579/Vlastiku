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

    [HideInInspector] public Item item;
    [HideInInspector] public int count = 1;
    [HideInInspector] public Transform parentAfterDrag;

    void Awake()
    {
        TooltipMenu = GameObject.FindGameObjectWithTag("OverworldUIManager").GetComponent<OverworldUIManager>().TooltipMenu.GetComponent<TooltipMenu>();
        ActionMenu = GameObject.FindGameObjectWithTag("OverworldUIManager").GetComponent<OverworldUIManager>().ActionMenu.GetComponent<ActionMenu>();
    }

    public void InitialiseItem(Item newItem)
    {
        item = newItem;
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

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            ActionMenu.changeAction1(new Consume());
            ActionMenu.changeAction2(new Drop());
            ActionMenu.setActive(true);
            ActionMenu.fixScreenPosition(true);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipMenu.changeHeaderText(item.name);
        TooltipMenu.changeDescriptionText(item.descriptionText);
        TooltipMenu.setActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipMenu.setActive(false);
    }
}
