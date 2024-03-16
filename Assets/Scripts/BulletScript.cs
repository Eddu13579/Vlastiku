using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public Gun weaponThatShot;
    public Bullet bulletType;

    public LayerMask enemyLayer;

    float time = 0f;
    float timeToDie = 5f;

    void Start()
    {
        Player playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        weaponThatShot = (Gun)playerScript.currentlyHoldingWeapon;
        bulletType = weaponThatShot.bulletUsed;

        transform.position = new Vector2(playerScript.rb.position.x + playerScript.spriteRenderer.size.x / 2, playerScript.rb.position.y + playerScript.spriteRenderer.size.y / 2);
    }
    void Update()
    {
        if (time >= timeToDie)
        {
            Destroy(gameObject);
        }
        else
        {
            time = time + Time.deltaTime;
        }
                
        transform.Translate(new Vector2(bulletType.speed, 0) * Time.deltaTime);
                
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == enemyLayer)
        {
            collision.gameObject.GetComponent<Enemy>().receiveDamage(bulletType.damage);
        }
    }
}
