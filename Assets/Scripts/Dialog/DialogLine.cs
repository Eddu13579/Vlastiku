using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogLine
{
    public string text;
    public DialogAction[] actions;

    public DialogLine(string newText, DialogAction[] newActions)
    {
        text = newText;
        actions = newActions;
    }
    public DialogLine(string newText, DialogAction newAction1)
    {
        text = newText;
        actions = new DialogAction[9];
        actions[0] = newAction1;
    }
    public DialogLine(string newText, DialogAction newAction1, DialogAction newAction2)
    {
        text = newText;
        actions = new DialogAction[9];
        actions[0] = newAction1;
        actions[1] = newAction2;
    }
    public DialogLine(string newText, DialogAction newAction1, DialogAction newAction2, DialogAction newAction3)
    {
        text = newText;
        actions = new DialogAction[9];
        actions[0] = newAction1;
        actions[1] = newAction2;
        actions[2] = newAction3;
    }
    public DialogLine(string newText, DialogAction newAction1, DialogAction newAction2, DialogAction newAction3, DialogAction newAction4)
    {
        text = newText;
        actions = new DialogAction[9];
        actions[0] = newAction1;
        actions[1] = newAction2;
        actions[2] = newAction3;
        actions[3] = newAction4;
    }
}
