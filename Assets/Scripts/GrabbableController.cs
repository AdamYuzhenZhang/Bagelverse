using System;
using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction.Demo;
using UnityEngine;
using UnityEngine.SceneManagement;


// Controls the grabbable object and how they reflect
public class GrabbableController : MonoBehaviour
{
    // Info about all bagels
    [SerializeField] private List<BagelManager> bagels;
    // Info about the physical hands
    [SerializeField] private RigTracker rigTracker;
    // Layer of this object
    [SerializeField] private int layer;
    // Parent of this object, set back after drop
    [SerializeField] private Transform defaultParent;
    // The target hands that overlaps with this object
    [SerializeField] private Transform leftTargetHand = null;
    [SerializeField] private Transform rightTargetHand = null;
    [SerializeField] private bool isGrabbed = false;
    [SerializeField] private bool grabbedByLeft = false;
    [SerializeField] private bool grabbedByRight = false;
    // [SerializeField] are for debug. many can be removed
    
    private void Start()
    {
        // set up this layer
        layer = this.gameObject.layer;
        defaultParent = this.transform.parent;
    }

    private void Update()
    {
        // release when tracking lost, to prevent bugs!
        if (isGrabbed && ((grabbedByLeft && !rigTracker.isLeftHandTracked()) ||
            (grabbedByRight && !rigTracker.isRightHandTracked())))
        {
            Debug.Log("Lost Track and Release");
            ReleaseObject();
        }
        
        
        if (isGrabbed)
        {
            if (grabbedByLeft && !rigTracker.leftPinching)
            {
                // if left hand not pinch, release it
                ReleaseObject();
            }
            if (grabbedByRight && !rigTracker.rightPinching)
            {
                // if left hand not pinch, release it
                ReleaseObject();
            }
        }
        else
        {
            // not grabbed
            if (leftTargetHand != null && rigTracker.leftPinching)
            {
                // grab with left hand when pinching
                isGrabbed = true;
                grabbedByLeft = true;
                grabbedByRight = false;
                this.transform.parent = leftTargetHand;
            }
            else if (rightTargetHand != null && rigTracker.rightPinching)
            {
                // Grab with right hand
                isGrabbed = true;
                grabbedByLeft = false;
                grabbedByRight = true;
                this.transform.parent = rightTargetHand;
            }
        }
    }

    private void ReleaseObject()
    {
        isGrabbed = false;
        grabbedByLeft = false;
        grabbedByRight = false;
        this.transform.parent = defaultParent;
    }

    private bool IsWithinPortal(int portalLayer)
    {
        bool result = false;
        foreach (BagelManager b in bagels)
        {
            if (b.mirrorLayer == portalLayer)
            {
                //Debug.Log("Checking is within portal");
                // in the same layer, check distance from object to bagel center
                float dist = Vector3.Distance(b.portalTransform.position, this.transform.position);
                result = dist < b.portalDiameter;
                // A better distance is the dist between portal and the object position projected onto the portal plane
                // This is still good enough for small objects
            }
        }

        return result;
    }

    void SetLayerRecursively(GameObject obj, int newLayer)
    {
        if (obj == null)
            return;

        // Set the layer of the current object
        obj.layer = newLayer;

        // Recursively set the layer of all children
        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // get other gameobject
        if (other.CompareTag("LeftHand"))
        {
            //Debug.Log("LeftHandTriggerEnter");
            //Debug.Log("This Layer " + layer);
            //Debug.Log("Left Hand Layer " + other.gameObject.layer);
            // this works only when hand is not pinching on enter
            // If is pinching will need to exit and enter again
            // This is a bit hard to solve
            if (!isGrabbed && other.gameObject.layer == layer && !rigTracker.leftPinching)
            {
                // if this is not grabbed
                // if is hand and on the same layer, can grab
                leftTargetHand = other.transform;
            }
            if (grabbedByLeft && ((layer == LayerMask.NameToLayer("Default") && 
                               other.gameObject.layer != LayerMask.NameToLayer("Default")) || 
                              (layer != LayerMask.NameToLayer("Default") && 
                               other.gameObject.layer == LayerMask.NameToLayer("Default"))))
            {
                // this is grabbed by left
                // Either this is the default layer and other not, or other is default this is not
                // if within the portal transfer the object through portal
                if ((layer == LayerMask.NameToLayer("Default") && IsWithinPortal(other.gameObject.layer)) ||
                    (other.gameObject.layer == LayerMask.NameToLayer("Default") && IsWithinPortal(layer)))
                {
                    leftTargetHand = other.transform;
                    this.transform.parent = leftTargetHand;
                    layer = other.gameObject.layer;
                    //this.gameObject.layer = layer;
                    SetLayerRecursively(this.gameObject, layer);
                }
                
            }
        }
        if (other.CompareTag("RightHand"))
        {
            if (!isGrabbed && other.gameObject.layer == layer && !rigTracker.rightPinching)
            {
                // if this is not grabbed
                // if is hand and on the same layer, can grab
                rightTargetHand = other.transform;
            }
            if (grabbedByRight && ((layer == LayerMask.NameToLayer("Default") && 
                                   other.gameObject.layer != LayerMask.NameToLayer("Default")) || 
                                  (layer != LayerMask.NameToLayer("Default") && 
                                   other.gameObject.layer == LayerMask.NameToLayer("Default"))))
            {
                // this is grabbed by left
                // Either this is the default layer and other not, or other is default this is not
                // if within the portal transfer the object through portal
                if ((layer == LayerMask.NameToLayer("Default") && IsWithinPortal(other.gameObject.layer)) ||
                    (other.gameObject.layer == LayerMask.NameToLayer("Default") && IsWithinPortal(layer)))
                {
                    rightTargetHand = other.transform;
                    this.transform.parent = rightTargetHand;
                    layer = other.gameObject.layer;
                    //this.gameObject.layer = layer;
                    SetLayerRecursively(this.gameObject, layer);
                }
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        // get other gameobject
        if (other.CompareTag("LeftHand"))
        {
            if (grabbedByLeft && other.gameObject.layer == layer)
            {
                // this should not happen, ungrab
                ReleaseObject();
            }

            if (other.gameObject.layer == layer)
            {
                leftTargetHand = null;  // remove reference to left hand
            }
        }
        if (other.CompareTag("RightHand"))
        {
            if (grabbedByRight && other.gameObject.layer == layer)
            {
                // this should not happen, ungrab
                ReleaseObject();
            }

            if (other.gameObject.layer == layer)
            {
                rightTargetHand = null;
            }
        }
    }
}
