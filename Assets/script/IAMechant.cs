using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAMechant : MonoBehaviour
{

    public string color;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    void OnTriggerStay2D(Collider2D other)
    {   //active la funtion Move si c'est le bon panier
        if ((other.gameObject.tag == "systeme") && (other.gameObject.GetComponent<DefensePanier>().color == color))
        {
            other.gameObject.GetComponent<DefensePanier>().pointDeVie -= 1;
        }
    }
}
