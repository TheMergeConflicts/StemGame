using UnityEngine;
using System.Collections;

public class Element{
	public string name;
	public enum State {SOLID = 0, LIQUID = 1, GAS = 2};
	State curState;


	public float freezingPoint;
	public float meltingPoint;
	public float boilingPoint;

	public float curTemp;


	//generic constructor
	public Element(){
		name = "generic";
		curState = State.SOLID;
		freezingPoint = 0f;
		meltingPoint = 1f;
		boilingPoint = 100f;
		curTemp = 0f;
	}

	//parametered constructor
	public Element(string name, State curState, float freezingPoint, float meltingPoint, float boilingPoint, float curTemp){
		this.name = name;
		this.curState = curState;
		this.freezingPoint = freezingPoint;
		this.meltingPoint = meltingPoint;
		this.boilingPoint = boilingPoint;
		this.curTemp = curTemp;
	}

	//state machine
	public State updateState(){
		if (curTemp <= freezingPoint) {
			this.curState = State.SOLID;
		} else if (curTemp >= meltingPoint && curTemp < boilingPoint) { //NOTE: if we set meltingPoint == freezingPoint, we should update >= to >.
			this.curState = State.LIQUID;
		} else if (curTemp >= boilingPoint) {
			this.curState = State.GAS;
		}

		return this.curState;
	}


	//getters

	public string getName(){
		return name;
	}

	public State getCurState(){
		return curState;
	}

	public float getFreezingPoint(){
		return freezingPoint;
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
	
	public string setName(string name){
		this.name = name;
		return this.name;
	}
	
	public State setCurState(State curState){
		this.curState = curState;
		return this.curState;
	}
	
	public float setFreezingPoint(float freezingPoint){
		this.freezingPoint = freezingPoint;
		return this.freezingPoint;
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
