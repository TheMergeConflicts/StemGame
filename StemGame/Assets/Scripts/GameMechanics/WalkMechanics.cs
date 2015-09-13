using UnityEngine;
using System.Collections;

public class WalkMechanics : MonoBehaviour {
	public const int NORTH = 0;
	public const int SOUTH = 1;
	public const int EAST = 2;
	public const int WEST = 3;

	public float speed = 5;
	public float walkSmoothing = 5;
	public int direction = NORTH;

	private float horizontalInput;
	private float verticalInput;
	private GrabMechanics grabMechanics;
	private Rigidbody2D rigid;



	void Awake() {
		rigid = GetComponent<Rigidbody2D> ();
		grabMechanics = GetComponent<GrabMechanics> ();
	}

	void Update() {
		updateMovement ();
		updateDirection ();
		updateRotation ();
	}

	void updateDirection() {
		if (grabMechanics.getIsGrabbing ()) {
			return;
		}
		if (Mathf.Abs (horizontalInput) > 0 && Mathf.Abs (verticalInput) == 0) {
			if (horizontalInput < 0) {
				direction = WEST;
			}
			else {
				direction = EAST;
			}
		}

		if (Mathf.Abs (verticalInput) > 0 && Mathf.Abs (horizontalInput) == 0) {
			if (verticalInput < 0) {
				direction = SOUTH;
			} else {
				direction = NORTH;
			}
		}
	}



	public void moveHorizontal (float hInput) {
		this.horizontalInput = hInput;
	}

	public void moveVertical (float vInput) {
		this.verticalInput = vInput;
	}

	void updateRotation() {
		switch (direction) {
		case NORTH: 
			transform.rotation = Quaternion.Euler(0, 0, 0);
			break;

		case SOUTH:
			transform.rotation = Quaternion.Euler(0, 0, 180);
			break;

		case WEST:
			transform.rotation = Quaternion.Euler(0, 0, 90);
			break;

		case EAST:
			transform.rotation = Quaternion.Euler(0, 0, 270);
			break;


		}
	}

	void updateMovement() {
		float scale = Mathf.Max (Mathf.Abs(horizontalInput), Mathf.Abs(verticalInput));
		Vector2 unitVec = new Vector2 (horizontalInput, verticalInput).normalized;
		Vector2 goalVec = unitVec * speed * scale;

		rigid.velocity = Vector2.MoveTowards (rigid.velocity, goalVec, Time.deltaTime * walkSmoothing);

	}


}
