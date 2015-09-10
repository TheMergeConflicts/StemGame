using UnityEngine;
using System.Collections;

public class WalkMechanics : MonoBehaviour {
	public float speed = 5;
	public float walkSmoothing = 5;

	private float horizontalInput;
	private float verticalInput;
	private Rigidbody2D rigid;

	void Awake() {
		rigid = GetComponent<Rigidbody2D> ();
	}

	void Update() {
		updateMovement ();
		updateRotation ();
	}



	public void moveHorizontal (float hInput) {
		this.horizontalInput = hInput;
	}

	public void moveVertical (float vInput) {
		this.verticalInput = vInput;
	}

	void updateRotation() {
		if (Mathf.Abs (horizontalInput) < .05f && Mathf.Abs (verticalInput) < .05f) {
			return;
		}
		float degree = Mathf.Atan2 (-horizontalInput, verticalInput) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler (0, 0, degree);
	}

	void updateMovement() {
		float scale = Mathf.Max (Mathf.Abs(horizontalInput), Mathf.Abs(verticalInput));
		Vector2 unitVec = new Vector2 (horizontalInput, verticalInput).normalized;
		Vector2 goalVec = unitVec * speed * scale;

		rigid.velocity = Vector2.MoveTowards (rigid.velocity, goalVec, Time.deltaTime * walkSmoothing);

	}


}
