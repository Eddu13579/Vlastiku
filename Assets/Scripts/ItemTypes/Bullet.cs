using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/Bullet")]
public class Bullet : Item
{
    public int damage;
    public int speed;
    public GameObject bulletPrefab;

    public Bullet()
    {
        type = ItemType.Bullet;
    }
}
