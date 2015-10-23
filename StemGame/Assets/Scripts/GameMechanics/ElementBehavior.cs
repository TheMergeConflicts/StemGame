﻿using UnityEngine;
using System.Collections;

public class ElementBehavior : MonoBehaviour {
	public string elementName;
    public Collider2D solidCollider;
	public ElementBehavior[] legalCombination;
	public ElementBehavior[] newCompound;
	GrabbedBehavior grabbedBehavior;
	//private bool isGrabbed;

	//Added in Nick P's code, updated temperature logic. Should change element states based on global temp variable. -Nick S

	TempManager tempManager = null;
	
	public enum State {SOLID = 0, LIQUID = 1, GAS = 2};
	protected bool sublime;
	State curState;
	Animator anim;
	public float meltingPoint;
	public float boilingPoint;

    public float activationTemp;
	
	float curTemp;

	//generic constructor
	public ElementBehavior(){
		elementName = "generic";
		curState = State.SOLID;
		meltingPoint = 1f;
		boilingPoint = 100f;
        activationTemp = 0.0f;
		curTemp = 0f;
		sublime = false;
	}

	//parametered constructor
	public ElementBehavior(string elementName, State curState, float freezingPoint, float meltingPoint, float boilingPoint, float curTemp, bool sublime, float activationTemp){
		this.elementName = elementName;
        this.activationTemp = activationTemp;
		this.curState = curState;
		this.meltingPoint = meltingPoint;
		this.boilingPoint = boilingPoint;
		this.curTemp = curTemp;
		this.sublime = sublime;
	}

	void Start(){
		TempManager[] tempManagers = FindObjectsOfType(typeof(TempManager)) as TempManager[];
		grabbedBehavior = GetComponent<GrabbedBehavior> ();
		if (tempManagers.Length != 0){
			tempManager = tempManagers[0];
		}
		anim = gameObject.GetComponent<Animator> ();
	}

	//Fixed update. Updates temp if it exists, changes states of element.
	void FixedUpdate(){
		if (tempManager != null) {
			curTemp = tempManager.getTemp();
		}
		updateState ();
	}

	//state machine
	public State updateState(){
		if (curTemp < meltingPoint && curState != State.SOLID) {
			this.curState = State.SOLID;
			Debug.Log (elementName + " froze!");
			solidCollider.enabled = true;
			anim.SetInteger("state", 0);
            shiftPlayer();
		} else if (!sublime && curTemp >= meltingPoint && curTemp < boilingPoint && curState != State.LIQUID) {
			this.curState = State.LIQUID;
			Debug.Log (elementName + " melted!");
			solidCollider.enabled = false;
			anim.SetInteger("state", 1);
		} else if (curTemp >= boilingPoint && curState != State.GAS) {
			this.curState = State.GAS;
			Debug.Log (elementName + " evaporated!");
			solidCollider.enabled = false;
			anim.SetInteger("state", 2);
		} else if (curTemp < boilingPoint && curState == State.GAS) {
			this.curState = State.LIQUID;
			Debug.Log (elementName + " condensated!");
			solidCollider.enabled = false;
			anim.SetInteger("state", 1);
		}
		return this.curState;
	}

	void OnTriggerEnter2D(Collider2D collider) {

		if (grabbedBehavior.getIsGrabbed()) {//This ensures that only one resulting element is produced when two blocks are combined -Nick S
			ElementBehavior collideElement = collider.GetComponent<ElementBehavior> ();
			if (collideElement != null) {
				checkLegalCombination (collideElement);

			}
		}
	}

    void shiftPlayer()
    {
        GameObject player = GameObject.FindWithTag("Player");
        Vector3 checkDistacne = transform.position - player.transform.position;
        if (Mathf.Abs(checkDistacne.x) < 1.0f && Mathf.Abs(checkDistacne.y) < 1.0f)
        {
            if (!Physics2D.Raycast(transform.position + new Vector3(0, transform.localScale.y / 2f + .1f, 0), Vector2.up, 1.5f, 1))
            {
                player.transform.position = transform.position + Vector3.up;

            }
            else if (!Physics2D.Raycast(transform.position + new Vector3(0, -transform.localScale.y / 2f - .1f, 0),  -Vector2.up, 1.5f, 1))
            {
                player.transform.position = transform.position - Vector3.up;
            }
            else if (!Physics2D.Raycast(transform.position + new Vector3(-transform.localScale.x / 2f - .1f, 0, 0), Vector2.left, 1.5f, 1))
            {
                player.transform.position = transform.position + Vector3.left;
            }
            else if (Physics2D.Raycast(transform.position + new Vector3(transform.localScale.x / 2f + .1f, 0, 0), -Vector2.left, 1.5f, 1))
            {
                player.transform.position = transform.position - Vector3.left;
            }
        }

    }

	void checkLegalCombination(ElementBehavior checkBehavior) {
		int i = 0;
		if (!grabbedBehavior.getIsGrabbed()) {
			return;
		}
		foreach (ElementBehavior ele in legalCombination) {

			if (curTemp >= newCompound[i].gameObject.GetComponent<ElementBehavior>().activationTemp &&checkBehavior.elementName == ele.elementName) {
				Destroy(checkBehavior.gameObject);
				Instantiate(newCompound[i].gameObject, checkBehavior.transform.position, new Quaternion());
				Destroy (this.gameObject);
			}
			i++;
		}
	}

	public void setIsGrabbed(bool isGrabbed) {
		//this.isGrabbed = isGrabbed;
	}

	//getters
	
	public string getName(){
		return elementName;
	}
	
	public State getCurState(){
		return curState;
	}
	
	public float getMeltingPoint(){
		return meltingPoint;
	}
	
	public float getBoilingPoint(){
		return boilingPoint;
	}

    public float getCurTemp()
    {
        return curTemp;
    }
	
	
	//setters
	
	public string setName(string elementName){
		this.elementName = elementName;
		return this.elementName;
	}
	
	public State setCurState(State curState){
		this.curState = curState;
		return this.curState;
	}
	
	public float setMeltingPoint(float meltingPoint){
		this.meltingPoint = meltingPoint;
		return this.meltingPoint;
	}
	
	public float setBoilingPoint(float boilingPoint){
		this.boilingPoint = boilingPoint;
		return this.boilingPoint;
	}
	
	public float setCurTemp(float curTemp){
		this.curTemp = curTemp;
		return this.curTemp;
	}

}
