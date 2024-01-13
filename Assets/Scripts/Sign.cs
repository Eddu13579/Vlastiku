using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.AssetImporters;

public class Sign : MonoBehaviour
{
    public string actionText = "Press E to read the sign";
    public DialogLine[] dialog;

    OverworldUIManager OverworldUIManager;
    ItemManager ItemManager;

    void Start()
    {
        OverworldUIManager = GameObject.FindGameObjectWithTag("OverworldUIManager").GetComponent<OverworldUIManager>();
        ItemManager = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<ItemManager>();

        dialog = new DialogLine[10];
        dialog[0] = new DialogLine("Hallo Reisender", new nextLine("Hallo"), null);
        dialog[1] = new DialogLine("Wie gehts es Ihnen?", new nextLine("Gut und dir?"), null);
        dialog[2] = new DialogLine("Nice", new nextLine("Freut mich zu hören"), new giveItem("Kann ich ein apfel haben?", ItemManager.getFoodWithName("Apple"), true));
        dialog[3] = new DialogLine("Dann verpiss dich", new endDialog("Ende"), null);
    }

    public void startDialog()
    {
        OverworldUIManager.startDialog(dialog);
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
