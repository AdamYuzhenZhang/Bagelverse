using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotController : MonoBehaviour
{
    private Renderer render;

    public GameObject waterBottle;
    public GameObject waterBlob;
    public GameObject bagels;
    public int amountOfWater;

    // Start is called before the first frame update
    void Start()
    {
        if (amountOfWater <= 0) amountOfWater = 300;
        render = GetComponent<Renderer>();
    }

    void fillPot()
    {
        ObjectStateManager waterControl = waterBottle.GetComponent<ObjectStateManager>();
        if (waterControl.isFilled())
        {
            waterControl.useWater();
            Vector3 newPosistion = transform.position;
            newPosistion.y += 1;
            for (int i = 0; i < amountOfWater; i++)
            {
                newPosistion.x += (i % 4 * .1f);
                GameObject waterDrop = Instantiate(waterBlob, newPosistion, Quaternion.identity);
                waterDrop.transform.localScale = new Vector3(.1f, .1f, .1f);
            }
            GetComponent<ObjectStateManager>().fillObject();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (render.bounds.Intersects(waterBottle.GetComponent<Renderer>().bounds))
        {
            fillPot();
        }
    }
}
