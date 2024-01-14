using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : ScriptableObject //jeder neuer Effekt in neue Klasse
{
    public int value;
    public int duration; //0 = instant
    [HideInInspector] public Player playerScript;

    public Effect()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public abstract void giveEffect();
}

public class Regeneration : Effect //IN EIGENE KLASSE
{
    public Regeneration() : base() { }
    public override void giveEffect()
    {

    }
}

public class Poison : Effect //IN EIGENE KLASSE
{
    public Poison() : base() { }
    public override void giveEffect()
    {

    }
}
