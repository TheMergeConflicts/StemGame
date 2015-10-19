using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TempManager : MonoBehaviour {
	public float temp;
    public float tempSmoothing = 1;
	public Slider temperatureSlider;
    public ParticleSystem heatDistort;
	public GameObject iceDistorter;
	public GameObject iceTexture;
	// Use this for initialization
	void Start () {

	}
	void Update(){
		temp = temperatureSlider.value;
		float iceStrength = -.1f * (temp-100);
		float iceColorStrength = (.3f) * (1-(temp / 100f));
		Color iceColor = iceTexture.GetComponent<Renderer> ().material.color;



		if (temp < 100.0f) {
			iceDistorter.GetComponent<Renderer> ().material.SetFloat ("_Refraction", iceStrength);
			Color iceModifiedColor = new Color (iceColor.r, iceColor.g, iceColor.b, iceColorStrength);
			iceTexture.GetComponent<Renderer> ().material.color = iceModifiedColor;
		} else { // to make sure screen ice is completely gone if we skip the melting stage
			iceDistorter.GetComponent<Renderer> ().material.SetFloat ("_Refraction", 0f);
			Color iceModifiedColor = new Color (iceColor.r, iceColor.g, iceColor.b, 0f);
			iceTexture.GetComponent<Renderer> ().material.color = iceModifiedColor;
		}
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
