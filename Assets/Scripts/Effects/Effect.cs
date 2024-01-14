using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : ScriptableObject //jeder neuer Effekt in neue Klasse
{
    public int value;
    public int duration = 0; //0 = instant

    public string TooltipDescription;
    public bool isShownInTooltip = true;

    [HideInInspector] public Player playerScript;

    public abstract void giveEffect();
}
