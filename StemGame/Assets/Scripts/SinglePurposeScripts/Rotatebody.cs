using UnityEngine;
using System.Collections;

public class Rotatebody : MonoBehaviour {
    public WalkMechanics walkMechanics;

    void Update()
    {
        updateRotation();
    }

    void updateRotation()
    {
        int direction = walkMechanics.direction;
        switch(direction)
        {
            case WalkMechanics.NORTH:
                this.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case WalkMechanics.SOUTH:
                this.transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
            case WalkMechanics.EAST:
                this.transform.rotation = Quaternion.Euler(0, 0, 270);
                break;
            case WalkMechanics.WEST:
                this.transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
        }
    }
}
