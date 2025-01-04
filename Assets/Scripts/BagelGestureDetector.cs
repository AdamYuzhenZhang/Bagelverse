using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Detecting the bagel gesture
public class BagelGestureDetector : MonoBehaviour
{
    [SerializeField] private RigTracker rigTracker;
    // How close the fingers should be
    [SerializeField] private float proximityThreshold = 0.02f;
    // How full the circle should be
    [SerializeField] private float distanceThreshold = 0.08f;

    [SerializeField] private bool bagelDetected = false;
    private bool animationStarted = false;
    
    [SerializeField] private GameObject firstBagel;
    [SerializeField] private GameObject gameScene;
    

    private void Start()
    {
        // hide game scene and first bagel
        firstBagel.SetActive(false);
        gameScene.SetActive(false);
    }

    // Comments are used for debugging joint positions! Not used anymore
    /*
    private GameObject leftI;
    private GameObject leftIB;
    private GameObject rightI;
    private GameObject rightIB;
    private GameObject leftT;
    private GameObject rightT;

    private void Start()
    {
        leftI = GameObject.CreatePrimitive(PrimitiveType.Cube);
        leftI.transform.localScale = Vector3.one * 0.02f;
        rightI = GameObject.CreatePrimitive(PrimitiveType.Cube);
        rightI.transform.localScale = Vector3.one * 0.02f;
        leftIB = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        leftIB.transform.localScale = Vector3.one * 0.02f;
        rightIB = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        rightIB.transform.localScale = Vector3.one * 0.02f;
        leftT = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        leftT.transform.localScale = Vector3.one * 0.02f;
        rightT = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        rightT.transform.localScale = Vector3.one * 0.02f;
    }
    */

    private void Update()
    {
        // check if the index tips and thumb tips are close to each other
        // make sure the index and thumbs are not too close to each other, and index base are not too close

        if (rigTracker.handBonesInitialized)
        {
            Vector3 leftIndex = rigTracker.leftHandBones[OVRSkeleton.BoneId.XRHand_IndexTip].position;
            Vector3 leftIndexBase = rigTracker.leftHandBones[OVRSkeleton.BoneId.XRHand_IndexProximal].position;
            Vector3 rightIndex = rigTracker.rightHandBones[OVRSkeleton.BoneId.XRHand_IndexTip].position;
            Vector3 rightIndexBase = rigTracker.rightHandBones[OVRSkeleton.BoneId.XRHand_IndexProximal].position;
            Vector3 leftThumb = rigTracker.leftHandBones[OVRSkeleton.BoneId.XRHand_ThumbTip].position;
            Vector3 rightThumb = rigTracker.rightHandBones[OVRSkeleton.BoneId.XRHand_ThumbTip].position;

            float indexDist = Vector3.Distance(leftIndex, rightIndex);
            float thumbDist = Vector3.Distance(leftThumb, rightThumb);
            float indexThumbDist = Vector3.Distance(leftIndex, rightThumb);
            float indexBaseDist = Vector3.Distance(leftIndexBase, rightIndexBase);

            bagelDetected = indexDist < proximityThreshold && thumbDist < proximityThreshold &&
                            indexThumbDist > distanceThreshold && indexBaseDist > distanceThreshold;
            
            /*
            print("-----------------------");
            print("Bagel! " + bagelDetected);
            print("LeftIndex " + leftIndex);
            print("RightIndex " + rightIndex);
            print("indexDist " + indexDist);
            print("thumbDist " + thumbDist);
            print("indexThumbDist " + indexThumbDist);
            print("indexBaseDist " + indexBaseDist);
            
            leftI.transform.position = leftIndex;
            leftIB.transform.position = leftIndexBase;
            rightI.transform.position = rightIndex;
            rightIB.transform.position = rightIndexBase;
            leftT.transform.position = leftThumb;
            rightT.transform.position = rightThumb;
            */
            
            if (bagelDetected & !animationStarted)
            {
                animationStarted = true;
                // start an animation coroutine
                StartCoroutine(BagelAnimation());

            }
        }
    }
    
    // a simple animation placeholder
    private IEnumerator BagelAnimation()
    {
        Debug.Log("Animation Started!");
        // make first bagel active
        firstBagel.SetActive(true);
        // update first bagel position and rotation with the hand position and rotation
        // position is the mid between left index and right thumb
        Vector3 leftIndex = rigTracker.leftHandBones[OVRSkeleton.BoneId.XRHand_IndexTip].position;
        Vector3 rightThumb = rigTracker.rightHandBones[OVRSkeleton.BoneId.XRHand_ThumbTip].position;
        Vector3 pos = (leftIndex + rightThumb) / 2.0f;
        firstBagel.transform.position = pos;
        // rotate to face player
        Vector3 toPlayer = (Camera.main.transform.position - pos).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.up, toPlayer);
        firstBagel.transform.rotation = targetRotation;
        Debug.Log("Bagel Created");
        
        // after a delay
        yield return new WaitForSeconds(3f);
            
        // set bagel inactive, enable the scene
        firstBagel.SetActive(false);
        gameScene.SetActive(true);
        // wait another frame
        yield return null;
        Debug.Log("Animation Done");
        // disable this detector
        this.enabled = false;
    }
    
}
