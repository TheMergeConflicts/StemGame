﻿using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	private SpriteRenderer spriteR;
	private Collider2D col;
	public bool open = false;
	private Animator anim;

	// Use this for initialization
	void Start () {
		spriteR = GetComponent<SpriteRenderer> ();
		col = GetComponent<Collider2D> ();
		anim = gameObject.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Open(){
		col.enabled = false;
		anim.SetInteger ("AnimState", 1);
	}
}