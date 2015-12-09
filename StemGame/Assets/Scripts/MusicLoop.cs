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
	

    /// <summary>
    /// Alternates between the intro track and the loop track after the first iteration
    /// </summary>
	void Update () {
        if (isIntro && !audioS.isPlaying)
        {
            isIntro = false;
            audioSLoop.volume = 1;
        }
	}
}
