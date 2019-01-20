using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public GameObject ghostCharacter;

    private Movement movement;
	private Animator anim;
	private SpriteRenderer sr;
	private SoundPlayer soundPlayer;

	[SerializeField]
	private bool dead;
	private bool ghostSpawned;
	private bool facingLeft;
	private bool fadeIn;
	private bool fadeOut;
	private float fadeLerp;

	[SerializeField]
	private Color[] fadeAlphas;

	private CameraFollow cameraFollow;

	public enum PlayerTypes {

		Human,
		Ghost

	}

	public PlayerTypes playerType;

	void Start () {
        movement = GetComponent<Movement> ();
		anim = GetComponent<Animator> ();
		sr = GetComponent<SpriteRenderer> ();
		cameraFollow = FindObjectOfType<CameraFollow> ();
		soundPlayer = GetComponent<SoundPlayer> ();
		facingLeft = true;

		if (playerType == PlayerTypes.Ghost) {
			FadeInGhost ();
		}

	}
	
	void Update () {

		if (!fadeOut) {
			Inputs ();
		} else {
			movement.DampAllVelocity ();
		}

		Animations ();
		if (!ghostSpawned) {
			if (dead) {
				if (movement.rb2d.velocity.magnitude <= 0.5f) {
					SpawnGhost ();
				}
			}
		}


		if (fadeIn) {
			fadeLerp += Time.deltaTime;
			sr.color = Color.Lerp (fadeAlphas[0], fadeAlphas[1], Mathf.SmoothStep(0f, 1f, fadeLerp));

			if (fadeLerp >= 1f) {
				fadeIn = false;
			}
		}

		if (fadeOut) {
			fadeLerp += Time.deltaTime;
			sr.color = Color.Lerp (fadeAlphas[1], fadeAlphas[0], Mathf.SmoothStep (0f, 1f, fadeLerp));

			if (fadeLerp >= 1f) {
				fadeOut = false;
			}
		}

	}

	void Animations () {
		//anim.SetFloat ("moveSpeed", movement.rb2d.velocity.magnitude);
		if (playerType == PlayerTypes.Human) {
			anim.SetBool ("Jumping", !movement.isGrounded);
			anim.SetFloat ("moveSpeed", movement.rb2d.velocity.magnitude);
		} else if (playerType == PlayerTypes.Ghost) {
			anim.SetFloat ("moveSpeed", Mathf.Abs (movement.rb2d.velocity.x));
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
					if (movement.isGrounded) {
						soundPlayer.PlayJumpNoise ();
					}
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
			} else {
				if (movement.isGrounded)
				movement.DampVelocity ();
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

	public void BouncedOnEnemy () {
		movement.Bounce();
		soundPlayer.PlayImpactNoise ();
	}

	public void KillPlayer () {

		if (facingLeft) {
			movement.rb2d.AddForce (new Vector2 (350f, 350f));
		} else {
			movement.rb2d.AddForce (new Vector2 (-350f, 350f));
		}
		movement.isGrounded = false;

		dead = true;
		anim.SetBool ("Dead", true);
		gameObject.layer = 10;
		soundPlayer.PlaySound ();

	}

	void SpawnGhost () {
		GameObject ghost = Instantiate (ghostCharacter, transform.position + new Vector3 (0f, -0.25f, 0f), Quaternion.identity) as GameObject;
		cameraFollow.UpdateCurrentPlayerObject (ghost);
		ghostSpawned = true;
		this.gameObject.GetComponent<PlayerController> ().enabled = false;
	}

	void FadeInGhost () {
		fadeLerp = 0f;
		fadeIn = true;
	}

	public void FadeOutPlayer () {
		fadeLerp = 0f;
		fadeOut = true;
	}

}
