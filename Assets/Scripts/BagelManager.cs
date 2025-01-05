using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Help manages a bagel portal
public class BagelManager : MonoBehaviour
{
    // Where the portal is
    public Transform portalTransform;
    public float portalDiameter = 1f;

    // The hand with colliders
    public GameObject leftHand;
    public GameObject rightHand;
    
    // the mirror layer
    [SerializeField] private GameObject mirror;
    public int mirrorLayer;

    private void Start()
    {
        // get mirror layer
        mirrorLayer = mirror.layer;
    }
}
