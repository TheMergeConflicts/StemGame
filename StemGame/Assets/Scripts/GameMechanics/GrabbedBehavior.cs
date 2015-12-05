using UnityEngine;
using System.Collections;

public class GrabbedBehavior : MonoBehaviour {
    public GrabMechanics grabMechanics;//The player's grab mechanics. Definitely should make that more clear

    private bool isGrabbed;
    private Rigidbody2D rigid;
    private Vector3 offset;


    /// <summary>
    /// sets the grab mechanics to true when called and takes care of all the 
    /// </summary>
    /// <param name="grabMechanics"></param>
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
            predictCollisionWithPlayer();

        }
        else
        {
            isGrabbed = false;
			grabMechanics = null;
            rigid.isKinematic = true;
            transform.parent = null;
        }
    }

    /// <summary>
    /// This helps with block passing through the player when colliding with a wall glitch
    /// It stops the block from moving toward the player if the player is not moving, but still adding a 
    /// force to the block
    /// </summary>
    void predictCollisionWithPlayer()
    {
        if (isGrabbed)
        {
            Vector3 checkDistance = grabMechanics.transform.position - this.transform.position;
            if (checkDistance.magnitude < offset.magnitude)
            {
                Vector3 predictionVector = checkDistance - new Vector3(rigid.velocity.x, rigid.velocity.y, 0);
                if (predictionVector.magnitude < checkDistance.magnitude)
                {
                    transform.position = grabMechanics.transform.position - offset;
                }
            }

        }
    }

    /// <summary>
    /// This is used to check the distance the block is away from the player.
    /// If it is too far away, it will disconnect the player from the block.
    /// </summary>
    void checkDistance()
    {
        Vector3 checkDistance = grabMechanics.transform.position - this.transform.position;
        if (checkDistance.magnitude > offset.magnitude + .1f)
        {
            grabMechanics.grab(false);
            isGrabbed = false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
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
