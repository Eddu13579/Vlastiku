using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroundItem : MonoBehaviour
{
    public Item item;
    public int anzahl = 1;

    public string actionText = "Press G to pick up";

    [SerializeField]
    SpriteRenderer spriteRenderer;

    [HideInInspector] public Player playerScript;
    [HideInInspector] public InventoryManager InventoryManager;
    [HideInInspector] public OverworldUIManager OverworldUIManager;

    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
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
        anzahl--;

        if (anzahl == 0)
        {
            playerScript.ItemGone();
            Destroy(gameObject);
        }
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
