using UnityEngine;
using System.Collections;

public class TempManager : MonoBehaviour {
	public float temp;
    public float tempSmoothing = 1;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	public float getTemp(){
		return temp;
	}

	public void setTemp(float temp){
		this.temp = temp;
	}

    void adjustTemp(float delta)
    {
        temp += delta;
    }

    public void increaseTemp()
    {
        adjustTemp(tempSmoothing);
    }

    public void decreaseTemp()
    {
        adjustTemp(-tempSmoothing);
    }
}
