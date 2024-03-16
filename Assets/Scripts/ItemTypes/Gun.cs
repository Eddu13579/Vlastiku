using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/Gun")]
public class Gun : Item
{
    public int cooldown;

    public Bullet bulletUsed;

    public Gun()
    {
        stackable = false;
        type = ItemType.Gun;
    }

    public void Attack(Vector3 pos)
    {
        Instantiate(bulletUsed.bulletPrefab, pos, Quaternion.identity);
    }
}
