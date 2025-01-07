using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotController : MonoBehaviour
{
    private Renderer render;

    public GameObject waterBottle;
    public GameObject waterBlob;
    public GameObject bagels;

    // Start is called before the first frame update
    void Start()
    {

        render = GetComponent<Renderer>();
    }

    void targetsCollided()
    {
        ObjectStateManager waterControl = waterBottle.GetComponent<ObjectStateManager>();
        if (waterControl.isFilled())
        {
            waterControl.useWater();
            Vector3 newPosistion = transform.position;
            newPosistion.y += 1;
            Instantiate(waterBlob, newPosistion, Quaternion.identity);
            GetComponent<ObjectStateManager>().fillObject();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (render.bounds.Intersects(waterBottle.GetComponent<Renderer>().bounds) && render.bounds.Intersects(bagels.GetComponent<Renderer>().bounds))
        {
            targetsCollided();
        }
    }
}
