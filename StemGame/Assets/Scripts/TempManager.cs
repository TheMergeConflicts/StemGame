using UnityEngine;
using System.Collections;

public class TempManager : MonoBehaviour {
	public float temp;

	// Use this for initialization
	void Start () {
		temp = 273.15f;

	}
	
	// Update is called once per frame
	public float getTemp(){
		return temp;
	}

	public void setTemp(float temp){
		temp = temp;
	}
}
