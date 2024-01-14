using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/PersistentEffects/WalkingSpeed")]
public class WalkingSpeed : PersistentEffect
{
    public WalkingSpeed(int newValue) : base()
    {
        value = newValue;
    }
    public override void giveEffect()
    {
        if (isEffectGiven == false)
        {
            isEffectGiven = true; //effizienter machen, indem man sicherstellt, das giveEffect bzw applyItemsEffects nur einmal pro Item aufgerufen wird
            playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>(); //siehe Konstruktor Hauptklasse
            playerScript.walkingSpeed += value;
        }
    }

    public override void removeEffect()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>(); //siehe Konstruktor Hauptklasse
        playerScript.walkingSpeed -= value;
        isEffectGiven = false;
    }
}
