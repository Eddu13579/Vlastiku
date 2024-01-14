using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/PersistentEffects/Resistance")]
public class Resistance : PersistentEffect
{
    public Resistance(int newValue) : base()
    {
        value = newValue;
    }
    public override void giveEffect()
    {
        if (isEffectGiven == false)
        {
            isEffectGiven = true; //effizienter machen, indem man sicherstellt, das giveEffect bzw applyItemsEffects nur einmal pro Item aufgerufen wird
            playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>(); //siehe Konstruktor Hauptklasse
            playerScript.resistance += value;
        }
    }

    public override void removeEffect()
    {
        isEffectGiven = false;
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>(); //siehe Konstruktor Hauptklasse
        playerScript.resistance -= value;
    }
}
