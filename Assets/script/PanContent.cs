using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if(Pan.Count == 6)   //Si le tableau est rempli / il y a 6 fantomes dans le panier
        {
            Pan[0].GetComponent<fantomeScript>().enabled = false;
            Destroy(Pan[0]);
            for(int i = 0; i < Pan.Count - 1; i++)
            {
                Pan[i] = Pan[i + 1];
            }
            Pan.Remove(Pan[5]);
        }

        if (isDestroy)
        {
            if(destruct(Sys) == 1)
            {
                CoroutineLose();
            }

            isDestroy = false;
        }
    }
    
    public void CoroutineLose()
    {
        //yield return new WaitForSeconds(1);
        DestroyFantome(Pan);
        gameObject.GetComponent<SpriteRenderer>().sprite = null;
        //SceneManager.LoadScene("menuLose");
        StartCoroutine(GameObject.Find("player").GetComponent<touchFantome>().LunchLose());
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
            Pan[i].gameObject.transform.localScale = new Vector2(-Pan[i].gameObject.transform.localScale.x, Pan[i].gameObject.transform.localScale.y);
        }
    }


}
