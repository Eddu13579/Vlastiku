using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract  class Effect
{
    public int value;
    public int duration; //0 = instant
    public Player playerScript;

    public Effect()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public abstract void giveEffect();
}

public class Heal : Effect
{
    public Heal(int newValue): base() {
        value = newValue;
    }
    public override void giveEffect()
    {
        playerScript.heal(value);
    }
}
public class Regeneration : Effect
{
    public Regeneration() : base() { }
    public override void giveEffect()
    {

    }
}
public class Damage : Effect
{
    public Damage(int newValue) : base()
    {
        value = newValue;
    }
    public override void giveEffect()
    {
        playerScript.damage(value);
    }
}

public class Poison : Effect
{
    public Poison() : base() { }
    public override void giveEffect()
    {

    }
}
