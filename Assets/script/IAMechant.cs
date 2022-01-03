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
    private float distFinal;
    void Start()
    {
        listPanier = GameObject.FindGameObjectsWithTag("panier");
        panier = FindPanierByColor(color);
        
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

    }
	void Update()
    {
        endMarkerObject = FindSecurity(panier.GetComponent<PanContent>().Sys);
        endMarker = endMarkerObject.GetComponent<Transform>();
        final = endMarker.position;
        final.z = 0;
        agent.SetDestination(final);
    }
    
    
    void OnTriggerStay2D(Collider2D other)
    {
        if ((other.gameObject.tag == "systeme") && (other.gameObject.GetComponent<DefensePanier>().color == color)
                                                && (other.gameObject == endMarkerObject))
        {
            other.gameObject.GetComponent<DefensePanier>().pointDeVie -= 2;
        }
    }
    
    GameObject FindSecurity(GameObject[] tab)
    {
        GameObject result;
        GameObject resultFinal = null;
        float dist = 10000F;
        for (int i = 0; i < tab.Length; i++)  
        {
            if (panier.GetComponent<PanContent>().Sys[i].GetComponent<DefensePanier>().pointDeVie > 0)
            {
                result = panier.GetComponent<PanContent>().Sys[i];
                distFinal = Vector3.Distance(gameObject.transform.position, result.transform.position);
                
                if (distFinal < dist)
                {
                    dist = distFinal;
                    resultFinal = panier.GetComponent<PanContent>().Sys[i];
                }
                //break;
            }
        }
        return resultFinal;
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
