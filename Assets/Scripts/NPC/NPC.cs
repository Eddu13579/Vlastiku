using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    bool hasShop;

    [SerializeField]
    public Animator animator;

    [HideInInspector] public InventoryManager inventoryManager;

    virtual public void action() { }

    void Start()
    {
        inventoryManager = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventoryManager>();
    }

    void Update()
    {
        
    }

    public void giveItem(Item itemToGive)
    {
        inventoryManager.AddItem(itemToGive);
    }

    public void startTalkingAnimation()
    {
        animator.SetBool("isTalking", true);
    }

    public void stopTalkingAnimation()
    {
        animator.SetBool("isTalking", false);
    }
}
