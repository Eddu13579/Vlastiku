using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Effect
{
    public int value = 0;
    public int duration = 0;

    public EffectType effect;

    public string TooltipDescription;
    public bool isShownInTooltip = true;

    [HideInInspector] public Player playerScript;

    public virtual void giveEffect()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>(); //siehe Konstruktor Hauptklasse
        switch (effect)
        {
            case EffectType.Heal:
                playerScript.heal(value);
                break;
            case EffectType.Damage:
                playerScript.damage(value);
                break;
        }
    }
}

public enum EffectType
{
    Heal,
    Damage
}
