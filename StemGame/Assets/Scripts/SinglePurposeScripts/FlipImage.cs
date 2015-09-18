using UnityEngine;
using System.Collections;

public class FlipImage : MonoBehaviour
{
    public WalkMechanics walkMechanics;

    void Update()
    {
        checkFlipImage();
    }

    void checkFlipImage()
    {
        if (walkMechanics.direction == WalkMechanics.WEST)
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            this.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}

