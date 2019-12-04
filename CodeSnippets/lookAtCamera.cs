/*
 * any game object with this script attached to it will look for the mainCamera
 * and continually 'look' towards it
 *
 * used (for example) to keep in world text looking at the player camera front on
 * or for creating other billborad effects
 *
 * use lockOnYRotoation in the inspector to lock or unlock rotation on Y axis
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAtCamera : MonoBehaviour {

    public bool lockOnYRotoation = true;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
        Vector3 lookPos = transform.position - Camera.main.transform.position;
        if (lockOnYRotoation == true)
        {
            lookPos.y = 0;
        }
        transform.rotation = Quaternion.LookRotation(lookPos);
    }
}
