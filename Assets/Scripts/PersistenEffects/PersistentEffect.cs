using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PersistentEffect : ScriptableObject //jeder neuer Effekt in neue Klasse
{
    public int value;
    public bool isEffectGiven = false;

    public string TooltipDescription;
    public bool isShownInTooltip = true;

    [HideInInspector] public Player playerScript;

    public PersistentEffect()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        isEffectGiven = false;
    }

    public abstract void giveEffect();

    public abstract void removeEffect();
}
