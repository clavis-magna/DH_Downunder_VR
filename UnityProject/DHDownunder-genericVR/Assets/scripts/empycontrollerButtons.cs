using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class emptyControllerButtons : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        print(Input.GetAxis("VRLeftTrigger"));
    }


}
