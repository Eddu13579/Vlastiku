using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    Player playerScript;
    OverworldUIManager OverworldUIManager;
    InventoryManager InventoryManager;

    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        OverworldUIManager = GameObject.FindGameObjectWithTag("OverworldUIManager").GetComponent<OverworldUIManager>();
        InventoryManager = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventoryManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            playerScript.startSprinting();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            playerScript.stopSprinting();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (playerScript.isTalkable == true)
            {
                playerScript.startTalkingWithNPC();
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerScript.Attack();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            playerScript.changeWeapon();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            OverworldUIManager.showInventory();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OverworldUIManager.showPause();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            InventoryManager.ChangeSelectedSlot(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            InventoryManager.ChangeSelectedSlot(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            InventoryManager.ChangeSelectedSlot(2);
        }
    }
}
