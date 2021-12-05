using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFantome : MonoBehaviour
{

    public Vector3 spawnPos;

    public GameObject fantR;
    public GameObject fantB;
    public GameObject fantJ;
    public GameObject fantV;

    GameObject[] fantArray;

    public int valApp; //fréquance d'apparition des fantomes
    public int nbApp = 1; //nombre de fanrtomes par apparitions

    public int[,] matriceApparition =       //matrice de taux d'apparition
    {
        { 250, 200, 150, 100, 50 },
        {250, 250, 200, 150, 100},
        {250, 250, 250, 200, 150},
        {250, 250, 250, 250, 200}
    };

    int n;

    int score;

    // Start is called before the first frame update
    void Start()
    {
        fantArray = new GameObject[] { fantR, fantB, fantJ, fantV };

        
    }

    // Update is called once per frame
    void Update()
    {
        score = gameObject.GetComponent<Score>().sc;    //récupère le score


        n += (int)Time.timeScale;
        if (n >= valApp)
        {
            for(int i = 1; i <= nbApp; i++)
            {
                int nb = UnityEngine.Random.Range(0, 4);
                spawn(nb);
                n = 0;
            }
        }


        valApp = GetValFromMatrice(score, matriceApparition);

    }

    private int GetValFromMatrice(int score, int[,] mat)
    {
        int x = nbApp - 1;
        int y = score / 10;             //ex : si score = 33 => colonne 3 de la matrice

        return mat[x, y];

    }

    void spawn(int color)
    {
        Instantiate(fantArray[color], spawnPos, Quaternion.identity);
    }

}
