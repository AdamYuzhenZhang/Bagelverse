using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using UnityEngine;

public class BakingTrayController : MonoBehaviour
{

    private GameObject bagel;
    // Start is called before the first frame update
    void Start()
    {
        bagel = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsSpilling())
        {
            bagel.transform.SetParent(null);
            bagel.GetComponent<Rigidbody>().isKinematic = false;
        }
        if (gameObject.layer == LayerMask.NameToLayer("fire") && bagel)
        {
            bagel.GetComponent<ObjectStateManager>().Bake();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.ToLower().Contains("bagel"))
        {
            collision.gameObject.transform.SetParent(gameObject.transform);
            bagel = collision.gameObject;
            collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
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
