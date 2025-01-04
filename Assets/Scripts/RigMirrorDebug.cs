using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigMirrorDebug : MonoBehaviour
{
    [SerializeField] private RigTracker rigTracker;
    [SerializeField] private Transform mirrorTransform;

    [SerializeField] private Transform reflectedHead;
    [SerializeField] private GameObject reflectedLeftHand;
    [SerializeField] private GameObject reflectedRightHand;
    
    private Dictionary<OVRSkeleton.BoneId, GameObject> leftHandDebugCubes = new Dictionary<OVRSkeleton.BoneId, GameObject>();
    private Dictionary<OVRSkeleton.BoneId, GameObject> rightHandDebugCubes = new Dictionary<OVRSkeleton.BoneId, GameObject>();
    
    [SerializeField] private float cubeSize = 0.01f;
    
    public bool handsInitialized = false;

    private void Update()
    {
        // Get transforms
        Transform headAnchor = rigTracker.headAnchor;
        Transform leftHandAnchor = rigTracker.leftHandAnchor;
        Transform rightHandAnchor = rigTracker.rightHandAnchor;
        
        // Calculate and assign mirrored transforms
        Reflect(headAnchor, reflectedHead);
        Reflect(leftHandAnchor, reflectedLeftHand.transform);
        Reflect(rightHandAnchor, reflectedRightHand.transform);

        if (!handsInitialized && rigTracker.handBonesInitialized)
        {
            // initialize hands debug bones
            InitHandsDebugger();
            handsInitialized = true;
        }

        if (handsInitialized)
        {
            // update transforms
            UpdateTransform(rigTracker.leftHandBones, leftHandDebugCubes);
            UpdateTransform(rigTracker.rightHandBones, rightHandDebugCubes);
        }
    }

    private void InitHandsDebugger()
    {
        DebugCubesCreator(rigTracker.leftHandBones, leftHandDebugCubes, reflectedLeftHand);
        DebugCubesCreator(rigTracker.rightHandBones, rightHandDebugCubes, reflectedRightHand);
    }

    private void DebugCubesCreator(Dictionary<OVRSkeleton.BoneId, Transform> bones,
        Dictionary<OVRSkeleton.BoneId, GameObject> cubes, GameObject parent)
    {
        int layer = parent.layer;  // set the same layer
        foreach (var b in bones)
        {
            GameObject c = GameObject.CreatePrimitive(PrimitiveType.Cube);
            c.transform.localScale = Vector3.one * cubeSize;
            c.layer = layer;
            Reflect(b.Value, c.transform);
            c.transform.parent = parent.transform;
            cubes[b.Key] = c;
        }
    }

    private void UpdateTransform(Dictionary<OVRSkeleton.BoneId, Transform> bones,
        Dictionary<OVRSkeleton.BoneId, GameObject> cubes)
    {
        foreach (var b in bones)
        {
            if (cubes.TryGetValue(b.Key, out GameObject c))
            {
                Reflect(b.Value, c.transform);
            }
        }
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
