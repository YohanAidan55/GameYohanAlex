using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanContent : MonoBehaviour
{

    public string color;
    public List<GameObject> Pan;
    public GameObject[] Sys = new GameObject[3];     //liste des syst�mes du panier

    public bool isDestroy;   //passe � true si un syst�me est d�truit pour �viter l'occurence

    // Start is called before the first frame update
    void Start()
    {
        isDestroy = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Pan.Count == 10)   //Si le tableau est rempli / il y a 10 fantomes dans le panier
        {
            Destroy(Pan[0]);
            for(int i = 0; i < Pan.Count - 1; i++)
            {
                Pan[i] = Pan[i + 1];
            }
            Pan.Remove(Pan[9]);
        }

        if (isDestroy)
        {
            if(destruct(Sys) == 1)
            {
                StartCoroutine(CoroutineLose());
            }

            isDestroy = false;
        }
    }
    
    IEnumerator CoroutineLose()
    {
        yield return new WaitForSeconds(1);
        DestroyFantome(Pan);
        Destroy(gameObject);
        GameObject.Find("touch").GetComponent<touchFantome>().LunchLose();
    }

    int destruct(GameObject[] tab)
    {
        for (int i = 0; i < tab.Length; i++)             //parcours les syt�mes du panier
        {
            if(tab[i].GetComponent<DefensePanier>().pointDeVie > 0)
            {
                return 0;
            }
        }
        return 1;    //rertourne 1 si le tableau est vide
    }

    void DestroyFantome(List<GameObject> tab)       //Detruit les fantomes dans le panier
    {
        for (int i = 0; i < tab.Count - 1; i++)             //parcours les syt�mes du panier
        {
            Destroy(tab[i].gameObject);
        }
    }


    public void resetRotation()        //fantomation rotation à 0 lorsque bonus melangePanier est active
    {

        for (int i = 0; i < Pan.Count; i++)             //parcours les syt�mes du panier
        {
            Pan[i].gameObject.transform.localRotation = this.gameObject.transform.rotation;
        }
    }


}
