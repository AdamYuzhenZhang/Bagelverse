using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGizmo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        // Get the object's Renderer
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            // Set Gizmos color
            Gizmos.color = Color.green;

            // Draw the bounding box
            Gizmos.DrawWireCube(renderer.bounds.center, renderer.bounds.size);
        }
    }
}
