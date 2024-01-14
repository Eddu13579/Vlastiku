using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/Dialog")]

public class Dialog : ScriptableObject
{
    public DialogLine[] Dialoge;
}