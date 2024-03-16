using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemyName;
    public float maxHealth;
    public float currentHealth;

    public float strength;

    void Update()
    {
        
    }

    public void Attack()
    {

    }
    public void receiveDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth < 0)
        {
            Destroy(this.gameObject);
        }
    }
}
