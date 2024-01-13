using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DialogAction
{
    public string dialogActionText;

    public DialogAction(string newDialogActionText)
    {
        dialogActionText = newDialogActionText;
    }
    public abstract void action();

    public void goToNextLine()
    {
        GameObject.FindGameObjectWithTag("OverworldUIManager").GetComponent<OverworldUIManager>().nextDialog();
    }
}

public class nextLine : DialogAction
{
    public nextLine(string newDialogActionText) : base(newDialogActionText) { }
    public override void action()
    {
        goToNextLine();
    }
}

public class openShop : DialogAction
{
    public openShop(string newDialogActionText) : base(newDialogActionText) { }
    public override void action()
    {
        //
    }

}

public class giveItem : DialogAction
{
    Item itemToGive;
    bool shouldContinue;

    public giveItem(string newDialogActionText, Item newItemToGive, bool newShouldContinue) : base(newDialogActionText) {
        itemToGive = newItemToGive;
        shouldContinue = newShouldContinue;
    }
    public override void action()
    {
        GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventoryManager>().AddItem(itemToGive);

        if(shouldContinue == true)
        {
            goToNextLine();
        }
    }

}

public class endDialog : DialogAction
{
    public endDialog(string newDialogActionText) : base(newDialogActionText) { }
    public override void action()
    {
        GameObject.FindGameObjectWithTag("OverworldUIManager").GetComponent<OverworldUIManager>().endDialog();
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().stopInteraction();
    }

}
