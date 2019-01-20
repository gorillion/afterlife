using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public GameObject[] detectors;
	public LayerMask groundMask;
	public GameObject deathParticles;

	private Movement movement;
	private SpriteRenderer sr;
	private Animator anim;
	private SoundPlayer soundPlayer;

	private Vector2 currentDir;
	private float stateTime;
	private float stateTimer;
	private bool moving;
	private bool facingLeft;
	private bool newStatePending;

	private bool bouncedOn;

	public enum EnemyStates {

		Idle,
		Moving,
		Attacking

	}

	public EnemyStates enemyState;

    void Start() {
		movement = GetComponent<Movement> ();
		sr = GetComponent<SpriteRenderer> ();
		anim = GetComponent<Animator> ();
		soundPlayer = GetComponent<SoundPlayer> ();
		stateTime = 5f;
    }

    void Update() {
        
		if (!moving) {
			movement.DampVelocity ();
		} else {
			CheckForGround ();
		}

		CurrentState ();

		anim.SetBool ("Moving", movement.rb2d.velocity.magnitude > 0.25f);

    }

	void NewState (EnemyStates newEnemyState) {
		enemyState = newEnemyState;
		newStatePending = false;

		if (newEnemyState == EnemyStates.Idle) {
			moving = false;
			stateTime = 5f;
		}

	}

	void NewMove () {

		int rand = Random.Range (0, 2);
		float randMoveTime = Random.Range (3f, 5f);

		switch (rand) {

			case 0:
				currentDir = Vector2.left;
				break;

			case 1:
				currentDir = Vector2.right;
				break;

		}

		stateTime = randMoveTime;
		moving = true;

	}

	void DirectedMove (Vector2 dir) {
		currentDir = dir;
		moving = true;
	}

    void CurrentState() {

		if (stateTimer < stateTime) {
			stateTimer += Time.deltaTime;
		} else if (stateTimer > stateTime) {
			newStatePending = true;
			stateTimer = 0f;
		}

		switch (enemyState) {

			case EnemyStates.Idle:

				if (newStatePending) {
					NewState(EnemyStates.Moving);
				}

				break;

			case EnemyStates.Moving:

				if (!moving) {
					NewMove();
				} else {
					movement.Move(currentDir);
					if (movement.rb2d.velocity.magnitude > 0f) {
						if (currentDir == Vector2.left) {
							if (!facingLeft) {
								facingLeft = true;
								sr.flipX = true;
							}
						} else {
							if (facingLeft) {
								facingLeft = false;
								sr.flipX = false;
							}
						}
					} else {
						if (currentDir == Vector2.left) {
							currentDir = Vector2.right;
						} else if (currentDir == Vector2.right) {
							currentDir = Vector2.left;
						}
					}
				}

				if (newStatePending) {
					moving = false;
					int rand = Random.Range(0, 2);

					switch (rand) {

						case 0:
							NewState(EnemyStates.Idle);
							break;

						case 1:
							NewState(EnemyStates.Moving);
							break;

					}
				}
				break;

			case EnemyStates.Attacking:
				break;

		}

    }

	public void OnCollisionEnter2D (Collision2D col) {
		if (col.collider.tag == "Player") {

			ContactPoint2D[] contacts = new ContactPoint2D[5];

			for (int i = 0; i < col.GetContacts(contacts); i++) {
				print(col.GetContact(i).normal.y);
				if (col.GetContact(i).collider.tag == "Player") {
					if (col.GetContact (i).normal.y < -0.95f) {
						if (!bouncedOn) {
							FindObjectOfType<PlayerController> ().BouncedOnEnemy ();
							bouncedOn = true;
							Instantiate (deathParticles, transform.position + new Vector3 (0f, 0.5f, 0f), Quaternion.identity);
							Destroy (gameObject);
						}
					}
					else if (Mathf.Approximately (col.GetContact (i).normal.y, 0f)) {
						//Destroy (col.gameObject);
						col.gameObject.GetComponent<PlayerController> ().KillPlayer ();
					}
				}
			}
		}
	}

	void CheckForGround() {

		RaycastHit2D leftHit = Physics2D.Raycast(detectors[0].transform.position, -transform.up, 0.55f, groundMask);
		RaycastHit2D rightHit = Physics2D.Raycast(detectors[1].transform.position, -transform.up, 0.55f, groundMask);

		Debug.DrawLine (detectors[0].transform.position, detectors[0].transform.position - new Vector3 (0f, 0.55f, 0f));
		Debug.DrawLine (detectors[1].transform.position, detectors[1].transform.position - new Vector3 (0f, 0.55f, 0f));

		if (currentDir == Vector2.left) {
			if (leftHit) {
				if (leftHit.collider.tag != "Level") {
					//DirectedMove (-currentDir);
				}
			} else {
				DirectedMove (-currentDir);
			}
		} else if (currentDir == Vector2.right) {
			if (rightHit) {
				if (rightHit.collider.tag != "Level") {
					//DirectedMove (-currentDir);
				}
			} else {
				DirectedMove (-currentDir);
			}
		}

	}
}
