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
        e_SPEED = 4.5f;
        e_HEALTH = 40f;
        e_DAMAGE = 1f;
    }
    
    public override bool CanSeePlayer()
    {
        if (Physics2D.OverlapCircle(transform.position, SightRange, WhatCanSee))
        {
            animate.SetBool("Awake",true);
            return true;
        }
        return false;
    }
    
    //run at player
    
    //attack player

    // Update is called once per frame
    
    // public override bool CanAttack()
    // {
    //     if (Physics2D.OverlapCircle(new Vector2(transform.position.x - 1.2f, transform.position.y), AttackRange, WhatCanSee))
    //     {
    //         return true;
    //     }
    //
    //     return false;
    // }


    // public override void AttackPlayer()
    // {
    //     if (Physics2D.OverlapCircle(transform.position, AttackRange, WhatCanSee))
    //     {
    //         animate.SetTrigger("Attack");
    //     }
    // }

    // private void OnDrawGizmosSelected()
    // {
    //     Gizmos.DrawWireSphere();
    // }
}
