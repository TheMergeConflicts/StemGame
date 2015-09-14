﻿using UnityEngine;
using System.Collections;

public class GrabMechanics : MonoBehaviour {
	public string grabTag = "Grabbable";
	public float grabSpeed;

	private Vector3 grabOffset;
	private bool isGrabbing;
	private Transform grabbableBlock; //The current block that can be grabbed


	void Update() {
		//print (isGrabbing);
		if (grabbableBlock != null && isGrabbing) {
			grabbableBlock.transform.position = this.transform.position + grabOffset;
		}
	}

	public void grab(bool grabInput) {
		if (grabbableBlock != null) {
			isGrabbing = grabInput;
			grabOffset = -this.transform.position + grabbableBlock.transform.position;
			grabbableBlock.GetComponent<ElementBehavior>().setIsGrabbed(true);
		} else {
			grabbableBlock.GetComponent<ElementBehavior>().setIsGrabbed(false);
			isGrabbing = false;

		}
	}

	void OnTriggerEnter2D (Collider2D collider) {
		if (collider.tag == grabTag) {
			grabbableBlock = collider.GetComponent<Transform>();
		}
	}

	void OnTriggerExit2D (Collider2D collider) {
		if (collider.tag == grabTag) {
			grabbableBlock = null;
		}
	}

	public bool getIsGrabbing() {
		return isGrabbing;
	}
}