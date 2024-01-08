using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

abstract public class NPC : MonoBehaviour
{
    bool haveShop;
    string text;
    [SerializeField]
    public Animator animator;

    abstract public void animate();


    // Update is called once per frame
    void Update()
    {
        
    }
}
