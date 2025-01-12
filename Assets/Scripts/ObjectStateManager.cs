using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStateManager : MonoBehaviour
{
    public bool _isWater;
    public bool _isBaked;
    public bool _isBoiled;
    private bool locked;
    private bool soundPlayed;
    private Renderer _renderer;
    private Material OGMaterial;
    public GameObject confetti;
    public GameObject parentTransform;

    // Start is called before the first frame update
    void Start()
    {
        //useWater();
        _isWater = gameObject.name.ToLower().Contains("water");
        _isBaked = false;
        _isBoiled = false;
        locked = false;
        soundPlayed = false;
        _renderer = GetComponent<Renderer>();
        OGMaterial = new Material(_renderer.material);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (locked) return;
        ObjectStateManager state = collision.gameObject.GetComponent<ObjectStateManager>(); // Should Boil Bagels
        bool shouldBoil = (collision.gameObject.tag == "fire") && _isWater && !_isBoiled; // Should boil water
        if (shouldBoil || (state && state.isBoiled()))
        {
            Boil();
        }
        else if (collision.gameObject.tag == "fire" && !_isWater && !_isBaked)
        {
            Bake();
        }

    }

    private void Update()
    {
        if (gameObject.name.ToLower().Contains("bagel") && !_isBoiled && !_isBaked)
        {
            GameObject[] waters = GameObject.FindGameObjectsWithTag("water");
            foreach (GameObject water in waters)
            {
                if (_renderer.bounds.Intersects(water.GetComponent<Renderer>().bounds))
                {
                    Boil();
                    break;
                }
            }
        }
        if (gameObject.layer == LayerMask.NameToLayer("fire") && _isWater && !_isBoiled)
        {
            Boil();
        }
        if (transform.position.y < -5)
        {
            Destroy(gameObject);
        }
        if (gameObject.name.ToLower().Contains("bagel") && !soundPlayed)
        {
            if (GetComponent<AudioPlayer>()) GetComponent<AudioPlayer>().Play();
            soundPlayed = true;
        } 
    }


    public void Boil()
    {
        _isBoiled = true;
        _isWater = false;
        if (gameObject.name.ToLower().Contains("bagel"))
        {
            // TODO: Raw Bagel Model
            _renderer.material.color = Color.gray;
            GetComponent<AudioPlayer>().SetAudio(2);
            GetComponent<AudioPlayer>().Play();
        }
        if (gameObject.name.ToLower().Contains("water")) // Visual change for boiled water.
        {
            _renderer.material.color = Color.white;
            //gameObject.GetComponent<SphereJiggle>().StartJiggle();
            //gameObject.GetComponent<SteamGenerator>().SetSteamColor(Color.white);
        }
    }

    public bool isBoiled()
    {
        return _isBoiled;
    }

    public bool isBaked()
    {
        return _isBaked;
    }

    public void UnBake()
    {
       _isBaked = false;
    }

    public void Bake()
    {
        if (locked) return;
        print(gameObject.name + " baking");
        if (gameObject.name.ToLower().Contains("bagel") && isBoiled())
        {
            // TODO: Baked Bagel Model
            // gameObject.SetActive(false);
            ////GameObject newbagel = Instantiate(bakedBagel, transform);
            // newbagel.transform.SetParent(gameObject.transform.parent);
            // newbagel.GetComponent<Rigidbody>().isKinematic = true;
            _renderer.material = OGMaterial;
            foreach (Transform child in confetti.transform)
            {
                Vector3 offsetPosistion = transform.position;
                offsetPosistion.x += Random.Range(0f, .2f);
                offsetPosistion.y += Random.Range(.2f, .4f);
                offsetPosistion.z += Random.Range(0f, .2f);
                Instantiate(child.gameObject, offsetPosistion, Quaternion.identity);
            }
            GetComponent<AudioPlayer>().SetAudio(3);
            GetComponent<AudioPlayer>().Play();
            locked = true;

        }
        _isBaked = true;
    }
}
