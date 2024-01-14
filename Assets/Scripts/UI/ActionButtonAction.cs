using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionButtonAction
{
    public InventoryItem item;
    public string actionButtonText;
    public OverworldUIManager OverworldUIManager;
    public InventoryManager InventoryManager;
    public ActionMenu ActionMenu;

    public ActionButtonAction(InventoryItem newItem)
    {
        item = newItem;
        OverworldUIManager = GameObject.FindGameObjectWithTag("OverworldUIManager").GetComponent<OverworldUIManager>();
        InventoryManager = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventoryManager>();
        ActionMenu = OverworldUIManager.ActionMenu.GetComponent<ActionMenu>();
    }
    public abstract void action();

    public void hideActionMenu()
    {
        ActionMenu.fixScreenPosition(false);
        OverworldUIManager.ActionMenu.GetComponent<ActionMenu>().setActive(false);
    }
}
public class Consume : ActionButtonAction
{
    public Effect effect;
    public Consume(InventoryItem newItem, Effect newEffect) : base(newItem)
    {
        effect = newEffect;
        actionButtonText = "Consume";
    }

    public override void action()
    {
        effect.giveEffect();
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
        ActionMenu.fixScreenPosition(false);
        hideActionMenu();
    }
}
