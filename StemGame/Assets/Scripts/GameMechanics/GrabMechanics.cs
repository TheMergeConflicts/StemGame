using UnityEngine;
using System.Collections;

public class GrabMechanics : MonoBehaviour {
	public string grabTag = "Grabbable";
	public float grabSpeed;

	private Vector3 grabOffset;
	private bool isGrabbing;
	private Transform grabbableBlock; //The current block that can be grabbed


	void FixedUpdate() {
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
            grabbableBlock.GetComponent<GrabbedBehavior>().setGrabMechanics(this);
            //grabbableBlock.parent = this.transform;
		} else {
			if(grabbableBlock!= null){
				grabbableBlock.GetComponent<ElementBehavior>().setIsGrabbed(false);
                //grabbableBlock.parent = null;
			}
			isGrabbing = false;

		}
	}

	void OnTriggerEnter2D (Collider2D collider) {
		if (collider.tag == grabTag) {
            
			//Only allows the player to grab an element if it's solid -Nick S
			if(collider.GetComponent<ElementBehavior>().getCurState() == 0){
				grabbableBlock = collider.GetComponent<Transform>();
			}
		}
	}

	void OnTriggerExit2D (Collider2D collider) {
		if (collider.tag == grabTag) {
            if (grabbableBlock != null)
            {
                grabbableBlock.parent = null;
            }
            grabbableBlock = null;
		}
	}

	public bool getIsGrabbing() {
		return isGrabbing;
	}
}
