using UnityEngine;
using UnityEngine.AI;
public class IAMechant : MonoBehaviour
{

    public string color;
    public GameObject panier;
    public GameObject[] listPanier;
    public Transform startMarker;
    public GameObject endMarkerObject;
    public Transform endMarker;
    private NavMeshAgent agent;
    public Vector3 final;

    void Start()
    {
        listPanier = GameObject.FindGameObjectsWithTag("panier");
        panier = FindPanierByColor(color);

        endMarkerObject = FindSecurity(panier.GetComponent<PanContent>().Sys);
        endMarker = endMarkerObject.GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

    }
	void Update()
    {
        // final = new Vector3(6.1F, -1.9F, 0.12F);
        final = endMarker.localPosition;
        agent.SetDestination(final);
    }
    
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.tag == "systeme") && (other.gameObject.GetComponent<DefensePanier>().color == color)
                                                && (other.gameObject == endMarkerObject))
        {
            Debug.Log(endMarker.localPosition);
            other.gameObject.GetComponent<DefensePanier>().pointDeVie = 0;
			if(FindSecurity(panier.GetComponent<PanContent>().Sys) != null)
			{
				endMarkerObject = FindSecurity(panier.GetComponent<PanContent>().Sys);
        		startMarker = GetComponent<Transform>();
        		endMarker = endMarkerObject.GetComponent<Transform>();
                Destroy(gameObject);
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
