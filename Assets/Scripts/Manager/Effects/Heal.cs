using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/Effect/Heal")]
public class Heal : Effect
{
    public Heal(int newValue) : base()
    {
        value = newValue;
    }
    public override void giveEffect()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerScript.heal(value);
    }
}
