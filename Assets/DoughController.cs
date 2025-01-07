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
        if (render.bounds.Contains(rollingPin.transform.position) && render.bounds.Contains(bakingTray.transform.position))
        {
            targetsCollided();
        }
    }


}
