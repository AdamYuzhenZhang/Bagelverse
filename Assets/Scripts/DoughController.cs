using System.Collections;
using System.Collections.Generic;
using Meta.Voice.Audio;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class DoughController : MonoBehaviour
{
    public GameObject bagelModel;
    private GameObject flourModel;
    private bool contactWithTray;

    // Start is called before the first frame update
    void Start()
    {
        contactWithTray = false;
    }

    void DoughToBagel() 
    {
       GameObject newBagel = Instantiate(bagelModel, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform child in transform) // Destroy dough after falling.
        {
            if (child.name.ToLower().Contains("aramture") && child.position.y < -10) {
                GetComponent<AudioPlayer>().Play();
                Destroy(gameObject);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("rollingPin") && contactWithTray)
        {
            DoughToBagel();
        }
        else if (collision.gameObject.name.Contains("tray"))
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
        flourModel = newFlour;
    }


}
