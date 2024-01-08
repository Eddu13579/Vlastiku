using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Weapon
{
    public string name;
    public int damage;

    public float cooldown; //wie schnell wieder angegriffen werden kann

    public bool isSword; //wenn ja, ist sword, wenn nicht, dann gun

}
