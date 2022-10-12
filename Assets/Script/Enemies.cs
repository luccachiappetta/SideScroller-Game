using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemies : MonoBehaviour
{
    // protected enum stats
    // {
    //     ATTACK,
    //     HEALTH,
    //     SPEED,
    //     DISTANCE
    // };

    [SerializeField] LayerMask WhatCanSee;

    protected float SightRange;
    
    void CanSeePlayer()
    {
        // // Physics2D.Raycast(transform.position, transform.TransformDirection(Vector3.left) * SightRange, WhatCanSee);;
        // RaycastHit hit;
        // if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left) * SightRange, out hit, Mathf.Infinity, WhatCanSee))
        // {
        //     // if (hit.collider.gameObject == CharacterController2D.myPlayer.gameObject)
        //     // {
        //         Debug.Log("i can see");
        //     // }
        // }
        if (Physics2D.Raycast(transform.position, Vector2.left, SightRange, WhatCanSee))
        {
            Debug.Log("i see you");
        }
        

    }

    // Start is called before the first frame update
    // void Start()
    // {
    //     Debug.Log("hi");
    // }

    // Update is called once per frame
    void Update()
    { 
        CanSeePlayer();
    }

    protected float DistanceToPlayer()
    {
        return Vector3.Distance(transform.position, CharacterController2D.myPlayer.transform.position);
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, transform.TransformDirection(Vector3.left) * SightRange);
        Gizmos.color = Color.blue;
    }
}
