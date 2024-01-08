using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    public Rigidbody2D rb;
    [SerializeField]
    public Animator animator;
    [SerializeField]
    public BoxCollider2D boxCollider2D;
    [SerializeField]
    public SpriteRenderer spriteRenderer;
    [SerializeField]
    public GameObject AttackPoint;
    [SerializeField]
    public GameObject bulletPrefab;
    GameObject nearestNPC;

    public Vector2 movement;
    public int direction = 0;

    public LayerMask enemyLayer;

    public Weapon currentlyHoldingWeapon;

    public float currentHealth = 100;
    public float maximumHealth = 100;

    public float jumpStrength = 750;
    public float walkingSpeed = 4f;
    public float sprintingSpeed = 7f;
    
    bool isEnabled = true;

    bool isSprinting = false;

    public bool isTalkable = false;

    void Start()
    {
        currentlyHoldingWeapon = new Sword("Longsword", 20, 5f, 5f, 2f);

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSprinting = true;
            animator.SetBool("isSprinting", true);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprinting = false;
            animator.SetBool("isSprinting", false);

        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if(movement.x < 0)
        {
            spriteRenderer.flipX = true;
        } else if(movement.x > 0)
        {
            spriteRenderer.flipX = false;
        }

        if(movement.x != 0 || movement.y != 0)
        {
            animator.SetBool("isWalking", true);
        }
        if(movement.x == 0 && movement.y == 0)
        {
            animator.SetBool("isWalking", false);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if(isTalkable)
            {
                animator.SetBool("isAnswering", true);
                NPC npcscript = nearestNPC.GetComponent<NPC>();
                npcscript.animate();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            direction = 270;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (currentlyHoldingWeapon.isSword)
            {
                currentlyHoldingWeapon = new Gun("Gun", 20, 5, 10f);
            }
            else
            {
                currentlyHoldingWeapon = new Sword("Longsword", 20, 5f, 5f, 2f);
            }
        }
        /*animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);*/     
    }

    void FixedUpdate()
    {
        if (isEnabled)
        {
            if (isSprinting)
            {
                rb.MovePosition(rb.position + movement * sprintingSpeed * Time.fixedDeltaTime);
            } else
            {
                rb.MovePosition(rb.position + movement * walkingSpeed * Time.fixedDeltaTime);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        nearestNPC = collision.gameObject;
        if(collision.gameObject.layer == 8)
        {
            isTalkable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            NPC npcscript = nearestNPC.GetComponent<NPC>();
            npcscript.animator.SetBool("isTalking", false);
            isTalkable = false;
            animator.SetBool("isAnswering", false);
            nearestNPC = null;
        }
    }

    void Attack()
    {
        if (currentlyHoldingWeapon.isSword)
        {
            Sword attackingWeapon = (Sword)currentlyHoldingWeapon;
            Collider2D[] hittedEnemies = Physics2D.OverlapCircleAll(rb.position, attackingWeapon.radius, enemyLayer);

            foreach (Collider2D enemy in hittedEnemies)
            {
                enemy.gameObject.GetComponent<Enemy>().receiveDamage(attackingWeapon.damage);
            }
        }
        else
        {
            Instantiate(bulletPrefab, rb.position, Quaternion.identity); //GameObject bullet = 
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(rb.position, rb.position + movement);

        if (currentlyHoldingWeapon != null && currentlyHoldingWeapon.isSword)
        {
            Sword attackingWeapon = (Sword)currentlyHoldingWeapon;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(rb.position, attackingWeapon.radius);
        }
    }
}