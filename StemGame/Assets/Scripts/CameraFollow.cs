using UnityEngine;
using System.Collections;
/// <summary>
/// Scales and positions the camera to follow the player
/// </summary>
public class CameraFollow : MonoBehaviour {
	public GameObject target;
    public float screenFactor;
	public Transform _t;
    public Vector3 pos;
	/// <summary>
    /// Changes the camera's orhtographic size based on a user-defined factor
    /// </summary>
	void Awake() {
		GetComponent<Camera>().orthographicSize = ((Screen.height / screenFactor) / 100f);
	}
    /// <summary>
    /// gets the follow target's transform object
    /// </summary>
	void Start () {
		_t = target.transform;
	}
	
	/// <summary>
    /// moves the camera x and y position to the follow targets position every
    /// frame
    /// </summary>
	void Update () {
            _t = target.transform;
            pos = _t.position;
            transform.position = new Vector3(_t.position.x, _t.position.y, transform.position.z);

	}
}
