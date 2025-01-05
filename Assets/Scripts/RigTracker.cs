using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Collects the tracking information of the whole rig
// Head, hands, fingers (hand tracking)
public class RigTracker : MonoBehaviour
{
    public Transform headAnchor;  // the tracked head transform
    public Transform leftHandAnchor; // tracked left hand transform
    public Transform rightHandAnchor; // tracked right hand transform

    [SerializeField] private OVRHand leftHand;
    [SerializeField] private OVRHand rightHand;
    [SerializeField] private OVRSkeleton leftHandSkeleton;
    [SerializeField] private OVRSkeleton rightHandSkeleton;

    public Dictionary<OVRSkeleton.BoneId, Transform> rightHandBones = new Dictionary<OVRSkeleton.BoneId, Transform>();
    public Dictionary<OVRSkeleton.BoneId, Transform> leftHandBones = new Dictionary<OVRSkeleton.BoneId, Transform>();

    private bool leftBonesInit = false;
    private bool rightBonesInit = false;
    public bool handBonesInitialized = false;

    public bool leftPinching = false;
    public bool rightPinching = false;

    //public bool leftTracked;
    //public bool rightTracked;
    public bool isLeftHandTracked()
    {
        return leftHand.IsTracked;
    }
    public bool isRightHandTracked()
    {
        return rightHand.IsTracked;
    }
    

    private void Start()
    {
        
    }

    private void Update()
    {
        //leftTracked = isLeftHandTracked();
        //rightTracked = isRightHandTracked();
        
        // create bones
        if (leftBonesInit == false && isLeftHandTracked() && leftHandSkeleton != null)
        {
            foreach (var bone in leftHandSkeleton.Bones)
            {
                leftHandBones[bone.Id] = bone.Transform;
            }

            leftBonesInit = true;
        }
        if (rightBonesInit == false && isRightHandTracked() && rightHandSkeleton != null)
        {
            foreach (var bone in rightHandSkeleton.Bones)
            {
                rightHandBones[bone.Id] = bone.Transform;
            }

            rightBonesInit = true;
        }

        if (!handBonesInitialized && leftBonesInit && rightBonesInit)
        {
            handBonesInitialized = true;  // dictionary init complete
        }
        
        /*
        foreach (var bone in leftHandBones)
        {
            Debug.Log($"Left BoneID: {bone.Key}, Position: {bone.Value.position}, Rotation: {bone.Value.rotation}");
        }
        */
        
        // get pinching state
        leftPinching = leftHand.GetFingerIsPinching(OVRHand.HandFinger.Index);
        rightPinching = rightHand.GetFingerIsPinching(OVRHand.HandFinger.Index);
    }
}
