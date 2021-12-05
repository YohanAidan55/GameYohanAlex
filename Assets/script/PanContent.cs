using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanContent : MonoBehaviour
{

    public GameObject[] Pan = new GameObject[10];    //liste avec les 10 fantomes dans le panier
    int i;

    // Start is called before the first frame update
    void Start()
    {
        
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
    }
}
