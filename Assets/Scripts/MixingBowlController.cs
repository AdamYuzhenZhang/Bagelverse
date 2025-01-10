using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixingBowlController : MonoBehaviour
{
    private Renderer render;
    public GameObject flour;
    public GameObject dough;
    private bool activated;

    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<Renderer>();
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

    void makeDough()
    {
        Vector3 newPosistion = transform.position;
        newPosistion.y += 1; // Place above mixing bowl + drops in w/ gravity.
        GameObject newDough = Instantiate(dough, newPosistion, Quaternion.identity);
        //newDough.GetComponent<DoughController>().SetFlour(flour);
    }

    // Update is called once per frame
    void Update()
    {
        if (render.bounds.Intersects(flour.GetComponent<Renderer>().bounds) && activated == false)
        {
            GameObject[] waters = GameObject.FindGameObjectsWithTag("water");
            print("number of waters: " + waters.Length);
            foreach (GameObject water in waters)
            {
                if (render.bounds.Intersects(water.GetComponent<Renderer>().bounds))
                {
                    if (!activated)
                    {
                        makeDough();
                        activated = true;
                        Destroy(water);
                    }
                    else
                    {
                        print("destroying left over water");
                        Destroy(water);
                    }

                }
            }
            activated = false;
        }
    }
}
