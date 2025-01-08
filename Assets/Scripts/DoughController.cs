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
    private bool contactWithTray;

    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<Renderer>();
        bakingTray = GameObject.FindGameObjectWithTag("tray");
        rollingPin = GameObject.FindGameObjectWithTag("rollingPin");
        contactWithTray = false;
    }

    void DoughToBagel() 
    {
       gameObject.SetActive(false);
       GameObject newBagel = Instantiate(bagels, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (render.bounds.Intersects(rollingPin.GetComponent<Renderer>().bounds) && !contactWithTray)
        {
            DoughToBagel();
        }
        if (transform.position.y < -10) // Respawns flour if dough falls.
        {
            gameObject.SetActive(false);
            Instantiate(flour, new Vector3(-0.564999998f, 0.898000002f, 0.370999992f), Quaternion.identity);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.name.Contains("tray"))
        {
            contactWithTray = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name.Contains("tray"))
        {
            contactWithTray = false;
        }
    }

    public void SetFlour(GameObject newFlour)
    {
        flour = newFlour;
    }


}
