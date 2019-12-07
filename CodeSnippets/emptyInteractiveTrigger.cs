using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class emptyInteractiveTrigger : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        print("enter");
    }

    private void OnTriggerStay(Collider other)
    {
        print("stay");
    }

    private void OnTriggerExit(Collider other)
    {
        print("exit");
    }
}
