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
            GetComponent<AudioPlayer>().Play();
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
        print(gameObject.name + " baking");
        if (gameObject.name.ToLower().Contains("bagel") && isBoiled() && _renderer.material.color != Color.black)
        {
            // TODO: Baked Bagel Model
            _renderer.material.color = Color.black;
            GetComponent<AudioPlayer>().SetAudio(3);
            GetComponent<AudioPlayer>().Play();
            locked = true;

        }
        _isBaked = true;
    }
}
