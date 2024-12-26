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

}
