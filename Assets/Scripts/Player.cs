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
    public GameObject GroundItemPrefab;
    [SerializeField]
    public GameObject bulletPrefab;

    GameObject nearestNPC;
    GameObject interactionObject;
    GameObject nearestGroundItem;

    public bool isEnabled = true;

    public Vector2 movement;

    public LayerMask enemyLayer;

    public Weapon currentlyHoldingWeapon;

    public float currentHealth = 100;
    public float maximumHealth = 100;

    public int resistance = 0;

    public float jumpStrength = 750;
    public float walkingSpeed = 4f;
    public float sprintingSpeed = 7f;

    bool isSprinting = false;

    public bool isTalkingWithNPCPossible = false;
    public bool isInteractionPossible = false;
    public bool isInDialog = false;
    public bool isAbleToPause = true;
    public bool isAbleToPickUpItem = false;
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
                
                Vector2 oldMovement = movement;

                if (isSprinting)
                {
                    movement = movement * sprintingSpeed;
                }
                else
                {
                    movement = movement * walkingSpeed;
                }

                rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);

                if(movement == oldMovement)
                {
                    animator.SetBool("isWalking", false);
                }

                if (movement != Vector2.zero && movement != oldMovement * walkingSpeed && oldMovement * sprintingSpeed == movement)
                {
                    animator.SetBool("isSprinting", true);
                } else
                {
                    animator.SetBool("isSprinting", false);
                }
            }
        }
    }

    public void startSprinting()
    {
        isSprinting = true;
    }
    public void stopSprinting()
    {
        isSprinting = false;
    }

    public void pickUpItem() //nachdem man G gedrückt hast
    {
        nearestGroundItem.GetComponent<GroundItem>().pickedUp();
    }

    public void DropItem(Item itemToDrop) //droppedItem wird NICHT gelöscht, das passiert bei Drop()
    {
        GameObject droppedItem = Instantiate(GroundItemPrefab, transform.parent);
        droppedItem.GetComponent<GroundItem>().item = itemToDrop;
        droppedItem.transform.position = transform.position;
    }

    public void ItemGone() //wenn das letzte Item aufgehoben worden ist
    {
        isAbleToPickUpItem = false;
        nearestGroundItem = null;
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
            isAbleToPause = false;
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
            isAbleToPause = true;
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

    public void heal(int healAmount)
    {
        if(currentHealth + healAmount >= maximumHealth)
        {
            currentHealth = currentHealth + (currentHealth + healAmount - maximumHealth);
        } else
        {
            currentHealth += healAmount;
        }
    }
    public void damage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if(currentHealth <= 0)
        {
            GameOver();
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

    public void GameOver()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)  //für GameObject mit LAYERN
    {
        if(collision.gameObject.layer == 8) //NPC
        {
            isTalkingWithNPCPossible = true;
            nearestNPC = collision.gameObject;
            nearestNPC.GetComponent<NPC>().showActionText();
        }
        if (collision.gameObject.layer == 10) //GroundItem
        {
            isAbleToPickUpItem = true;
            nearestGroundItem = collision.gameObject;
            nearestGroundItem.GetComponent<GroundItem>().showActionText();
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
        if (collision.gameObject.layer == 10) //GroundItem
        {
            if(nearestGroundItem != null)
            {
                nearestGroundItem.GetComponent<GroundItem>().hideActionText();
            }
            ItemGone();
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
