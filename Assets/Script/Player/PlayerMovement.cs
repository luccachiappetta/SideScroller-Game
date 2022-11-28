using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	[SerializeField] CharacterController2D controller;
	// private Animator animate;

	public float runSpeed = 40f;

	float m_HorizontalMove = 0f;
	bool m_Jump = false;
	bool m_Crouch = false;
	bool m_Slide = false;
	private bool m_Dash = false;
	private bool m_Attack = false;

	private string[] AttackAnimations = new string[] { "Attack1", "Attack2", "Attack3" };
	private string CurrentAttack;
	private int AttackIndex;

	private float elaspedTime = 0;

	private void Start()
	{
		Physics2D.IgnoreLayerCollision(6,7, true);
		Physics2D.IgnoreLayerCollision(6,8, true);
		AttackIndex = 0;
		CurrentAttack = AttackAnimations[AttackIndex];
	}

	// Update is called once per frame
    void Update ()
    {
	    elaspedTime += Time.deltaTime;
	    
	    //move
		m_HorizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
		controller.animate.SetFloat("speed", Mathf.Abs(m_HorizontalMove));

	    //jump
		if (Input.GetButtonDown("Jump"))
		{
			Debug.Log("jump");
			
			controller.animate.SetBool("isJump", true);
			
			m_Jump = true;
		}

		if (Input.GetKeyDown(KeyCode.Q))
		{
			m_Dash = true;
		}

		//attack
		if (Input.GetButtonDown("Attack"))
		{
			CurrentAttack = AttackAnimations[AttackIndex];
			controller.animate.SetTrigger(CurrentAttack);
			m_Attack = true;

			AttackIndex++;
			elaspedTime = 0;
		}
		
		//crouch
		if (Input.GetButtonDown("Crouch"))
		{
			m_Crouch = true;
		} else if (Input.GetButtonUp("Crouch"))
		{
			m_Crouch = false;
		}

		LoopAttack();
    }

    void FixedUpdate ()
	{
		// Move our character
		if(!m_Attack)
			controller.Move(m_HorizontalMove * Time.fixedDeltaTime, m_Crouch, m_Jump, m_Dash);
		
		controller.Attack(m_Attack);

		m_Jump = false;
		m_Dash = false;
		m_Attack = false;
    }
    private void LoopAttack()
    {
	    if (AttackIndex == 3)
	    {
		    AttackIndex = 0;
	    }

	    if (elaspedTime > 1)
	    {
		    AttackIndex = 0;
	    }
    }

	public void OnLanding()
    {
	    controller.animate.SetBool("isJump", false);
	    controller.animate.SetBool("isFalling", false);
    }

	public void OnFalling()
	{
		controller.animate.SetBool("isFalling", true);
	}

	public void  OnCrouch(bool isCrouch)
    {
	    controller.animate.SetBool("isCrouch", isCrouch);
    }

	public void onWallSlide(bool isWall)
	{
		controller.animate.SetBool("isWallSlide", isWall);
	}
	
}
