using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStateManager : MonoBehaviour
{
    public bool _isWater;
    public bool _isBaked;
    public bool _isBoiled;
    private Renderer _renderer;

    // Start is called before the first frame update
    void Start()
    {
        //useWater();
        _isWater = gameObject.name.ToLower().Contains("water");
        _isBaked = false;
        _isBoiled = false;
        _renderer = GetComponent<Renderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        ObjectStateManager state = collision.gameObject.GetComponent<ObjectStateManager>(); // Should Boil Bagels
        bool shouldBoil = collision.gameObject.tag == "fire" && _isWater && !_isBoiled; // Should boil water
        if (shouldBoil || (state && state.isBoiled()))
        {
            Boil();
        }
        else if (collision.gameObject.tag == "fire" && !_isWater && !_isBaked)
        {
            Bake();
        }
        
        //if (_isBoiled && !GetComponent<SteamGenerator>().IsSteamActive())
        //{
        //    //GetComponent<SteamGenerator>().SteamOn();
        //}

    }

    private void Update()
    {
        if (transform.position.y < -5)
        {
            Destroy(gameObject);
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
        }
        _isBaked = true;
    }
}
