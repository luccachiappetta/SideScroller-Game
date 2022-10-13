using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemies : MonoBehaviour
{
    protected float e_HEALTH;
    protected float e_DAMAGE;

    [SerializeField] LayerMask WhatCanSee;

    protected float AttackRange;
    protected float SightRange;
    
    void CanSeePlayer()
    {
        if (Physics2D.Raycast(transform.position, Vector2.left, SightRange, WhatCanSee))
        {
            Debug.Log("i see you");
        }
    }
    
    void Update()
    { 
        CanSeePlayer();
    }

    // protected float DistanceToPlayer()
    // {
    //     return Vector3.Distance(transform.position, CharacterController2D.myPlayer.transform.position);
    // }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * SightRange);
        Gizmos.color = Color.blue;
    }
}
