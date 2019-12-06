//
// Based on Desi Quintans (CowfaceGames.com) + FlyThrough.js http://wiki.unity3d.com/index.php/FlyThrough
// EXTENDED further for VR use in the Glossopticon & Layered Horizons Projects
//

using UnityEngine;
using System.Collections;
using UnityEngine.VR;

public class mouselookFallback : MonoBehaviour
{
    public float cameraSensitivity = 360;
    public float personHeight = 1.7f;

    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    void Start()
    {
        if (!UnityEngine.XR.XRDevice.isPresent)
        {
            transform.position = new Vector3(transform.position.x, personHeight, transform.position.z);
        }
    }

    void FixedUpdate()
    {
        //
        // Mouse and Keyboard
        // we don't do anything in this script if it is in VR mode
        //
        if (!UnityEngine.XR.XRDevice.isPresent)
        {
            if (Input.GetMouseButton(1))
            {
                rotationX += Input.GetAxis("Mouse X") * cameraSensitivity * Time.deltaTime;
                rotationY += Input.GetAxis("Mouse Y") * cameraSensitivity * Time.deltaTime;
                rotationY = Mathf.Clamp(rotationY, -90, 90);

                transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
                transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.left);
            }

        }
    }
}