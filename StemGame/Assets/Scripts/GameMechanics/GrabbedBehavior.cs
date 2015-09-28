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
    }

    void Update()
    {
        if (isGrabbed && grabMechanics.getIsGrabbing())
        {
            rigid.isKinematic = false ;
        }
        else
        {
            isGrabbed = false;
			grabMechanics = null;
            rigid.isKinematic = true;
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
