using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suit : NPC 
{
    int randomIdle;
    float time;

    public Item[] randomItem;

    void Start()
    {
        //CODE VON DER OBERKLASSE "START" MUSS HINZUGEFÜGT WERDEN
        inventoryManager = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventoryManager>();
        OverworldUIManager = GameObject.FindGameObjectWithTag("OverworldUIManager").GetComponent<OverworldUIManager>();

        randomIdle = Random.Range(0, 10);
    }

    public override void action()
    {
        giveItem(randomItem[Random.Range(0, randomItem.Length)]);
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

}
