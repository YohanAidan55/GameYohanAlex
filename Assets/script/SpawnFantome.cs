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
    public GameObject parent;

    GameObject[] fantArray;
    public GameObject[] tabBonus;   //tableau contenant les différents bonus

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
    int tauxAppBonus = 2; //plus ctte variable est grande, moins il y a de chance d'avoir un bonus
    int valAppBonus = 0;  // si un bonus doit apparaitre, temps en ms après le dernier fantome

    public int speedVariation = 0; //agmentation ou diminution de la vitesse si bonus/malus 
    int appMin = 60;
    public int appVariation = 0;

    int score;
    int valScore;  //combien de point par fantome ajouté

    // Start is called before the first frame update
    void Start()
    {
        fantArray = new GameObject[] { fantR, fantB, fantJ, fantV };


        valApp = GetValFromMatrice(score, matriceApparition);   //première ValApp
        valScore = 1;
    }

    // Update is called once per frame
    void Update()
    {
        score = gameObject.GetComponent<Score>().sc;    //récupère le score


        n += (int)Time.timeScale;

        if ((n >= valApp + speedVariation)&&(n >= appMin))   //speed variation : modifie la vitesse d'apparition des fantomes
        {
            int appBonus = UnityEngine.Random.Range(0, tauxAppBonus);

            if (appBonus == valAppBonus)
            {
                int nb = UnityEngine.Random.Range(0, tabBonus.Length);
                spawnBonus(nb);
                n = 0;
            }
            else
            {
                for (int i = 0; i < nbApp; i++)    //Pour faire apparaitre plusieurs fantomes
                {
                    int nb = UnityEngine.Random.Range(0, 4);
                    spawnFantome(nb);
                    n = 0;
                }
            }

            valApp = GetValFromMatrice(score, matriceApparition);
        }

        if(score > 10)
        {
            nbApp = score / 10 + 1 + appVariation;
        }
        

    }

    private int GetValFromMatrice(int score, int[,] mat)
    {

        int x = nbApp - 1;
        int y = score / 10;             //ex : si score = 33 => colonne 3 de la matrice

        return mat[x, y];

    }

    void spawnFantome(int color)
    {
        Instantiate(fantArray[color], spawnPos, Quaternion.identity, parent.transform);
    }

    void spawnBonus(int b)
    {
        Instantiate(tabBonus[b], spawnPos, Quaternion.identity);
    }

    public void SetSpeedVariation(int a)
    {
        speedVariation += a;
    }

    public void setValScore(int a)
    {
        valScore = a;
    }

    public int getValScore()
    {
        return valScore;
    }

}
