using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectStateManager : MonoBehaviour
{
    private bool _isFilled;
    private bool _isBaked;
    private bool _isBoiled;
    // Start is called before the first frame update
    void Start()
    {
        _isFilled = false;
        _isBaked = false;
        _isBoiled = false;
    }

    private string GetCurrentWorld()
    {
        // TODO
        return "";
    }

    // Update is called once per frame
    void Update()
    {
        if (GetCurrentWorld() == "water")
        {
            _isFilled = true;
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
    }

    public void fillObject()
    {
        _isFilled = true;
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
