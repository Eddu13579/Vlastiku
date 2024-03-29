using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/Effect/Heal")]
public class Heal : Effect
{
    public override void giveEffect()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>(); //siehe Konstruktor Hauptklasse
        playerScript.heal(value);
    }
}
