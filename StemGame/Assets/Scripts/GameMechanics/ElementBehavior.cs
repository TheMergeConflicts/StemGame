using UnityEngine;
using System.Collections;

public class ElementBehavior : MonoBehaviour {
	public string elementName;
	public ElementBehavior[] legalCombination;
	public ElementBehavior[] newCompound;
	private bool isGrabbed;

	//Added in Nick P's code, updated temperature logic. Should change element states based on global temp variable. -Nick S

	TempManager tempManager = null;
	
	public enum State {SOLID = 0, LIQUID = 1, GAS = 2};
	State curState;
	
	public float meltingPoint;
	public float boilingPoint;
	
	float curTemp;

	//generic constructor
	public ElementBehavior(){
		elementName = "generic";
		curState = State.SOLID;
		meltingPoint = 1f;
		boilingPoint = 100f;
		curTemp = 0f;
	}

	//parametered constructor
	public ElementBehavior(string elementName, State curState, float freezingPoint, float meltingPoint, float boilingPoint, float curTemp){
		this.elementName = elementName;
		this.curState = curState;
		this.meltingPoint = meltingPoint;
		this.boilingPoint = boilingPoint;
		this.curTemp = curTemp;
	}

	void Start(){
		TempManager[] tempManagers = FindObjectsOfType(typeof(TempManager)) as TempManager[];
		if (tempManagers.Length != 0){
			tempManager = tempManagers[0];
		}
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
			gameObject.GetComponent<Collider2D> ().enabled = true;
		} else if (curTemp >= meltingPoint && curTemp < boilingPoint && curState != State.LIQUID) {
			this.curState = State.LIQUID;
			Debug.Log (elementName + " melted!");
			gameObject.GetComponent<Collider2D> ().enabled = false;
		} else if (curTemp >= boilingPoint && curState != State.GAS) {
			this.curState = State.GAS;
			Debug.Log (elementName + " evaporated!");
			gameObject.GetComponent<Collider2D> ().enabled = false;
		} else if (curTemp < boilingPoint && curState == State.GAS) {
			this.curState = State.LIQUID;
			Debug.Log (elementName + " condensated!");
			gameObject.GetComponent<Collider2D> ().enabled = false;
		}
		return this.curState;
	}

	void OnCollisionEnter2D(Collision2D collider) {

		if (isGrabbed) {//This ensures that only one resulting element is produced when two blocks are combined -Nick S
			ElementBehavior collideElement = collider.collider.GetComponent<ElementBehavior> ();
			if (collideElement != null) {
				checkLegalCombination (collideElement);

			}
		}
	}

	void checkLegalCombination(ElementBehavior checkBehavior) {
		int i = 0;
		if (!isGrabbed) {
			return;
		}
		foreach (ElementBehavior ele in legalCombination) {

			if (checkBehavior.elementName == ele.elementName) {
				Destroy(checkBehavior.gameObject);
				Instantiate(newCompound[i].gameObject, checkBehavior.transform.position, new Quaternion());
				Destroy (this.gameObject);
			}
			i++;
		}
	}

	public void setIsGrabbed(bool isGrabbed) {
		this.isGrabbed = isGrabbed;
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
	
	public float getCurTemp(){
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
