using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public GrabMechanics grabMechanics;
    public TempManager tempManager;
    private float hInput;
	private float vInput;
	private WalkMechanics walkMechanics;

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

        if (Input.GetButton("TempUp"))
        {
            tempManager.increaseTemp();
        }
        if (Input.GetButton("TempDown"))
        {
            tempManager.decreaseTemp();
        }
	}

	void Awake() {
		walkMechanics = GetComponent<WalkMechanics> ();
		//grabMechanics = GetComponent<GrabMechanics> ();
	}
}
