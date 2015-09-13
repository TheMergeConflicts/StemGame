using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	private float hInput;
	private float vInput;
	private WalkMechanics walkMechanics;
	private GrabMechanics grabMechanics;

	void Update() {
		hInput = Input.GetAxisRaw ("Horizontal");
		vInput = Input.GetAxisRaw ("Vertical");

		walkMechanics.moveHorizontal (hInput);
		walkMechanics.moveVertical (vInput);

		if (Input.GetButtonDown ("Jump")) {
			grabMechanics.grab(true);
		}
		if (Input.GetButtonUp ("Jump")) {
			grabMechanics.grab (false);
		}
	}

	void Awake() {
		walkMechanics = GetComponent<WalkMechanics> ();
		grabMechanics = GetComponent<GrabMechanics> ();
	}
}
