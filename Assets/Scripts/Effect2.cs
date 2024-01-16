using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Effect2
{
    public int value = 0;
    public int duration = 0;

    public EffectType effect;

    public String tooltip;

    public virtual void giveEffect()
    {
        switch (effect)
        {
            case EffectType.Heal:
                //
                break;
        }
    }
}

public enum EffectType
{
    Heal,
    Damage
}

public class Heal2 : Effect2
{
    public Heal2() : base() { }

    public override void giveEffect()
    {
        
    }
}
