using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotController : MonoBehaviour
{
    private Renderer _renderer;
    bool active;
    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] waters = GameObject.FindGameObjectsWithTag("water");
        if (gameObject.layer == LayerMask.NameToLayer("fire"))
        {
            if (active) return;
            foreach (GameObject water in waters)
            {
                if (_renderer.bounds.Intersects(water.GetComponent<Renderer>().bounds))
                {
                    water.GetComponent<Rigidbody>().isKinematic = true;
                    water.transform.SetParent(gameObject.transform.parent.transform);
                    water.GetComponent<ObjectStateManager>().Boil();
                }
            }
            active = true;
        }
    }
}
