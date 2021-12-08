using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class lvlDefense : MonoBehaviour
{

    public int lvl;
    public float chanceExit;

    public GameObject fantome;
    public GameObject fantomeNoir;


    // Start is called before the first frame update
    void Start()
    {
        lvl = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void transformNoir(int a, int b)
    {
        if (((a == 1) && (b < 0.25)) || ((a == 2) && (b < 0.50)) || ((a == 3) && (b < 0.75)) || ((a == 4)))
        {   //en fonction du nombre de defense détruite, la chance de se transformer
            Instantiate(fantomeNoir, fantome.transform.position, fantome.transform.rotation);
            Destroy(fantome);
            fantome = null;
        }
    }
}
