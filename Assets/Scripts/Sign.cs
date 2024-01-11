using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{
    public string textToShow;

    OverworldUIManager OverworldUIManager;

    void Start()
    {
        OverworldUIManager = GameObject.FindGameObjectWithTag("OverworldUIManager").GetComponent<OverworldUIManager>();
    }

    public void showDialogText()
    {
        OverworldUIManager.showDialog(true);
        OverworldUIManager.changeDialogText(textToShow);
    }

    public void exitDialogText()
    {
        OverworldUIManager.showDialog(false);
        OverworldUIManager.changeDialogText(textToShow);
    }
}
