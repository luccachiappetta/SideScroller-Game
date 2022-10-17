using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Assasin : Enemies
{
    // Start is called before the first frame update
    void Start()
    {
        SightRange = 5f;
        AttackRange = 1f;
        e_SPEED = 25f;
        e_HEALTH = 40f;
        e_DAMAGE = 10f;
    }
    
    //run at player
    
    //attack player

    // Update is called once per frame

    public override bool CanSeePlayer()
    {
        if (Physics2D.OverlapCircle(transform.position, SightRange, WhatCanSee))
        {
            return true;
        }
        return false;
    }

    // private void AssasinAwake()
    // {
    //     if (CanSeePlayer())
    //     {
    //         Debug.Log("Awake!");
    //     }
    // }
    

    public override void AttackPlayer()
    {
        if (Physics2D.OverlapCircle(transform.position, AttackRange, WhatCanSee))
        {
            animate.SetTrigger("Attack");
        }
    }


}
