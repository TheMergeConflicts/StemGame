﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;


/// <summary>
/// This class is responsible for any temperature slider
/// manipulations, and the resulting effects on the 
/// elements and the visual presentation.
/// </summary>


public class TempManager : MonoBehaviour {
	public float temp;
    public float tempSmoothing = 1;
	public Slider temperatureSlider;
    public ParticleSystem heatDistort;
	public GameObject iceDistorter;
	public GameObject iceTexture;
	public GameObject coldScreenColor;
	public GameObject hotScreenColor;
	// Use this for initialization

	/// <summary>
	/// Initializes the cold and hot overlay screens
	/// by finding them in the game.
	/// </summary>
	void Start () {
		coldScreenColor = GameObject.Find ("ColdScreen");
		hotScreenColor = GameObject.Find ("HotScreen");
	}

	/// <summary>
	/// This is called every frame. It checks the temperature
	/// value from the slider, calculates the intensity of the
	/// special effects based on the temperature, and
	/// applies them to the screen.
	/// </summary>
	void Update(){
		temp = temperatureSlider.value;
		
		float iceStrength = -.1f * (temp - 100);
		float iceColorStrength = (.3f) * (1 - (temp / 100f));
		float hotColorStrength = (.3f) * (((temp + 300) - 300) / 700);//(.3f) * (1 - (300 - temp / 300f));
		Color iceColor = iceTexture.GetComponent<Renderer> ().material.color;
		Color coldScreenColorColor = coldScreenColor.GetComponent<Renderer> ().material.color;
		Color hotScreenColorColor = hotScreenColor.GetComponent<Renderer> ().material.color;
		
		//tint the screen a color based on temp, system independent
		if (temp < 100.0f) {
			
			Color coldScreenModifiedColor = new Color (coldScreenColorColor.r, coldScreenColorColor.g, coldScreenColorColor.b, iceColorStrength);
			coldScreenColor.GetComponent<Renderer> ().material.color = coldScreenModifiedColor;
		} else { // to make sure screen ice is completely gone if we skip the melting stage
			Color  coldScreenModifiedColor = new Color (coldScreenColorColor.r, coldScreenColorColor.g, coldScreenColorColor.b, 0);
			coldScreenColor.GetComponent<Renderer> ().material.color = coldScreenModifiedColor;
		}
		
		if (temp > 300.0f) {
			Color hotScreenModifiedColor = new Color (hotScreenColorColor.r, hotScreenColorColor.g, hotScreenColorColor.b, hotColorStrength);
			hotScreenColor.GetComponent<Renderer> ().material.color = hotScreenModifiedColor;
		} else {
			Color  hotScreenModifiedColor = new Color (hotScreenColorColor.r, hotScreenColorColor.g, hotScreenColorColor.b, 0);
			hotScreenColor.GetComponent<Renderer> ().material.color = hotScreenModifiedColor;
		}
		
		
		
		//deal with platform stuff Win vs Mac
		RuntimePlatform curSys = Application.platform;
		if ((curSys == RuntimePlatform.WindowsPlayer || curSys == RuntimePlatform.WindowsEditor
		     || curSys == RuntimePlatform.WindowsWebPlayer)) {
				
			if (temp < 100.0f) {
				iceDistorter.GetComponent<Renderer> ().material.SetFloat ("_Refraction", iceStrength);
				Color iceModifiedColor = new Color (iceColor.r, iceColor.g, iceColor.b, iceColorStrength);
				iceTexture.GetComponent<Renderer> ().material.color = iceModifiedColor;
			} else { // to make sure screen ice is completely gone if we skip the melting stage
				iceDistorter.GetComponent<Renderer> ().material.SetFloat ("_Refraction", 0f);
				Color iceModifiedColor = new Color (iceColor.r, iceColor.g, iceColor.b, 0f);
				iceTexture.GetComponent<Renderer> ().material.color = iceModifiedColor;
			}
			
			if (temp > 300.0f) {
				heatDistort.enableEmission = true;
			} else {
				heatDistort.enableEmission = false;
			}
			
			
		} else { // if on a mac, etc
			
			//iceTexture.GetComponent<Renderer>().enabled=false;
			iceDistorter.GetComponent<Renderer>().enabled = false;
			heatDistort.GetComponent<Renderer>().enabled = false;
			
			if (temp < 100.0f) {
				Color iceModifiedColor = new Color (iceColor.r, iceColor.g, iceColor.b, iceColorStrength);
				iceTexture.GetComponent<Renderer> ().material.color = iceModifiedColor;
			} else { // to make sure screen ice is completely gone if we skip the melting stage
				Color iceModifiedColor = new Color (iceColor.r, iceColor.g, iceColor.b, 0f);
				iceTexture.GetComponent<Renderer> ().material.color = iceModifiedColor;
			}
			
		}
	}
	// Update is called once per frame

	/// <summary>
	/// Gets the temp.
	/// </summary>
	/// <returns>The temp.</returns>

	public float getTemp(){
		return temp;
	}

	/// <summary>
	/// Sets the temp.
	/// </summary>
	/// <param name="temp">Temp.</param>
	public void setTemp(float temp){
		this.temp = temp;
	}

	/// <summary>
	/// Adjusts the temp.
	/// </summary>
	/// <param name="delta">Delta.</param>
    void adjustTemp(float delta)
    {
        temp += delta;
    }

	/// <summary>
	/// Increases the temp.
	/// </summary>
    public void increaseTemp()
    {
		//adjustTemp (tempSmoothing);
		//temperature.value = temp;
    }


	/// <summary>
	/// Decreases the temp.
	/// </summary>
    public void decreaseTemp()
    {
        //adjustTemp(-tempSmoothing);
		//temperature.value = temp;
    }
}
