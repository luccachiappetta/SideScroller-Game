using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore;

public class CharacterController2D : MonoBehaviour
{
    //Jumping
    [SerializeField] private float m_JumpForce = 400f;
    [SerializeField] private bool m_AirControl = false;
    private bool m_isFalling;
    
    //Movement
    [Range(0, .3f)][SerializeField] private float m_MovementSmoothing = .05f;
    private bool m_FacingRight = true;
    private Vector3 m_Velocity = Vector3.zero;
    private Rigidbody2D m_Rigidbody2D;
  
    //Ground Checks
    [SerializeField] private LayerMask m_WhatIsGround;
    [SerializeField] private Transform m_GroundCheck;
    private bool m_Grounded;
    
    const float k_GroundedRadius = .2f;
    const float k_CeilingRadius = .2f;

    //Crouch Check
    [SerializeField] private Transform m_CeilingCheck;
    [SerializeField] private Collider2D m_CrouchDisableCollider;
    [Range(0, 1)][SerializeField] private float m_CrouchSpeed = .36f;
    [SerializeField] private float m_SlideSpeed = 2f;
    
    //Player info
    public static CharacterController2D myPlayer;
    public Vector3 lastPos
    {
        get
        {
            return lastPos;
        }
        set
        {
            lastPos = value;
        }
    }
    
    //Events
    [Header("Events")]
    [Space]
    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    public BoolEvent OnCrouchEvent;
    private bool m_wasCrouching = false;
    
    public UnityEvent OnFallEvent;

    private void Awake()
    {
        myPlayer = this;

        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        OnLandEvent ??= new UnityEvent();

        OnCrouchEvent ??= new BoolEvent();
        
        if (OnCrouchEvent == null)
            OnFallEvent = new UnityEvent();
    }

    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;
        
        //Ground Check
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        // Gizmos.DrawSphere(m_GroundCheck.position, k_GroundedRadius);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }

        // Fall Check
        if (!m_Grounded && m_Rigidbody2D.velocity.y <= 0)
        {
            // Debug.Log("Falling!!");
            OnFallEvent.Invoke();
        }
    }


    public void Move(float move, bool crouch, bool jump)
    {
        // If crouching, check to see if the character can stand up
        if (!crouch)
        {
            // If the character has a ceiling preventing them from standing up, keep them crouching
            if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
            {
                crouch = true;
            }
        }

        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
        {

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

            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
        }
        // If the player should jump...
        if (m_Grounded && jump)
        {
            // Add a vertical force to the player.
            m_Grounded = true;
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        }
    }

    public void Attack()
    {
        if (!m_Grounded)
        {
            //attack 1
        }
        else if (m_Grounded)
        {
            //air attack
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
}