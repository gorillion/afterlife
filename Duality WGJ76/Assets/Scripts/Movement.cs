using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

	public bool isGrounded;

    [SerializeField]
    private float moveSpeed;
	[SerializeField]
	private float maxSpeed;
	[SerializeField]
	private float jumpForce;
	[SerializeField]
    private bool dead;
	[SerializeField]
	//private LayerMask thisObjectMask;

	private PhysicsMaterial2D physicsMat;
    public Rigidbody2D rb2d;

	void Start () {
        rb2d = GetComponent<Rigidbody2D> ();
		physicsMat = rb2d.sharedMaterial;
	}
	
	void FixedUpdate () {
		if (Mathf.Abs (rb2d.velocity.x) > maxSpeed) {
			if (Mathf.Abs (rb2d.velocity.y) > maxSpeed) {
				rb2d.velocity = new Vector2 (rb2d.velocity.x * 0.96f, rb2d.velocity.y * 0.975f);
			} else {
				rb2d.velocity = new Vector2 (rb2d.velocity.x * 0.96f, rb2d.velocity.y);
			}
		} else if (Mathf.Abs (rb2d.velocity.y) > maxSpeed) {
			rb2d.velocity = new Vector2 (rb2d.velocity.x, rb2d.velocity.y * 0.975f);
		}
	}

	//void Update () {
	
	//}
    
    public void Move (Vector2 dir) {
        if (dead) {
            rb2d.AddForce (dir * moveSpeed);
        } else {
            rb2d.AddForce (new Vector2 (dir.x, 0f) * moveSpeed);
        }
        
    }

	public void Jump () {
		if (!dead) {
			if (isGrounded) {
				rb2d.AddForce (Vector2.up * jumpForce);
				isGrounded = false;
			}
		}
	}

	public void Bounce () {
		rb2d.velocity = new Vector2 (rb2d.velocity.x, 0f);
		rb2d.AddForce (Vector2.up * (jumpForce * 1.1f));
		isGrounded = false;
	}

	public void DampVelocity () {
		rb2d.velocity = new Vector2 (rb2d.velocity.x * 0.85f, rb2d.velocity.y);
	}

	public void DampAllVelocity () {
		rb2d.velocity = new Vector2 (rb2d.velocity.x * 0.85f, rb2d.velocity.y * 0.85f);
	}

	public void OnCollisionEnter2D (Collision2D col) {

		if (!isGrounded) {

			ContactPoint2D[] contacts = new ContactPoint2D[5];

			for (int i = 0; i < col.GetContacts(contacts); i++) {
				if (col.GetContact(i).normal.y > 0.95f) {
					isGrounded = true;
				}
			}
		}
	}

	public void OnCollisionStay2D (Collision2D col) {

		if (!isGrounded) {

			ContactPoint2D[] contacts = new ContactPoint2D[5];

			for (int i = 0; i < col.GetContacts (contacts); i++) {
				if (col.GetContact (i).normal.y > 0.95f) {
					isGrounded = true;
				}
			}
		}
	}

	public void OnCollisionExit2D (Collision2D col) {

		if (isGrounded) {

			ContactPoint2D[] contacts = new ContactPoint2D[5];

			if (col.GetContacts(contacts) <= 0) {
				isGrounded = false;
			}

		}

	}

	public void ChangeFriction (float newFriction) {
		physicsMat.friction = newFriction;
	}

}
