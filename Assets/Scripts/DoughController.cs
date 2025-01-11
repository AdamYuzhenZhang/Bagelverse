using UnityEngine;

public class DoughController : MonoBehaviour
{
    public GameObject bagelModel;
    private GameObject flourModel;
    private bool contactWithTray;
    private bool destroy;

    // Start is called before the first frame update
    void Start()
    {
        contactWithTray = false;
        destroy = false;
    }

    void DoughToBagel() 
    {
        GameObject newBagel = Instantiate(bagelModel, transform.position, Quaternion.identity);
        destroy = true;
    }

    // Update is called once per frame
    void Update()
    {
        AudioPlayer player = GetComponent<AudioPlayer>();
        if (!destroy && transform.position.y < .3) // Destroy dough after falling.
        {
            player.SetAudio(1);
            player.Play();
            destroy = true;
         
        }
        if (destroy && !player.IsPlaying())
        {
            print("Destroy: " + transform.parent.gameObject.name);
            Destroy(transform.parent.gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.ToLower().Contains("rollingpin") && contactWithTray)
        {

            DoughToBagel();
        }
        else if (collision.gameObject.name.ToLower().Contains("tray"))
        {
            contactWithTray = true;
        }
    }



    private void OnCollisionStay(Collision collision)
    {

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name.ToLower().Contains("tray"))
        {
            contactWithTray = false;
        }
    }

    public void SetFlour(GameObject newFlour)
    {
        flourModel = newFlour;
    }


}
