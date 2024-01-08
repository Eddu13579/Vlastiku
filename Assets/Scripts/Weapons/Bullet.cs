using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Gun weaponThatShot;

    public LayerMask enemyLayer;

    float time = 0f;
    float timeToDie = 5f;

    int direction = 0;

    void Start()
    {
        Player playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if(playerScript.movement.x > 0)
        {
            direction = 1;
            transform.position = new Vector2(playerScript.rb.position.x + playerScript.spriteRenderer.size.x / 2, playerScript.rb.position.y + playerScript.spriteRenderer.size.y / 2);
        }
        else if (playerScript.movement.x < 0)
        {
            direction = 3;
            transform.position = new Vector2(playerScript.rb.position.x - playerScript.spriteRenderer.size.x / 2, playerScript.rb.position.y + playerScript.spriteRenderer.size.y / 2);
        } else if (playerScript.movement.y > 0)
        {
            direction = 4;
            transform.position = new Vector2(playerScript.rb.position.x, playerScript.rb.position.y + playerScript.spriteRenderer.size.y);
        }
        else if (playerScript.movement.y < 0)
        {
            direction = 2;
            transform.position = new Vector2(playerScript.rb.position.x, playerScript.rb.position.y);
        }
        weaponThatShot = (Gun)playerScript.currentlyHoldingWeapon;
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
        switch (direction)
        {
            case 1:
                transform.Translate(new Vector2(weaponThatShot.bulletSpeed, 0) * Time.deltaTime);
                break;
            case 2:
                transform.Translate(new Vector2(0, weaponThatShot.bulletSpeed * -1) * Time.deltaTime);
                break;
            case 3:
                transform.Translate(new Vector2(weaponThatShot.bulletSpeed * -1, 0) * Time.deltaTime);
                break;
            case 4:
                transform.Translate(new Vector2(0, weaponThatShot.bulletSpeed) * Time.deltaTime);
                break;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == enemyLayer)
        {
            collision.gameObject.GetComponent<Enemy>().receiveDamage(weaponThatShot.damage);
        }
    }
}
