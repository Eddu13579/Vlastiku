using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{
    public string actionText = "Press E to read the sign";
    public DialogLine[] dialog;

    OverworldUIManager OverworldUIManager;

    void Start()
    {
        OverworldUIManager = GameObject.FindGameObjectWithTag("OverworldUIManager").GetComponent<OverworldUIManager>();

        dialog = new DialogLine[10];
        dialog[0] = new DialogLine("Hallo Reisender", new nextLine("Hallo"), null);
        dialog[1] = new DialogLine("Wie gehts es Ihnen?", new nextLine("gut und dir?"), null);
        dialog[2] = new DialogLine("nice", new nextLine("freut mich zu hören"), null);
        dialog[3] = new DialogLine("dann verpiss dich", new endDialog("Ende"), null);
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
