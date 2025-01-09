using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStateManager : MonoBehaviour
{
    public bool _isFilled;
    public bool _isBaked;
    public bool _isBoiled;
    private Renderer _renderer;

    // Start is called before the first frame update
    void Start()
    {
        //useWater();
        _isFilled = gameObject.name.Contains("water");
        _isBaked = false;
        _isBoiled = false;
        _renderer = GetComponent<Renderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        ObjectStateManager state = collision.gameObject.GetComponent<ObjectStateManager>();
        bool shouldBoil = collision.gameObject.tag == "fire" && _isFilled && !_isBoiled;
        if (shouldBoil || (state && state.isBoiled()))
        {
            Boil();
        }
        else if (collision.gameObject.tag == "fire" && !_isFilled && !_isBaked)
        {
            Bake();
        }
        else if (collision.gameObject.tag == "water" && !_isFilled)
        {
            Fill();
        }
        
        if (_isBoiled && !GetComponent<SteamGenerator>().IsSteamActive())
        {
            GetComponent<SteamGenerator>().SteamOn();
        }

    }

    private void Update()
    {
        if (transform.position.y < -5)
        {
            Destroy(gameObject);
        }
    }

    public bool isFilled()
    {
        return _isFilled;
    }

    public void useWater()
    {
        _isFilled = false;
        if (gameObject.name.Contains("water"))
            _renderer.material.color = Color.gray;
    }

    public void Fill()
    {
        //if (_isBoiled)
        //{
        //    return;
        //}
        _isBoiled = false;
        _isFilled = true;
        if (gameObject.name.Contains("water") && _renderer.material.color != Color.blue)
        {
            _renderer.material.color = Color.blue;
        }
        //Debug.Log("testing: " + gameObject.name + " filled");
    }

    public void Boil()
    {
        _isBoiled = true;
        _isFilled = false;
        if (gameObject.name.Contains("bagel"))
        {
            _renderer.material.color = Color.gray;
        }
        if (gameObject.name.Contains("water")) // Visual change for boiled water.
        {
            _renderer.material.color = Color.white;
            //gameObject.GetComponent<SphereJiggle>().StartJiggle();
            gameObject.GetComponent<SteamGenerator>().SetSteamColor(Color.white);
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
        if (gameObject.name.Contains("bagel") && isBoiled() && _renderer.material.color != Color.black)
        {
            _renderer.material.color = Color.black;
        }
        _isBaked = true;
    }
}
