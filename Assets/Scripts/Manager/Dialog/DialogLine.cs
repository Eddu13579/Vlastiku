using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogLine
{
    public string text;
    public DialogAction action1;
    public DialogAction action2;

    public DialogLine(string newText, DialogAction newAction1, DialogAction newAction2) {
        text = newText;
        action1 = newAction1;
        action2 = newAction2;
    }
}
