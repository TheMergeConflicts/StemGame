using UnityEngine;
using System.Collections;

public class MusicLoop : MonoBehaviour {

    bool isIntro;
    AudioSource audioS;
    public AudioSource audioSLoop;

	// Use this for initialization
	void Start () {
        isIntro = true;
        audioS = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (isIntro && !audioS.isPlaying)
        {
            isIntro = false;
            audioSLoop.volume = 1;
        }
	}
}
