using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suit : NPC 
{

    int randomIdle;
    float time;

    public InventoryManager inventoryManager;
    public Item[] randomItem;

    private void Start()
    {
        randomIdle = Random.Range(0, 10);
    }

    void Update()
    {
        time += Time.deltaTime;
        if(time >= 2)
        {
            animator.SetInteger("randomIdle", randomIdle);
            randomIdle = Random.Range(0, 11);
            time = 0;
        }
    }

    public override void animate()
    {
        GiveItem(Random.Range(0,6));
        animator.SetBool("isTalking", true);
    }

    public void GiveItem(int id)
    {
        inventoryManager.AddItem(randomItem[id]);
    }

}
