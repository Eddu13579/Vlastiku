using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundItem : MonoBehaviour
{
    public Item item;

    public string actionText = "Press G to pick up";

    [SerializeField]
    SpriteRenderer spriteRenderer;

    [HideInInspector] public InventoryManager InventoryManager;
    [HideInInspector] public OverworldUIManager OverworldUIManager;

    void Start()
    {
        InventoryManager = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventoryManager>();
        OverworldUIManager = GameObject.FindGameObjectWithTag("OverworldUIManager").GetComponent<OverworldUIManager>();

        spriteRenderer.sprite = item.image;
    }

    void Update()
    {

    }

    public void pickedUp()
    {
        InventoryManager.AddItem(item);
        Destroy(gameObject);
    }

    public void showActionText()
    {
        OverworldUIManager.showActionText(true);
        OverworldUIManager.changeActionText(actionText);
    }

    public void hideActionText()
    {
        OverworldUIManager.showActionText(false);
    }
}
