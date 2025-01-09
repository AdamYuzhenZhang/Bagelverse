using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using Unity.VisualScripting;
using UnityEngine;

public class WaterBottleController : MonoBehaviour
{
    public GameObject waterDrop;
    public float waterSpawnPoint; // How far from the center of the bottle the drops spawn.
    public int fillRate;
    public int spillRate;
    public int waterLimit;

    private bool inWater;
    private float waterAmount;
    private Renderer _renderer;


    // Start is called before the first frame update
    void Start()
    {
        waterAmount = 0;
        _renderer = GetComponent<Renderer>();
        _renderer.material.color = Color.gray;
    }

    // Update is called once per frame
    void Update()
    {
        FillAndSpill();
        ChangeColor();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "water" && !other.gameObject.name.Contains("Drop"))
        {
            inWater = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "water" && !other.gameObject.name.Contains("Drop"))
        {
            inWater = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "water" && !collision.gameObject.name.Contains("Drop"))
        {
            inWater = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "water" && !collision.gameObject.name.Contains("Drop"))
        {
            inWater = false;
        }
    }

    private void ChangeColor()
    {
        if (_renderer.material.color != Color.blue && waterAmount >= waterLimit)
        {
            _renderer.material.color = Color.blue;
        }
        if (waterAmount > 0 && waterAmount < waterLimit)
        {
            float percent = waterAmount / waterLimit;
            Color blue = Color.blue * percent;
            Color gray = Color.gray * (1 - percent);
            _renderer.material.color = blue + gray;
        }
        if (_renderer.material.color != Color.gray && waterAmount <= 0)
        {
            _renderer.material.color = Color.gray;
        }
    }

    private bool IsSpilling()
    {
        if (Vector3.Dot(transform.up, Vector3.up) < 0)
        {
            // The object is rotated more than 90 degrees
            return true;
        }
        return false;

    }

    private void FillAndSpill()
    {
        if (IsSpilling() && waterAmount > 0) // Spill Water.
        {
            Vector3 capPosition = transform.position + transform.up * waterSpawnPoint;
            for (int i = 0; i < spillRate; i++)
            {
                float yOffset = i * .05f;
                float xOffset = i * .05f;
                capPosition.y += yOffset;
                Instantiate(waterDrop, capPosition, Quaternion.identity);
            }
            waterAmount -= spillRate;
        }
        if (inWater && waterAmount < waterLimit)
        { // Fill Water.
            waterAmount += fillRate;
        }
    }


}
