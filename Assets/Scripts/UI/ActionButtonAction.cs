using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class ActionButtonAction
{
    public InventoryItem item;
    public string actionButtonText;
    public OverworldUIManager OverworldUIManager;
    public InventoryManager InventoryManager;
    public Player playerScript;
    public ActionMenu ActionMenu;

    public ActionButtonAction(InventoryItem newItem)
    {
        item = newItem;
        OverworldUIManager = GameObject.FindGameObjectWithTag("OverworldUIManager").GetComponent<OverworldUIManager>();
        InventoryManager = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventoryManager>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        ActionMenu = OverworldUIManager.ActionMenuCanvas.GetComponent<ActionMenu>();
    }
    public abstract void action();

    public void hideActionMenu()
    {
        ActionMenu.fixScreenPosition(false);
        OverworldUIManager.ActionMenuCanvas.GetComponent<ActionMenu>().removeActions();
        OverworldUIManager.ActionMenuCanvas.GetComponent<ActionMenu>().setMenuActive(false);
    }
}
public class Consume : ActionButtonAction
{
    public Effect[] effect;
    public Consume(InventoryItem newItem, Effect[] newEffect) : base(newItem)
    {
        effect = newEffect;
        actionButtonText = "Consume";
    }

    public override void action()
    {
        if (effect != null)
        {
            for (int i = 0; i < effect.Length; i++)
            {
                if (effect[i] != null)
                {
                    effect[i].giveEffect();
                }
            }
        }
        InventoryManager.DeleteItem(item);
        hideActionMenu();
    }
}

public class Drop : ActionButtonAction
{
    public Drop(InventoryItem newItem) : base(newItem)
    {
        actionButtonText = "Drop";
    }
    public override void action()
    {
        playerScript.DropItem(item.item);
        InventoryManager.DeleteItem(item);
        hideActionMenu();
    }
}
