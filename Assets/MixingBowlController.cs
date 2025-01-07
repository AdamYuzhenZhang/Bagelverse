using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixingBowlController : MonoBehaviour
{
    private Renderer render;
    public GameObject flour;
    public GameObject water;
    public GameObject dough;

    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<Renderer>();
    }

    void targetsCollided()
    {
        ObjectStateManager waterControl = water.GetComponent<ObjectStateManager>();
        if (waterControl.isFilled())
        {
            waterControl.useWater();
            Vector3 newPosistion = transform.position;
            newPosistion.y += 1; // Place above mixing bowl + drops in w/ gravity.
            GameObject newDough = Instantiate(dough, newPosistion, Quaternion.identity);
            flour.SetActive(false); // Flour gets used
        }    
    }

    // Update is called once per frame
    void Update()
    {
       if (render.bounds.Contains(flour.transform.position) && render.bounds.Contains(water.transform.position))
            {
                targetsCollided();
            }
        }
}
