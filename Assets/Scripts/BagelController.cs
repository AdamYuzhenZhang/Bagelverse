using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagelController : MonoBehaviour
{

    private ObjectStateManager state;
    private Renderer render;
    private GameObject bakingTray;
    public Material bakedMaterial;
    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<Renderer>();
        state = GetComponent<ObjectStateManager>();
        bakingTray = GameObject.FindGameObjectWithTag("bakingTray");
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] waters = GameObject.FindGameObjectsWithTag("water");

        if (!state.isBoiled()) foreach (GameObject water in waters)
        {
            ObjectStateManager waterController = water.GetComponent<ObjectStateManager>();
            if (render.bounds.Intersects(water.GetComponent<Renderer>().bounds) && waterController.isBoiled())
            {
                state.Boil();
                state.UnBake();
            }
        }

        if (state.isBoiled())
        {
            if (render.bounds.Intersects(bakingTray.GetComponent<Renderer>().bounds) && state.isBaked())
            {
                gameObject.GetComponent<Renderer>().material = bakedMaterial;
            }
        }
    }
}
