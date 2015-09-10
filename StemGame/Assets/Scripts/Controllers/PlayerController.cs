using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	private float hInput;
	private float vInput;
	private WalkMechanics walkMechanics;

	void Update() {
		hInput = Input.GetAxisRaw ("Horizontal");
		vInput = Input.GetAxisRaw ("Vertical");

		walkMechanics.moveHorizontal (hInput);
		walkMechanics.moveVertical (vInput);
	}

	void Awake() {
		walkMechanics = GetComponent<WalkMechanics> ();
	}
}
