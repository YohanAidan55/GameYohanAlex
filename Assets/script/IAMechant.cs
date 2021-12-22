using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class IAMechant : MonoBehaviour
{

    public string color;
    public GameObject panier;
    public GameObject[] listPanier;
    public Transform startMarker;
    public GameObject endMarkerObject;
public GameObject startMarkerObject;
    public Transform endMarker;
	public bool colliding = false;
	public float speed = 0.5F;
	private NavMeshAgent agent;


    void Start()
    {
        listPanier = GameObject.FindGameObjectsWithTag("panier");
        panier = FindPanierByColor(color);

        endMarkerObject = FindSecurity(panier.GetComponent<PanContent>().Sys);
        startMarker = this.GetComponent<Transform>();
        endMarker = endMarkerObject.GetComponent<Transform>();
		//agent = this.AddComponent<NavMeshAgent>();
		agent = this.GetComponent<NavMeshAgent>();
    }

    // Move to the target end position.
    void Update()
    {
    /* if(!colliding)
		{
			float step = speed * Time.deltaTime;
			transform.position = Vector2.MoveTowards(startMarker.position, endMarker.position, step);
		//}
    }*/
	//agent.SetDestination(endMarker.position);
}
    void OnTriggerEnter2D(Collider2D other)
    {
		colliding = true;
        if ((other.gameObject.tag == "systeme") && (other.gameObject.GetComponent<DefensePanier>().color == color)
                                                && (other.gameObject == endMarkerObject))
        {
            other.gameObject.GetComponent<DefensePanier>().pointDeVie = 0;
			if(FindSecurity(panier.GetComponent<PanContent>().Sys) != null)
			{
				endMarkerObject = FindSecurity(panier.GetComponent<PanContent>().Sys);
        		startMarker = this.GetComponent<Transform>();
        		endMarker = endMarkerObject.GetComponent<Transform>();
				float step = speed * Time.deltaTime;
			transform.position = Vector2.MoveTowards(startMarker.position, endMarker.position, step);
        	
        		//Destroy(this.gameObject);
			}
        }
    }
    
    GameObject FindSecurity(GameObject[] tab)
    {
        GameObject result = null;
        for (int i = 0; i < tab.Length; i++)  
        {
            if (panier.GetComponent<PanContent>().Sys[i].GetComponent<DefensePanier>().pointDeVie > 0)
            {
                result = panier.GetComponent<PanContent>().Sys[i];
                break;
            }
        }
        return result;
    }
    
    GameObject FindPanierByColor(string color) {
        GameObject result = null;
        for (int i = 0; i < listPanier.Length; i++)
        {
            if (listPanier[i].GetComponent<PanContent>().color == color) {
                result = listPanier[i];
                break;
            }
        }
        return result;
    }
}
