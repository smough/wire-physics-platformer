using UnityEngine;
using System.Collections;

public class TestUserControl : MonoBehaviour {

	[SerializeField] private float m_MaxSpeed = 10f;
	[SerializeField] private float m_JumpForce = 500f;
	[SerializeField] private LayerMask m_WhatIsGround;

	private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	private bool m_FacingRight = true;
	private Rigidbody2D m_Rigidbody2D;
	//private LayerMask ground = "Default";

	// Use this for initialization
	void Start () 
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector2 pos = (Vector2)gameObject.transform.position;

		Collider2D[] colliders = Physics2D.OverlapCircleAll (pos, k_GroundedRadius);
		foreach(Collider2D collider in colliders)
		{
			if(collider.gameObject != gameObject)
			{
				m_Grounded = true;
			}
		}

		var move = Input.GetAxis ("Horizontal");

		if (move > 0 && !m_FacingRight) {
			Flip();
		} else if (move < 0 && m_FacingRight) {
			Flip();
		}

		m_Rigidbody2D.velocity  = new Vector2 (move * m_MaxSpeed, m_Rigidbody2D.velocity.y);

		if (m_Grounded && Input.GetButton ("Jump"))
		{
			m_Grounded = false;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
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

	public bool FacingRight()
	{
		return m_FacingRight;
	}
}
