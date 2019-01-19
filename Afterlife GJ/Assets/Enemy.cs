using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public GameObject[] detectors;
	public LayerMask groundMask;

	private Movement movement;
	private SpriteRenderer sr;
	private Animator anim;

	private Vector2 currentDir;
	private float stateTime;
	private float stateTimer;
	private bool moving;
	private bool facingLeft;
	private bool newStatePending;

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
		stateTime = 5f;
    }

    void Update() {
        
		if (!moving) {
			movement.DampVelocity ();
	
		} else {
			CheckForGround();
		}

        CurrentState();

    }

	void NewState (EnemyStates newEnemyState) {
		enemyState = newEnemyState;
		newStatePending = false;
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
							stateTime = 5f;
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
					if (col.GetContact(i).normal.y < -0.95f) {
						// definitely kill this noob
					}
				}
			}
		}
	}

	void CheckForGround() {

		RaycastHit2D leftHit = Physics2D.Raycast(detectors[0].transform.position, -transform.up, 0.55f, groundMask);
		RaycastHit2D rightHit = Physics2D.Raycast(detectors[1].transform.position, -transform.up, 0.55f, groundMask);

		if (currentDir == Vector2.left) {
			if (!leftHit) {
				NewState(EnemyStates.Idle);
				//print(leftHit);
			}
		} else if (currentDir == Vector2.right) {
			if (!rightHit) {
				NewState(EnemyStates.Idle);
			}
		}

	}

}
