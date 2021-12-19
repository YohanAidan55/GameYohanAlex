using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanContent : MonoBehaviour
{

    public string color;

    public GameObject[] Pan = new GameObject[10];    //liste avec les 10 fantomes dans le panier
    public GameObject[] Sys = new GameObject[4];     //liste des syst�mes du panier

    public bool isDestroy;   //passe � true si un syst�me est d�truit pour �viter l'occurence

    // Start is called before the first frame update
    void Start()
    {
        isDestroy = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Pan[9] != null)   //Si le tableau est rempli / il y a 10 fantomes dans le panier
        {
            Destroy(Pan[0]);
            for(int i = 0; i < Pan.Length - 2; i++)
            {
                Pan[i] = Pan[i + 1];
            }
            Pan[9] = null;
        }

        if (isDestroy)
        {
            if(destruct(Sys) == 1)
            {               
                DestroyFantome(Pan);
                Destroy(this.gameObject);
                SceneManager.LoadScene("menuLose");
            }

            isDestroy = false;
        }
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

    void DestroyFantome(GameObject[] tab)       //Detruit les fantomes dans le panier
    {
        for (int i = 0; i < tab.Length - 1; i++)             //parcours les syt�mes du panier
        {
            Destroy(tab[i].gameObject);
        }
    }

}
