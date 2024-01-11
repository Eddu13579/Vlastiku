using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    Player playerScript;
    OverworldUIManager OverworldUIManager;

    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        OverworldUIManager = GameObject.FindGameObjectWithTag("OverworldUIManager").GetComponent<OverworldUIManager>();
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
            playerScript.talkingWithNPC();
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
    }
}
