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

    private string GetCurrentWorld()
    {
        GameObject fire = GameObject.FindGameObjectWithTag("fire");
        Renderer fireRender = fire.GetComponent<Renderer>();

        //Debug.Log(fireRender == null);
        if (_renderer.bounds.Intersects(fireRender.bounds))
        {
            return "fire";
        }

        GameObject[] waters = GameObject.FindGameObjectsWithTag("water");
        foreach (GameObject water in waters) { 
            Renderer waterRender = water.GetComponent<Renderer>();
            ObjectStateManager state = water.GetComponent<ObjectStateManager>();
            if (waterRender && _renderer && _renderer.bounds.Intersects(waterRender.bounds)) {
                if (state.isBoiled()) return "hot_water";
                return "water"; 
            } 
        }
        
        return "kitchen";
    }

    // Update is called once per frame
    void Update()
    {
        string world = GetCurrentWorld();
        if (world == "fire")
        {
            Bake();
        }
        else if (world == "water")
        {
            fillObject();
            
        } else if (world == "hot_water")
        {
            Boil();
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

    public void fillObject()
    {
        if (_isBoiled)
        {
            return;
        }
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
        Debug.Log("Boiling: " + gameObject.name);
        if (gameObject.name.Contains("bagel"))
        {
            _renderer.material.color = Color.gray;
        }
        if (gameObject.name.Contains("water")) // Visual change for boiled water.
        {
            _renderer.material.color = Color.white;
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
        _isBaked |= true;
        if (_isFilled && !_isBoiled) {
            Boil(); 
        }
    }
}
