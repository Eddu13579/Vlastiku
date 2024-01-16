using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PersistentEffect
{
    public int value = 0;

    [HideInInspector] public bool isEffectGiven = false;

    public PersistentEffectType effect;

    public string TooltipDescription;
    public bool isShownInTooltip = true;

    [HideInInspector] public Player playerScript;

    public virtual void giveEffect()
    {
        if (isEffectGiven == false)
        {
            isEffectGiven = true; //effizienter machen, indem man sicherstellt, das giveEffect bzw applyItemsEffects nur einmal pro Item aufgerufen wird
            playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            switch (effect)
            {
                case PersistentEffectType.Resistance:
                    playerScript.resistance += value;
                    break;
                case PersistentEffectType.WalkingSpeed:
                    playerScript.walkingSpeed += value;
                    break;
            }


        }
    }

    public virtual void removeEffect()
    {
        isEffectGiven = false;
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>(); //siehe Konstruktor Hauptklasse
        switch (effect)
        {
            case PersistentEffectType.Resistance:
                playerScript.resistance -= value;
                break;
            case PersistentEffectType.WalkingSpeed:
                playerScript.walkingSpeed -= value;
                break;
        }
    }
}

public enum PersistentEffectType
{
    Resistance,
    WalkingSpeed
}