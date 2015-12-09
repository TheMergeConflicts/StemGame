using UnityEngine;
using System.Collections;

/// <summary>
/// Trigers the door open animation and disables the door collider
/// </summary>
public class Door : MonoBehaviour {

	private SpriteRenderer spriteR;
	private Collider2D col;
	public bool opened = false;
	private Animator anim;

	// Use this for initialization
	void Start () {
		spriteR = GetComponent<SpriteRenderer> ();
		col = GetComponent<Collider2D> ();
		anim = gameObject.GetComponent<Animator> ();
	}
	


	/// <summary>
	/// Open the Door.
	/// </summary>
    public bool Open() {
        if (!opened) {
            gameObject.GetComponent<AudioSource>().volume = 0.35f;
            gameObject.GetComponent<AudioSource>().Play();
        }
        col.enabled = false;
		anim.SetInteger ("AnimState", 1);
		opened = true;
		return opened;
	}
}
