using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore;

public class CharacterController2D : MonoBehaviour
{
    public Animator animate;
    
    //Jumping
    [SerializeField] private float m_JumpForce = 2;
    [SerializeField] private float m_FallForce = 2.5f;
    [SerializeField] private bool m_AirControl = false;
    private bool m_isFalling;

    //Movement
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
    private bool m_FacingRight = true;
    private Vector3 m_Velocity = Vector3.zero;
    private Rigidbody2D m_Rigidbody2D;

    //Ground Checks
    [SerializeField] private LayerMask m_WhatIsGround;

    private bool m_Grounded;

    const float k_GroundedRadius = .2f;
    const float k_CeilingRadius = .2f;
    private const float k_SideRadius = .3f;

    //Collision Check
    [SerializeField] private Transform m_SideCheck;
    [SerializeField] private Transform m_CeilingCheck;
    [SerializeField] private Transform m_GroundCheck;
    [SerializeField] private Transform m_AttackCheck;

    //Particles
    [SerializeField] ParticleSystem partJump;
    [SerializeField] ParticleSystem partLand;
    [SerializeField] private ParticleSystem partDash;

    //Crouch Check
    [SerializeField] private Collider2D m_CrouchDisableCollider;
    [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;
    [SerializeField] private float m_SlideSpeed = 2f;

    //Player info
    public static CharacterController2D myPlayer;
    [SerializeField] private LayerMask canHit;
    
    //Attack
    private float attackRange = 0.75f;
    [Range(1,20)] [SerializeField] private float PlayerDamage = 10f;

    //Events
    [Header("Events")] [Space] public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool>
    {
    }

    public BoolEvent OnCrouchEvent;
    private bool m_wasCrouching = false;

    public UnityEvent OnFallEvent;
    public BoolEvent onWallEvent;
    private bool wall = false;

    private bool canDash;
    


    private void Awake()
    {
        myPlayer = this;

        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        OnLandEvent ??= new UnityEvent();
        OnCrouchEvent ??= new BoolEvent();
        OnFallEvent ??= new UnityEvent();
        onWallEvent ??= new BoolEvent();
        if (OnCrouchEvent == null)
            OnFallEvent = new UnityEvent();
    }

    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;
        m_Rigidbody2D.drag = 0;

        //Ground Check
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                if (!wasGrounded)
                {
                    canDash = true;
                    partLand.Play();
                    OnLandEvent.Invoke();
                }
            }
        }
    }


    public void Move(float move, bool crouch, bool jump, bool dash)
    {
        // Crouch
        if (!crouch)
        {
            if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
            {
                crouch = true;
            }
        }

        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
        {
            //Wall
            if (Physics2D.OverlapCircle(m_SideCheck.position, k_SideRadius, m_WhatIsGround))
            {
                Debug.Log("onWall");
                wall = true;

                m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, -0.5f);
            }
            else
            {
                wall = false;
            }

            onWallEvent.Invoke(wall);


            // If crouching
            if (crouch)
            {
                if (!m_wasCrouching)
                {
                    m_wasCrouching = true;
                    OnCrouchEvent.Invoke(true);
                }

                // Reduce the speed by the crouchSpeed multiplier
                move *= m_CrouchSpeed;

                // Disable one of the colliders when crouching
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = false;
            }
            else
            {
                // Enable the collider when not crouching
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = true;

                if (m_wasCrouching)
                {
                    m_wasCrouching = false;
                    OnCrouchEvent.Invoke(false);
                }
            }

            // Move
            Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);

            // Move Smooth
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity,
                m_MovementSmoothing);

            // Flip
            if (move > 0 && !m_FacingRight)
            {
                Flip();
            }
            else if (move < 0 && m_FacingRight)
            {
                Flip();
            }
        }

        // Should Jump
        if ((wall || m_Grounded) && jump)
        {
            //Jump
            partJump.Play();
            if (wall)
            {
                m_Rigidbody2D.AddForce(new Vector2(600f * -transform.localScale.x, m_JumpForce));
            }
            else
            {
                m_Rigidbody2D.AddForce(new Vector2(0, m_JumpForce));
            }
            // m_Rigidbody2D.velocity += Vector2.up * (Physics2D.gravity.y * (m_JumpForce - 1) * Time.deltaTime);
        }

        // Falling
        if (!m_Grounded && m_Rigidbody2D.velocity.y < 0)
        {
            // Debug.Log("Falling!!");
            // m_Rigidbody2D.velocity += Vector2.up * (Physics2D.gravity.y * (m_FallForce - 1) * Time.deltaTime);
            OnFallEvent.Invoke();
        }

        if (dash)
        {
            Dash();
        }
    }

    public void Dash()
    {
        m_Rigidbody2D.velocity = Vector2.zero;
        m_Rigidbody2D.velocity += new Vector2(50f * transform.localScale.x, 10);
        m_Rigidbody2D.drag = 50f;

        partDash.Play();


        // Camera.main.transform.DoComplete
    }


    //adapted from brackeys https://www.youtube.com/watch?v=sPiVz1k-fEs
    public void Attack(bool attack)
    {
        if (attack == true)
        {
            // Debug.Log("atatack");
            Collider2D[] hits = Physics2D.OverlapCircleAll(m_AttackCheck.position, attackRange, canHit);
            
            foreach (Collider2D hit in hits)
            {
                hit.GetComponent<Enemies>().Damage(PlayerDamage);
            }
        }
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(m_GroundCheck.position, k_GroundedRadius);
        Gizmos.DrawWireSphere(m_CeilingCheck.position, k_CeilingRadius);
        Gizmos.DrawWireSphere(m_SideCheck.position, k_SideRadius);
        Gizmos.DrawWireSphere(m_AttackCheck.position, attackRange);
    }
}