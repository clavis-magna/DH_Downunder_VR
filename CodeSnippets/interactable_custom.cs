// interactable_custom.cs
// an extension on steamVR interactable example script
// for UTS Design Practice (Theatres of Memory)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using TMPro;



//-------------------------------------------------------------------------
[RequireComponent(typeof(Interactable))]
public class interactable_custom : MonoBehaviour
{
    // text to display based on interaction
    private TextMeshPro generalText;
    [TextArea]
    public string generalTextContent = "No Hand Hovering";
    [TextArea]
    public string generalHoverTextContent = "show this when we hover";
    [TextArea]
    public string generalAttachTextContent = "gripping now";

    // to keep record of where object was before interaction began
    private Vector3 oldPosition;
    private Quaternion oldRotation;
    // and should the object snap back to its original position
    public bool returnObject = true;

    private Hand.AttachmentFlags attachmentFlags = Hand.defaultAttachmentFlags & (~Hand.AttachmentFlags.SnapOnAttach) & (~Hand.AttachmentFlags.DetachOthers) & (~Hand.AttachmentFlags.VelocityMovement);

    private Interactable interactable;

    // changeing material on hover
    private Material startMaterial;
    public Material hoverMaterial;
    public bool changeMaterialOnHover = false;

    // play sound on interaction
    public bool playSoundOnInteraction = false;
    public AudioClip soundToPlayOnHover;
    private AudioSource audioSource;


    //-------------------------------------------------
    void Awake()
    {
        var textMeshs = GetComponentsInChildren<TextMeshPro>();
        generalText = textMeshs[0];

        generalText.text = generalTextContent;

        interactable = this.GetComponent<Interactable>();

        startMaterial = GetComponent<Renderer>().material;

        if (playSoundOnInteraction)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }


    //-------------------------------------------------
    // Called when a Hand starts hovering over this object
    //-------------------------------------------------
    private void OnHandHoverBegin(Hand hand)
    {
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
    // Called when a Hand stops hovering over this object
    //-------------------------------------------------
    private void OnHandHoverEnd(Hand hand)
    {
        generalText.text = generalTextContent;

        if (changeMaterialOnHover)
        {
            GetComponent<Renderer>().material = startMaterial;
        }
    }


    //-------------------------------------------------
    // Called every Update() while a Hand is hovering over this object
    //-------------------------------------------------
    private void HandHoverUpdate(Hand hand)
    {
        GrabTypes startingGrabType = hand.GetGrabStarting();
        bool isGrabEnding = hand.IsGrabEnding(this.gameObject);

        if (interactable.attachedToHand == null && startingGrabType != GrabTypes.None)
        {
            // Save our position/rotation so that we can restore it when we detach
            oldPosition = transform.position;
            oldRotation = transform.rotation;

            // Call this to continue receiving HandHoverUpdate messages,
            // and prevent the hand from hovering over anything else
            hand.HoverLock(interactable);

            // Attach this object to the hand
            hand.AttachObject(gameObject, startingGrabType, attachmentFlags);
        }
        else if (isGrabEnding)
        {
            // Detach this object from the hand
            hand.DetachObject(gameObject);

            // Call this to undo HoverLock
            hand.HoverUnlock(interactable);

            // Restore position/rotation
            if (returnObject)
            {
                transform.position = oldPosition;
                transform.rotation = oldRotation;
            }
        }
    }


    //-------------------------------------------------
    // Called when this GameObject becomes attached to the hand
    //-------------------------------------------------
    private void OnAttachedToHand(Hand hand)
    {
        generalText.text = generalAttachTextContent;
    }



    //-------------------------------------------------
    // Called when this GameObject is detached from the hand
    //-------------------------------------------------
    private void OnDetachedFromHand(Hand hand)
    {
        generalText.text = generalHoverTextContent;
    }


    //-------------------------------------------------
    // Called every Update() while this GameObject is attached to the hand
    //-------------------------------------------------
    private void HandAttachedUpdate(Hand hand)
    {

    }

    private bool lastHovering = false;
    private void Update()
    {
        if (interactable.isHovering != lastHovering) //save on the .tostrings a bit
        {
            lastHovering = interactable.isHovering;
        }
    }


    //-------------------------------------------------
    // Called when this attached GameObject becomes the primary attached object
    //-------------------------------------------------
    private void OnHandFocusAcquired(Hand hand)
    {
    }


    //-------------------------------------------------
    // Called when another attached GameObject becomes the primary attached object
    //-------------------------------------------------
    private void OnHandFocusLost(Hand hand)
    {
    }
}
