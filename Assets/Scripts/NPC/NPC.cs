using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public bool hasShop;
    public string actionText = "Press E to talk";

    [SerializeField]
    public Animator animator;

    [HideInInspector] public InventoryManager inventoryManager;
    [HideInInspector] public OverworldUIManager OverworldUIManager;

    virtual public void action() { }

    void Start()
    {
        inventoryManager = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventoryManager>();
        OverworldUIManager = GameObject.FindGameObjectWithTag("OverworldUIManager").GetComponent<OverworldUIManager>();
    }

    void Update()
    {
        
    }

    public void startDialog()
    {
        
    }

    public void showActionText()
    {
        OverworldUIManager.showActionText(true);
        OverworldUIManager.changeActionText(actionText);
    }

    public void hideActionText()
    {
        OverworldUIManager.showActionText(false);
    }

    public void giveItem(Item itemToGive)
    {
        inventoryManager.AddItem(itemToGive);
    }

    public void takeItem(Item itemToGive)
    {
        
    }

    public void startTalkingAnimation()
    {
        animator.SetBool("isTalking", true);
    }

    public void stopTalkingAnimation()
    {
        animator.SetBool("isTalking", false);
    }
    public void startIdleAnimation()
    {
        
    }

    //FollowThePlayer()
}
