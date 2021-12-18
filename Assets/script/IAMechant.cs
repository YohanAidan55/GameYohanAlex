using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAMechant : MonoBehaviour
{

    public string color;

    public Transform startMarker;
    public GameObject endMarkerObject;
    public Transform endMarker;

    // Movement speed in units per second.
    public float speed = 0.1F;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;


    void Start()
    {
        endMarkerObject = GameObject.Find("systemeTest");
        startMarker = this.GetComponent<Transform>();
        endMarker = endMarkerObject.GetComponent<Transform>();

        // Keep a note of the time the movement started.
        startTime = Time.time;

        // Calculate the journey length.
        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
    }

    // Move to the target end position.
    void Update()
    {
        // Distance moved equals elapsed time times speed..
        float distCovered = (Time.time - startTime) * speed;

        // Fraction of journey completed equals current distance divided by total distance.
        float fractionOfJourney = distCovered / journeyLength;

        // Set our position as a fraction of the distance between the markers.
        transform.position = Vector3.Lerp(startMarker.position, endMarker.position, fractionOfJourney);
    }


    void OnTriggerEnter2D(Collider2D other)
    {   //active la funtion Move si c'est le bon panier
        if ((other.gameObject.tag == "systeme") && (other.gameObject.GetComponent<DefensePanier>().color == color))
        {
            other.gameObject.GetComponent<DefensePanier>().pointDeVie = 0;
        }
    }
}
