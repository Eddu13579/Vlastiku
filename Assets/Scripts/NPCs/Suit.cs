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
        ItemManager = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<ItemManager>();

        randomIdle = Random.Range(0, 10);

        dialog = new DialogLine[10];
        dialog[0] = new DialogLine("Hallo Reisender", new nextLine("Hallo"), null);
        dialog[1] = new DialogLine("Möchten sie etwas kaufen?", new nextLine("Ja, Gerne"), new nextLine("Nein, Danke"));
        dialog[2] = new DialogLine("Nice", new nextLine("Freut mich zu hören"), new giveItem("Kann ich ein apfel haben?", ItemManager.getFoodWithName("Apple"), true));
        dialog[3] = new DialogLine("Dann verpiss dich", new endDialog("Ende"), null);
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
