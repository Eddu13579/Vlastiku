using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suit : NPC 
{
    int randomIdle;
    float time;

    public Item[] randomItem;

    private void Start()
    {
        randomIdle = Random.Range(0, 10);
    }

    public override void action()
    {
        giveItem(randomItem[Random.RandomRange(0, randomItem.Length)]);
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
