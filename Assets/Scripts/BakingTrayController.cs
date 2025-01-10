using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakingTrayController : MonoBehaviour
{

    private Transform bagelTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsSpilling())
        {
            bagelTransform.SetParent(null);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.ToLower().Contains("bagel"))
        {
            collision.gameObject.transform.SetParent(gameObject.transform);
            bagelTransform = collision.gameObject.transform;
        }
    }

    private bool IsSpilling()
    {
        if (Vector3.Dot(transform.up, Vector3.up) < 0)
        {
            // The object is rotated more than 90 degrees
            return true;
        }
        return false;

    }
}
