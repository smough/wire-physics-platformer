using UnityEngine;
using System.Collections;

public class Wire : MonoBehaviour 
{
	[SerializeField] private float m_WireForce = 70f;
	[SerializeField] private float m_WireLength = 100f;
	[SerializeField] private float m_FiringDelay = 300f;
	[SerializeField] private LayerMask m_AttachesToWhat;

	private Rigidbody2D m_RigidBody2D;

	// Use this for initialization
	void Start () 
	{
		m_RigidBody2D = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		Debug.Log (gameObject.GetComponent<TestUserControl>().FacingRight ());

		if(Input.GetButton("Fire1"))
		{
			//Debug.Log("You pressed the right button!");
			if(gameObject.GetComponent<TestUserControl>().FacingRight())
			{
				RaycastHit2D ray = Physics2D.Raycast((Vector2)transform.position, Vector2.right, m_WireLength, m_AttachesToWhat);
				if(ray.collider != null)
				{
					//m_RigidBody2D.MovePosition(ray.point);
					m_RigidBody2D.AddForce(new Vector2(m_WireForce, 0));
					Debug.Log (m_RigidBody2D.position);
					Debug.DrawLine(transform.position, (Vector3)ray.point);
				}
			}
			else
			{
				RaycastHit2D ray = Physics2D.Raycast((Vector2)transform.position, Vector2.left, m_WireLength, m_AttachesToWhat);
				if(ray.collider != null)
				{
					m_RigidBody2D.AddForce(new Vector2(-m_WireForce, 0));
					Debug.Log (m_RigidBody2D.position);
					Debug.DrawLine(transform.position, (Vector3)ray.point);
				}
			}
		}
	}
}
