using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemies : MonoBehaviour, IDamagable
{
    // public CharacterController2D EnemyController;
    
    public float e_HEALTH;
    protected float e_DAMAGE;
    protected float e_SPEED;
    public bool isDead = false;
    public bool isHit = false;

    [SerializeField] protected LayerMask WhatCanSee;

    protected float AttackRange;
    protected float SightRange;
    private Vector3 e_Velocity = Vector3.zero;

    [SerializeField] protected Animator animate;
    protected Rigidbody2D e_RigidBody;

    private void Awake()
    {
        e_RigidBody = GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate()
    {
        // if (CanSeePlayer())
        // {
        //     RunToPlayer();
        //     AttackPlayer();
        // }
        Debug.Log(GetPlayerDirX());
        // transform.localScale = new Vector3(-GetPlayerDirX(), 1f, 1f);
    }
    
    public void Idle()
    {
        // transform.localScale = new Vector3(-GetPlayerDirX(), 1f, 1f);
        animate.SetTrigger("Idle");
        Debug.Log("Turn");
    }

    public virtual bool CanSeePlayer()
    {
        if (Physics2D.OverlapCircle(transform.position, SightRange, WhatCanSee))
        {
            // animate.SetBool("Awake",true);
            return true;
        }
        return false;
    }
    
    public void RunToPlayer()
    {
        // Move
        e_RigidBody.velocity = new Vector2(e_SPEED * GetPlayerDirX(), e_RigidBody.velocity.y);
        
        //Flip
        transform.localScale = new Vector3(-GetPlayerDirX(), 1f, 1f);
        animate.SetFloat("Run",Math.Abs(e_RigidBody.velocity.x));
    }
    
    public bool CanAttack()
    {
        if (Physics2D.OverlapCircle(transform.position, AttackRange, WhatCanSee))
        {
            
            animate.SetFloat("Run",0);
            return true;
        }

        return false;
    }

    public virtual void Attack()
    {
        e_RigidBody.velocity = Vector2.zero;
        animate.SetTrigger("Attack");
        StartCoroutine(AttackDelay());

        transform.localScale = new Vector3(-GetPlayerDirX(), 1f, 1f);
    }

    protected IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(0.5f);
        if (Physics2D.OverlapCircle(transform.position, AttackRange * 1.2f, WhatCanSee))
        {
            CharacterController2D.character.Damage(e_DAMAGE);
        }
    }

    public float GetPlayerDirX()
    {
        Vector3 playerDir = CharacterController2D.character.transform.position - transform.position;
        playerDir.x /= Math.Abs(playerDir.x);
        return playerDir.x;
    }

    public void Damage(float DamageAmount)
    {
        e_HEALTH -= DamageAmount;
        Debug.Log("hit enemy");
        isHit = true;

        if (e_HEALTH < 1)
        {
            isDead = true;
        }
    }

    public void HitTrigger()
    {
        animate.SetTrigger("Hit");
        e_RigidBody.velocity = Vector2.zero;
        isHit = false;
    }

    public void DeathTrigger()
    {
        animate.SetTrigger("Death");
        StartCoroutine(Death());
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
