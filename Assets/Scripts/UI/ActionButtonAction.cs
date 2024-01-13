using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionButtonAction
{
    public Item item;
    public string actionButtonText;
    public OverworldUIManager OverworldUIManager;
    public ActionMenu ActionMenu;

    public ActionButtonAction()
    {
        OverworldUIManager = GameObject.FindGameObjectWithTag("OverworldUIManager").GetComponent<OverworldUIManager>();
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
    public Consume() : base()
    {
        actionButtonText = "Consume";
    }

    public override void action()
    {
        hideActionMenu();
    }
}
public class Drop : ActionButtonAction
{
    public Drop() : base()
    {
        actionButtonText = "Drop";
    }
    public override void action()
    {
        ActionMenu.fixScreenPosition(false);
        hideActionMenu();
    }
}
