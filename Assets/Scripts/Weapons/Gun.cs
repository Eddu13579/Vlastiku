using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    public float bulletSpeed;
    public Gun(string name, int damage, float cooldown, float bulletSpeed)
    {
        this.name = name;
        this.damage = damage;

        this.cooldown = cooldown;

        this.bulletSpeed = bulletSpeed;

        this.isSword = false;
    }
}
