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
    GameObject interactionObject;

    public bool isEnabled = true;

    public Vector2 movement;

    public LayerMask enemyLayer;

    public Weapon currentlyHoldingWeapon;

    public float currentHealth = 100;
    public float maximumHealth = 100;

    public float jumpStrength = 750;
    public float walkingSpeed = 4f;
    public float sprintingSpeed = 7f;

    bool isSprinting = false;

    public bool isTalkingWithNPCPossible = false;
    public bool isInteractionPossible = false;
    public bool isInDialog = false;
    public bool isAbleToMove = true;

    void Start()
    {
        currentlyHoldingWeapon = new Sword("Longsword", 20, 5f, 5f, 2f);
    }

    void Update()
    {
        if (isEnabled)
        {
            if (isAbleToMove)
            {
                if (movement.x < 0)
                {
                    spriteRenderer.flipX = true;
                }
                else if (movement.x > 0)
                {
                    spriteRenderer.flipX = false;
                }

                if (movement.x != 0 || movement.y != 0)
                {
                    animator.SetBool("isWalking", true);
                }
                if (movement.x == 0 && movement.y == 0)
                {
                    animator.SetBool("isWalking", false);
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (isEnabled)
        {
            if (isAbleToMove)
            {
                movement = movement.normalized;
                if (isSprinting)
                {
                    rb.MovePosition(rb.position + movement * sprintingSpeed * Time.fixedDeltaTime);
                }
                else
                {
                    rb.MovePosition(rb.position + movement * walkingSpeed * Time.fixedDeltaTime);
                }
            }
        }
    }

    public void startSprinting()
    {
        isSprinting = true;
        animator.SetBool("isSprinting", true);
    }
    public void stopSprinting()
    {
        isSprinting = false;
        animator.SetBool("isSprinting", false);
    }
    public void startTalkingWithNPC() //nachdem man E gedrückt hast
    {
         animator.SetBool("isAnswering", true);
         nearestNPC.GetComponent<NPC>().startTalkingAnimation();
         nearestNPC.GetComponent<NPC>().showActionText();
         nearestNPC.GetComponent<NPC>().action();
    }
    public void stopTalkingWithNPC() //wenn man weggeht
    {
        animator.SetBool("isAnswering", false);
    }
    public void startInteraction() //nachdem man E gedrückt hast
    {
        isAbleToMove = false;

        if(interactionObject.tag == "Sign")
        {
            isInDialog = true;
            interactionObject.GetComponent<Sign>().hideActionText();
            interactionObject.GetComponent<Sign>().startDialog();
            animator.SetBool("isAnswering", true);
        }
    }
    public void stopInteraction()
    {
        isAbleToMove = true;
        animator.SetBool("isAnswering", false);

        if (interactionObject.tag == "Sign")
        {
            isInDialog = false;
            interactionObject.GetComponent<Sign>().showActionText();
        }
    }
    public void changeWeapon()
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
    public void Attack()
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

    private void OnTriggerEnter2D(Collider2D collision)  //für GameObject mit LAYERN
    {
        if(collision.gameObject.layer == 8) //NPC
        {
            isTalkingWithNPCPossible = true;
            nearestNPC = collision.gameObject;
            nearestNPC.GetComponent<NPC>().showActionText();
        }
    }

    private void OnTriggerExit2D(Collider2D collision) //für GameObject mit LAYERN
    {
        if (collision.gameObject.layer == 8) //NPCs
        {
            isTalkingWithNPCPossible = false;
            nearestNPC.GetComponent<NPC>().stopTalkingAnimation();
            nearestNPC.GetComponent<NPC>().hideActionText();
            nearestNPC = null;
            stopTalkingWithNPC();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) //für Kollisionen mit BoxCollidern
    {
        isInteractionPossible = true;
        interactionObject = collision.gameObject;

        if (interactionObject.tag == "Sign")
        {
            interactionObject.GetComponent<Sign>().showActionText();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)  //für Kollisionen mit BoxCollidern
    {
        isInteractionPossible = false;

        if (interactionObject.tag == "Sign")
        {
            interactionObject.GetComponent<Sign>().hideActionText();
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
