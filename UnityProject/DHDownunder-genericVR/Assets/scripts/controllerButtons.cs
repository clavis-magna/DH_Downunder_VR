using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class controllerButtons : MonoBehaviour
{
    public GameObject leftThumb;
    public GameObject rightThumb;

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        //print(Input.GetAxis("VRLeftTrigger"));
        if(Input.GetAxis("VRLeftTrigger") > 0.6f)
        {
            leftThumb.transform.localRotation = Quaternion.Euler(0, 0, 0); 
        }
        else
        {
            leftThumb.transform.localRotation = Quaternion.Euler(0, 45, 0);
        }

        if (Input.GetAxis("VRRightTrigger") > 0.6f)
        {
            rightThumb.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            rightThumb.transform.localRotation = Quaternion.Euler(0, -45, 0);
        }
    }


}
