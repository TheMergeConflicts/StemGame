using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TempManager : MonoBehaviour {
	public float temp;
    public float tempSmoothing = 1;
	public Slider temperatureSlider;
    public ParticleSystem heatDistort;
	// Use this for initialization
	void Start () {

	}
	void Update(){
		temp = temperatureSlider.value;
        if(temp > 300.0f)
        {
            heatDistort.enableEmission = true;
        } else
        {
            heatDistort.enableEmission = false;
        }
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
		//adjustTemp (tempSmoothing);
		//temperature.value = temp;
    }

    public void decreaseTemp()
    {
        //adjustTemp(-tempSmoothing);
		//temperature.value = temp;
    }
}
