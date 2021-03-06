﻿using UnityEngine;
using System.Collections;

public class WalkMechanics : MonoBehaviour {
	public const int NORTH = 0;
	public const int SOUTH = 1;
	public const int EAST = 2;
	public const int WEST = 3;

    public Animator animator;
    public GrabMechanics grabMechanics;
    public float speed = 5;
	public float walkSmoothing = 5;
	public int direction = NORTH;
    public bool isFrozen;

	private float horizontalInput;
	private float verticalInput;
	private Rigidbody2D rigid;
	private int animState;

	void Awake() {
		rigid = GetComponent<Rigidbody2D> ();
		//grabMechanics = GetComponent<GrabMechanics> ();
		//animator = GetComponent<Animator> ();
		animState = 0;
	}

	void Update() {
		updateMovement ();
		updateDirection ();
	}

    /// <summary>
    /// Based on the direction of the player's input, the direction of the character will be updated
    /// </summary>
	void updateDirection() {
        if (grabMechanics.getIsGrabbing())
        {
            return;
        }
		if (Mathf.Abs (horizontalInput) > 0 && Mathf.Abs (verticalInput) == 0) {
			if (horizontalInput < 0) {
				animState = 1;
				direction = WEST;
			}
			else {
				animState = 1;
				direction = EAST;
			}
		}

		if (Mathf.Abs (verticalInput) > 0 && Mathf.Abs (horizontalInput) == 0) {
			if (verticalInput < 0) {
				animState = 0;
				direction = SOUTH;
			} else {
				direction = NORTH;
				animState = 2;
			}
		}
	}



	public void moveHorizontal (float hInput) {
		this.horizontalInput = hInput;
	}

	public void moveVertical (float vInput) {
		this.verticalInput = vInput;
	}

    /// <summary>
    /// Based on the inputs of the player, the velocity of the character will be updated
    /// </summary>
	void updateMovement() {
		float scale = Mathf.Max (Mathf.Abs(horizontalInput), Mathf.Abs(verticalInput));
		Vector2 unitVec = new Vector2 (horizontalInput, verticalInput).normalized;
		Vector2 goalVec = unitVec * speed * scale;

		rigid.velocity = Vector2.MoveTowards (rigid.velocity, goalVec, Time.deltaTime * walkSmoothing);
		if (animator) { 
			if (Mathf.Abs(rigid.velocity.x) > 0.5f || Mathf.Abs(rigid.velocity.y) > 0.5f && animState < 3) {
				animState += 10;
			} else {
				if (animState > 2) {
					animState -= 10;
				}
			}
			animator.SetInteger ("AnimState", animState);
		}
	}


}
