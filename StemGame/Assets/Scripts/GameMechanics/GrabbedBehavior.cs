using UnityEngine;
using System.Collections;

public class GrabbedBehavior : MonoBehaviour {
    public GrabMechanics grabMechanics;

    private bool isGrabbed;
    private Rigidbody2D rigid;


    public void setGrabMechanics(GrabMechanics grabMechanics) 
    {
        isGrabbed = true;
        this.grabMechanics = grabMechanics;
        rigid.isKinematic = false;
    }

    void Update()
    {
        if (isGrabbed && grabMechanics.getIsGrabbing())
        {
            
            
        }
        else
        {
            isGrabbed = false;
			grabMechanics = null;
            rigid.isKinematic = true;
            transform.parent = null;
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
