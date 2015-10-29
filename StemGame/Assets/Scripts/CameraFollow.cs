using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	public GameObject target;
    public float screenFactor;
	public Transform _t;
    public Vector3 pos;
	// Use this for initialization
	void Awake() {
		GetComponent<Camera>().orthographicSize = ((Screen.height / screenFactor) / 100f);
	}
	void Start () {
		_t = target.transform;
	}
	
	// Update is called once per frame
	void Update () {
            _t = target.transform;
            pos = _t.position;
            transform.position = new Vector3(_t.position.x, _t.position.y, transform.position.z);

	}
}
