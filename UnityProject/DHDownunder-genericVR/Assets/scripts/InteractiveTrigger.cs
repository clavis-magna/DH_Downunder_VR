using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class InteractiveTrigger : MonoBehaviour
{
    // text to display based on interaction
    private TextMeshPro generalText;
    [TextArea]
    public string generalTextContent = "No Hand Hovering";
    [TextArea]
    public string generalHoverTextContent = "show this when we hover";
    [TextArea]
    public string generalAttachTextContent = "gripping now";

    // changeing material on hover
    private Material startMaterial;
    public Material hoverMaterial;
    public bool changeMaterialOnHover = false;

    // play sound on interaction
    public bool playSoundOnInteraction = false;
    public AudioClip soundToPlayOnHover;
    private AudioSource audioSource;

    // can we pick the object up?
    public bool canPickUp = false;
    bool isAttached = false;

    void Start()
    {
        var textMeshs = GetComponentsInChildren<TextMeshPro>();
        generalText = textMeshs[0];
        generalText.text = generalTextContent;
        startMaterial = GetComponent<Renderer>().material;

        if (playSoundOnInteraction)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void Update()
    {

    }

    //-------------------------------------------------
    // Called when a Hand starts hovering over this object
    //-------------------------------------------------
    private void OnTriggerEnter(Collider other)
    {
        print("enter");
        generalText.text = generalHoverTextContent;

        if (changeMaterialOnHover)
        {
            GetComponent<Renderer>().material = hoverMaterial;
        }

        if (playSoundOnInteraction)
        {
            audioSource.clip = soundToPlayOnHover;
            audioSource.Play();
        }
    }

    //-------------------------------------------------
    // Called every Update() while a Hand is hovering over this object
    //-------------------------------------------------
    private void OnTriggerStay(Collider other)
    {
        print("stay");
        generalText.text = generalHoverTextContent;

        // we are hovering and gripping with the left hand
        if ((Input.GetAxis("VRLeftTrigger") > 0.6f && other.transform.name == "leftHand") || (Input.GetAxis("VRRightTrigger") > 0.6f && other.transform.name == "rightHand"))
        {
            generalText.text = generalAttachTextContent;
            if (canPickUp)
            {
                if (isAttached == false)
                {
                    other.transform.Find("attachmentPoint").transform.position = this.gameObject.transform.position;
                    other.transform.Find("attachmentPoint").transform.rotation = this.gameObject.transform.rotation;
                    isAttached = true;
                }
                this.gameObject.transform.position = other.transform.Find("attachmentPoint").transform.position;
                this.gameObject.transform.rotation = other.transform.Find("attachmentPoint").transform.rotation;
            }
        }
        else
        {
            isAttached = false;
        }

    }

    //-------------------------------------------------
    // Called when a Hand stops hovering over this object
    //-------------------------------------------------
    private void OnTriggerExit(Collider other)
    {
        print("exit");
        generalText.text = generalTextContent;

        if (changeMaterialOnHover)
        {
            GetComponent<Renderer>().material = startMaterial;
        }

    }
}
