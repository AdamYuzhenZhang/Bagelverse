using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Takes in the original tracked rig transforms
// calculates the mirrored transforms
public class RigMirror : MonoBehaviour
{
    [SerializeField] private RigTracker rigTracker;
    [SerializeField] private Transform mirrorTransform;

    [SerializeField] private Transform reflectedHead;
    [SerializeField] private Transform reflectedLeftHand;
    [SerializeField] private Transform reflectedRightHand;
    private void Update()
    {
        // Get transforms
        Transform headAnchor = rigTracker.headAnchor;
        Transform leftHandAnchor = rigTracker.leftHandAnchor;
        Transform rightHandAnchor = rigTracker.rightHandAnchor;
        
        // Calculate and assign mirrored transforms
        Reflect(headAnchor, reflectedHead);
        Reflect(leftHandAnchor, reflectedLeftHand);
        Reflect(rightHandAnchor, reflectedRightHand);
    }

    // Takes in a transform and get its reflection with the mirror
    private void Reflect(Transform input, Transform reflected)
    {
        Vector3 mirrorPos = mirrorTransform.position;
        Vector3 mirrorNormal = mirrorTransform.up;

        Vector3 directionToTarget = input.position - mirrorPos;
        Vector3 reflectedPosition = input.position - 2 * Vector3.Dot(directionToTarget, mirrorNormal) * mirrorNormal;
        reflected.position = reflectedPosition;
        
        // Reflect rotation
        Quaternion inputRotation = input.rotation;

        // Extract mirror's local basis (tangent and binormal)
        Vector3 mirrorTangent = mirrorTransform.right;  // Local right vector
        Vector3 mirrorBinormal = mirrorTransform.forward; // Local forward vector

        // Transform the input's local axes relative to the mirror
        Vector3 reflectedRight = Vector3.Reflect(inputRotation * Vector3.right, mirrorNormal);
        Vector3 reflectedUp = Vector3.Reflect(inputRotation * Vector3.up, mirrorNormal);
        Vector3 reflectedForward = Vector3.Reflect(inputRotation * Vector3.forward, mirrorNormal);

        // Create the reflected rotation from the transformed axes
        Quaternion reflectedRotation = Quaternion.LookRotation(reflectedForward, reflectedUp);
        reflected.rotation = reflectedRotation;
    }
}
