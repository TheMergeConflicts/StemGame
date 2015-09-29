using UnityEngine;
using System.Collections;

public class WaterPit : MonoBehaviour {
	Animator anim;
	Collider2D collider;
	bool filled;
	GameObject man;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		collider = GetComponent<Collider2D> ();
		man = GameObject.Find ("Temp_Manager");
		filled = false;
	}
	
	// Update is called once per frame
	void Update () {

		float temp = man.GetComponent<TempManager>().getTemp();
		if (filled) {
			if (temp < 276.0f) {
				freeze ();
			}
			if (temp >= 276.0f) {
				unfreeze ();
			}
		}	
	}

	void freeze(){
		collider.enabled = false;
		anim.SetInteger ("state", 2);
	}

	void unfreeze(){
		collider.enabled = true;
		anim.SetInteger ("state", 1);
	}

	public void fill(){
		anim.SetInteger ("state", 1);
	}

	void setFill(){
		filled = true;
	}
}
