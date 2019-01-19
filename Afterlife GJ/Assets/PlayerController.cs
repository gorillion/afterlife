using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Movement movement;
	private Animator anim;
	private SpriteRenderer sr;

	[SerializeField]
	private bool dead;
	private bool facingLeft;

	public enum PlayerTypes {

		Human,
		Ghost

	}

	public PlayerTypes playerType;

	void Start () {
        movement = GetComponent<Movement> ();
		anim = GetComponent<Animator> ();
		sr = GetComponent<SpriteRenderer> ();
		facingLeft = true;
	}
	
	void Update () {

        Inputs();
		Animations ();

	}

	void Animations () {
		anim.SetFloat ("moveSpeed", movement.rb2d.velocity.magnitude);
		if (playerType == PlayerTypes.Human) {
			anim.SetBool ("Jumping", !movement.isGrounded);
		}
	}

	void Inputs() {

		if (playerType == PlayerTypes.Human) {
			if (!dead) {
				if (Input.GetKey (KeyCode.D)) {
					movement.Move (Vector2.right);

					if (facingLeft) {
						facingLeft = false;
						sr.flipX = false;
					}
				}

				if (Input.GetKey (KeyCode.A)) {
					movement.Move (Vector2.left);

					if (!facingLeft) {
						facingLeft = true;
						sr.flipX = true;
					}
				}

				if (Input.GetKeyDown (KeyCode.Space)) {
					movement.Jump ();
				}

				if (movement.isGrounded) {
					if (Mathf.Approximately (Input.GetAxis ("Horizontal"), 0f)) {
						movement.ChangeFriction (1f);
						movement.DampVelocity ();
					}
					else {
						movement.ChangeFriction (0f);
					}
				}
				else {
					movement.ChangeFriction (0f);
				}
			}
		} else {
			if (Input.GetKey (KeyCode.D)) {
				movement.Move (Vector2.right);

				if (facingLeft) {
					facingLeft = false;
					sr.flipX = false;
				}
			}

			if (Input.GetKey (KeyCode.A)) {
				movement.Move (Vector2.left);

				if (!facingLeft) {
					facingLeft = true;
					sr.flipX = true;
				}
			}

			if (Input.GetKey (KeyCode.W)) {
				movement.Move (Vector2.up);
			}

			if (Input.GetKey (KeyCode.S)) {
				movement.Move (Vector2.down);
			}

			if (Mathf.Approximately (Input.GetAxis ("Horizontal"), 0f) && Mathf.Approximately (Input.GetAxis ("Vertical"), 0f)) {
				movement.DampAllVelocity ();
			}
		}

    }

}
