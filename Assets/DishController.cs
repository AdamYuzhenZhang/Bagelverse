using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishController : MonoBehaviour
{
    public GameObject confetti;
    public Transform parentTransform;
    private bool soundPlayed;

    // Start is called before the first frame update
    void Start()
    {
        soundPlayed = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //print("dish collide w " + collision.gameObject.name);
        //ObjectStateManager state = collision.gameObject.GetComponent<ObjectStateManager>();
        //if (collision.gameObject.name.ToLower().Contains("bagel") && !soundPlayed)
        //{
        //    collision.transform.SetParent(null);
        //    foreach (Transform child in confetti.transform)
        //    {
        //        Vector3 offsetPosistion = parentTransform.position;
        //        offsetPosistion.x += Random.Range(0f, .2f);
        //        offsetPosistion.y += Random.Range(.2f, .4f);
        //        offsetPosistion.z += Random.Range(0f, .2f);
        //        Instantiate(child.gameObject, offsetPosistion, Quaternion.identity);
        //    }
        //    GetComponent<AudioPlayer>().Play();
        //    soundPlayed = true;
        }
    }

