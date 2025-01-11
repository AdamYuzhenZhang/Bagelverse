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
        activated = false;
    }


    void makeDough()
    {
        Vector3 newPosistion = transform.position;
        newPosistion.y += 1; // Place above mixing bowl + drops in w/ gravity.
        GameObject newDough = Instantiate(dough, newPosistion, Quaternion.identity);
        GetComponent<AudioPlayer>().Play();
        //newDough.GetComponent<DoughController>().SetFlour(flour);
    }

    // Update is called once per frame
    void Update()
    {
        if (render.bounds.Intersects(flour.GetComponent<Renderer>().bounds))
        {
            if (activated) return;
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
        } else
        {
            activated = false;
        }
    }
}
