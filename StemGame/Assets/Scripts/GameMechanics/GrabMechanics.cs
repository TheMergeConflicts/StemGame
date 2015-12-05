using UnityEngine;
using System.Collections;

public class GrabMechanics : MonoBehaviour {
	public string grabTag = "Grabbable";
	public float grabSpeed;

	private Vector3 grabOffset;
	private bool isGrabbing;
	private Transform grabbableBlock; //The current block that can be grabbed


	void FixedUpdate() {
		
	}

    /// <summary>
    /// This method is used to check if the player can grab a block they are next to. If the player is in front of a block
    /// and they have pushed the grab button
    /// </summary>
    /// <param name="grabInput"></param>
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

    /// <summary>
    /// There is a trigger box attached to the front of the player. If a grabbable block is placed in it, it will
    /// add it as a possible block that the player can grab
    /// </summary>
    /// <param name="collider"></param>
	void OnTriggerEnter2D (Collider2D collider) {
		if (collider.tag == grabTag) {
            
			//Only allows the player to grab an element if it's solid -Nick S
			if(collider.GetComponent<ElementBehavior>().getCurState() == 0){
				grabbableBlock = collider.GetComponent<Transform>();
			}
		}
	}

    /// <summary>
    /// Removes the grabbable block that is currently set
    /// </summary>
    /// <param name="collider"></param>
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
