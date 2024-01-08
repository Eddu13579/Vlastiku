using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    public float radius; //wie groﬂ die Reichweite ist
    public float attackDuration; //wie lange der angriff dauert
    public Sword(string name, int damage, float attackDuration, float cooldown, float radius)
    {
        this.name = name;
        this.damage = damage;
        
        this.cooldown = cooldown;

        this.radius = radius;
        this.attackDuration = attackDuration;

        this.isSword = true;
    }
}