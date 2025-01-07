using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DoughController : MonoBehaviour
{
    private Renderer render;

    public GameObject bakingTray;
    public GameObject rollingPin;
    public GameObject bagels;
    public GameObject flour;


    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<Renderer>();
        bakingTray = GameObject.FindGameObjectWithTag("tray");
        rollingPin = GameObject.FindGameObjectWithTag("rollingPin");
    }

    void targetsCollided() 
    {
       gameObject.SetActive(false);
       GameObject newBagels = Instantiate(bagels, bakingTray.transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (render.bounds.Intersects(rollingPin.GetComponent<Renderer>().bounds) && render.bounds.Intersects(bakingTray.GetComponent<Renderer>().bounds))
        {
            targetsCollided();
        }
        if (transform.position.y < -10) // Respawns flour if dough falls.
        {
            gameObject.SetActive(false);
            Instantiate(flour, new Vector3(-0.564999998f, 0.898000002f, 0.370999992f), Quaternion.identity);
        }
    }

    public void SetFlour(GameObject newFlour)
    {
        flour = newFlour;
    }


}
