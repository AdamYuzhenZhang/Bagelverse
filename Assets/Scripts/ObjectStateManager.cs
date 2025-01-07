using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStateManager : MonoBehaviour
{
    private bool _isFilled;
    private bool _isBaked;
    private bool _isBoiled;
    private Renderer _renderer;

    // Start is called before the first frame update
    void Start()
    {
        _isFilled = true;
        _isBaked = false;
        _isBoiled = false;
        _renderer = GetComponent<Renderer>();
    }

    private string GetCurrentWorld()
    {
        GameObject[] waters = GameObject.FindGameObjectsWithTag("water");
        foreach (GameObject water in waters) { 
            Renderer waterRender = water.GetComponent<Renderer>();
            if (waterRender && _renderer && _renderer.bounds.Intersects(waterRender.bounds)) return "water"; 
        }
        return "";
    }

    // Update is called once per frame
    void Update()
    {
        if (GetCurrentWorld() == "water")
        {
            fillObject();
            
        }
        else if (GetCurrentWorld() == "fire")
        {
            _isBaked = true;
            if (_isFilled) { _isBoiled = true; }
        }

    }

    public bool isFilled()
    {
        return _isFilled;
    }

    public void useWater()
    {
        _isFilled = false;
        _renderer.material.color = Color.gray;
    }

    public void fillObject()
    {
        _isFilled = true;
        if (gameObject.name.Contains("water") && _renderer.material.color != Color.blue)
        {
            _renderer.material.color = Color.blue;
        }
    }

    public void Boil()
    {
        _isBoiled = true;
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
        _isBaked |= true;
    }
}
