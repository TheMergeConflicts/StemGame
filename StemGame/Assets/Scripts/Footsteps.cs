using UnityEngine;
using System.Collections;

public class Footsteps : MonoBehaviour
{

    AudioSource audioS;
    float coolDown = 0;

    // Use this for initialization
    void Start()
    {
        audioS = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (coolDown > 0)
        {
            coolDown -= Time.deltaTime;
        }
    }


    /// <summary>
    /// Plays a randomized footstep sound twice per animation cycle
    /// </summary>
    public void Step()
    {
        if (audioS != null && coolDown <= 0)
        {
            switch (Random.Range(0, 4))
            {
                case 0:
                    audioS.clip = Resources.Load("SFX/StemGameMetalStep1") as AudioClip;
                    break;
                case 1:
                    audioS.clip = Resources.Load("SFX/StemGameMetalStep2") as AudioClip;
                    break;
                case 2:
                    audioS.clip = Resources.Load("SFX/StemGameMetalStep3") as AudioClip;
                    break;
                case 3:
                    audioS.clip = Resources.Load("SFX/StemGameMetalStep4") as AudioClip;
                    break;
            }
            audioS.pitch = Random.Range(0.95f, 1.05f);
            audioS.volume = Random.Range(0.17f, 0.23f);
            audioS.Play();
            coolDown = 0.1f;
        }
    }

}