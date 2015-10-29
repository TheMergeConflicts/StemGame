using UnityEngine;
using System.Collections;

public class TextureScale : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.localScale = new Vector3(.25f * GetComponentInParent<Camera>().orthographicSize, .25f * GetComponentInParent<Camera>().orthographicSize, 1);
        transform.localPosition = new Vector3(0 + transform.localScale.x * 4, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
