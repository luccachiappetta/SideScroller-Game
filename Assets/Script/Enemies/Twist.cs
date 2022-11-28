using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Twist : Enemies
{
    // Start is called before the first frame update
    void Start()
    {
        SightRange = 7f;
        AttackRange = 1.25f;
        e_SPEED = 3f;
        e_HEALTH = 40f;
        e_DAMAGE = 1f;
    }

    // public override void Attack()
    // {
    //     e_RigidBody.velocity = Vector2.zero;
    //     animate.SetTrigger("Attack");
    //     if (Physics2D.Raycast(transform.position, new Vector3(transform.position.x * AttackRange, transform.position.y), WhatCanSee))
    //     {
    //         StartCoroutine(AttackDelay());
    //     }
    //     
    //     transform.localScale = new Vector3(-GetPlayerDirX(), 1f, 1f);
    // }

    // private void OnDrawGizmos()
    // {
    //     Gizmos.DrawLine(transform.position, new Vector3(transform.position.x * 1.25f, transform.position.y));
    // }
}
