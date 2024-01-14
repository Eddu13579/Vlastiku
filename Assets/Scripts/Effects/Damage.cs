using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/Effect/Damage")]
public class Damage : Effect
{
    public Damage(int newValue) : base()
    {
        value = newValue;
    }
    public override void giveEffect()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>(); //siehe Konstruktor Hauptklasse
        playerScript.damage(value);
    }
}
