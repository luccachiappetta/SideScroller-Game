using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemies : MonoBehaviour, IDamagable
{
    // public CharacterController2D EnemyController;
    
    protected float e_HEALTH;
    protected float e_DAMAGE;
    protected float e_SPEED;

    [SerializeField] protected LayerMask WhatCanSee;

    protected float AttackRange;
    protected float SightRange;

    [SerializeField] protected Animator animate;
    protected Rigidbody2D e_RigidBody;

    private void Awake()
    {
        // e_RigidBody = this.GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate()
    {
        // if (CanSeePlayer())
        // {
        //     RunToPlayer();
        //     AttackPlayer();
        // }
    }

    public virtual bool CanSeePlayer()
    {
        if (Physics2D.Raycast(transform.position, Vector2.left, SightRange, WhatCanSee))
        {
            return true;
            // Debug.Log("i see you");
        }
        else
        {
            return false;
        }
    }

    public virtual void AttackPlayer()
    {
        if (Physics2D.OverlapCircle(transform.position, AttackRange))
        {
            animate.SetTrigger("Attack");
        }
    }

    protected void WalkToPlayer()
    {
        Debug.Log("Walk");
    }

    public void Damage(float DamageAmount)
    {
        e_HEALTH -= DamageAmount;
        Debug.Log("hit enemy");
        animate.SetTrigger("Hit");

        if (e_HEALTH < 1)
        {
            animate.SetTrigger("Death");
            StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, SightRange);
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }
    
}
