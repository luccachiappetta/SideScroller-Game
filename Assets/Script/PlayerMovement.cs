using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public CharacterController2D controller;
	public Animator animate;

    public float runSpeed = 40f;

	float m_HorizontalMove = 0f;
	bool m_Jump = false;
	bool m_Crouch = false;

    // Update is called once per frame
    void Update () {

		m_HorizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		animate.SetFloat("speed", Mathf.Abs(m_HorizontalMove));

		if (Input.GetButtonDown("Jump"))
		{
			Debug.Log("jump");
			
			animate.SetBool("isJump", true);
			m_Jump = true;
		}

		if (Input.GetButtonDown("Crouch"))
		{
			m_Crouch = true;
		} else if (Input.GetButtonUp("Crouch"))
		{
			m_Crouch = false;
		}

	}

	void FixedUpdate ()
	{
		// Move our character
		controller.Move(m_HorizontalMove * Time.fixedDeltaTime, m_Crouch, m_Jump);
        m_Jump = false;
    }

	public void OnLanding()
    {
		animate.SetBool("isJump", false);
		animate.SetBool("isFalling", false);
    }

	public void OnFalling()
	{
		animate.SetBool("isFalling", true);
	}

	public void  OnCrouch(bool isCrouch)
    {
		animate.SetBool("isCrouch", isCrouch);
    }
}
