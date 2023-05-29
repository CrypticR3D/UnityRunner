using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    private bool heart1InBounds;
    private bool heart2InBounds;
    public ParticleSystem ending;

    public GameObject P1;
    public GameObject P2;

    public GameObject endText;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Heart_1")
        {
            heart1InBounds = true;
        }
        else if (other.gameObject.name == "Heart_2")
        {
            heart2InBounds = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Heart_1")
        {
            heart1InBounds = false;
        }
        else if (other.gameObject.name == "Heart_2")
        {
            heart2InBounds = false;
        }
    }

    void Update()
    {
        if (heart1InBounds && heart2InBounds)
        {
            Debug.Log("Ending");
            ending.enableEmission = true;
            P1.GetComponent<PlayerController>().enabled = false;
            P2.GetComponent<PlayerController>().enabled = false;

            endText.SetActive(true);
            // Effects to apply go here.
        }
    }
}
