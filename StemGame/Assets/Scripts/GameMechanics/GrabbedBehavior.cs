using UnityEngine;
using System.Collections;

public class GrabbedBehavior : MonoBehaviour {
    public GrabMechanics grabMechanics;//The player's grab mechanics. Definitely should make that more clear

    private bool isGrabbed;
    private Rigidbody2D rigid;
    private Vector3 offset;


    public void setGrabMechanics(GrabMechanics grabMechanics) 
    {
        isGrabbed = true;
        this.grabMechanics = grabMechanics;
        rigid.isKinematic = false;
        offset = grabMechanics.transform.position - this.transform.position;
    }

    void Update()
    {
        
        if (isGrabbed && grabMechanics.getIsGrabbing())
        {
            checkDistance();

        }
        else
        {
            isGrabbed = false;
			grabMechanics = null;
            rigid.isKinematic = true;
            transform.parent = null;
        }
    }

    void checkDistance()
    {
        Vector3 checkDistance = grabMechanics.transform.position - this.transform.position;
        if (checkDistance.magnitude > offset.magnitude + .1f)
        {
            grabMechanics.grab(false);
            isGrabbed = false;
        }
    }

    void FixedUpdate()
    {
        if (isGrabbed)
        {
           rigid.velocity = grabMechanics.transform.parent.GetComponent<Rigidbody2D>().velocity;
        }
    }

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

	public bool getIsGrabbed() {
		return isGrabbed;
	}
}
