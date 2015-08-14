using UnityEngine;
using System.Collections;

/* When the wire hits something, a player should be able to
 * * Pull themselves to the point of impact (implemented)
 * * Drag the object/enemy to them
 * * Do the sterotypical swinging thing and change the length of the wire while swinging, you know, THAT
 * ****************************************************************************************************
 * TODO
 * allow for adjusting the length of the wire
 * implement pulling
 * implement swinging
 * change the "zip to" mechanic to work a bit slower (define a force to apply to the player, this will also make the player not drop like a rock when the hook is destroyed)
 * render the wire with a simple line or somesuch
 * ****************************************************************************************************
 */

public class Wire : MonoBehaviour 
{
	[SerializeField] private float m_WireForce = 70f;
	[SerializeField] private float m_WireLength = 100f;
	[SerializeField] private float m_FiringDelay = 300f;
	[SerializeField] private LayerMask m_AttachesToWhat;

	private Rigidbody2D m_RigidBody2D;
	private DistanceJoint2D m_Wire;

	// Use this for initialization
	void Start () 
	{
		m_RigidBody2D = GetComponent<Rigidbody2D> ();
		m_Wire = GetComponent<DistanceJoint2D> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		//use player's "look" to aim the wire (this might get changed if I think it feels too awkward to control)
		Vector2 aim = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

		//to drag a player to the point of hook impact, just shrink the joint size to 0 (Got this done)
		if(Input.GetButtonDown("Fire1"))
		{
			//shoot out a raycast at the player's position towards where they are looking
			RaycastHit2D collision = Physics2D.Raycast(transform.position, aim);
			Debug.DrawLine(transform.position, collision.point, Color.red);

			//destroy an hold hook when the fire button is pressed again
			if(gameObject.GetComponent<DistanceJoint2D>() != null)
			{
				Destroy(gameObject.GetComponent<DistanceJoint2D>());
			}
			else
			{
				//if the hook hits something drag the player to it
				if(collision.collider != null)
				{
					DistanceJoint2D newJoint = gameObject.AddComponent<DistanceJoint2D>();
					newJoint.connectedAnchor = collision.point;
					newJoint.distance = 0;
					newJoint.connectedBody = collision.collider.gameObject.AddComponent<Rigidbody2D>();
					Debug.Log(collision.point);
				}
			}
		}
	}
}
