using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bonusScript : MonoBehaviour
{

    public int n;   //type du bonus indiquant la fonction � utiliser
    public int proba;

    GameObject spawn;

    public int speed;

    bool isEffected = false; //indique si le bonus a �t� utilis� 
    
    private int timeRemaining = 11;

    // Start is called before the first frame update
    void Start()
    {
        spawn = GameObject.Find("player");
    }


    // Update is called once per frame
    void Update()
    {
        this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed) * Time.timeScale;
        if((this.transform.position.y <= -10)&&(!isEffected))
        {
            Destroy(this.gameObject);
        }
    }

    void DisplayTime(float time)
    {
        GameObject.Find("ProgressIndicator").GetComponent<radialProgress>().time = time;
    }

    public void BonusSelected()
    {
        if (!isEffected)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = null;       //retire le sprite
            if (n == 0)
            {
                MelangePanier();
            }
            else if (n == 1)
            {
                StartCoroutine(SpeedUp());
                
            }
            else if (n == 2)
            {
                StartCoroutine(SpeedDown());
            }
            else if (n == 3)
            {
                AddCartouche();
            }
            else if (n == 4)
            {
                StartCoroutine(Combo());
            }else if(n == 5)
            {
                Bombe();
            }
        }

        isEffected = true;  //empeche le joueur de cliquer deux fois sur le bonus
    }


    void MelangePanier()
    {
        Transform vert = GameObject.Find("PanierVert").transform;
        Vector3 x = vert.transform.position;
        Quaternion y = vert.transform.rotation;

        GameObject.Find("PanierVert").transform.position = GameObject.Find("PanierRouge").transform.position;
        GameObject.Find("PanierVert").transform.rotation = GameObject.Find("PanierRouge").transform.rotation;
        GameObject.Find("PanierVert").GetComponent<PanContent>().resetRotation();   //repasse la rotation � 0 pour les fantomes dans les paniers

        GameObject.Find("PanierRouge").transform.position = GameObject.Find("PanierJaune").transform.position;
        GameObject.Find("PanierRouge").transform.rotation = GameObject.Find("PanierJaune").transform.rotation;
        GameObject.Find("PanierRouge").GetComponent<PanContent>().resetRotation();   //repasse la rotation � 0 pour les fantomes dans les paniers

        GameObject.Find("PanierJaune").transform.position = GameObject.Find("PanierBleu").transform.position;
        GameObject.Find("PanierJaune").transform.rotation = GameObject.Find("PanierBleu").transform.rotation;
        GameObject.Find("PanierJaune").GetComponent<PanContent>().resetRotation();   //repasse la rotation � 0 pour les fantomes dans les paniers

        GameObject.Find("PanierBleu").transform.position = x;
        GameObject.Find("PanierBleu").transform.rotation = y;
        GameObject.Find("PanierBleu").GetComponent<PanContent>().resetRotation();   //repasse la rotation � 0 pour les fantomes dans les paniers

        GameObject[] sysAngle = GameObject.FindGameObjectsWithTag("systeme");
        for(int i = 0; i < sysAngle.Length; i++)
        {
            if (sysAngle[i].GetComponent<DefensePanier>().angle)
            {
                sysAngle[i].transform.localPosition = new Vector2(sysAngle[i].transform.localPosition.x, -sysAngle[i].transform.localPosition.y);
            }
        }

        Destroy(this.gameObject);

        GameObject[]  objs = GameObject.FindGameObjectsWithTag("fantomeMechant");
        foreach (var obj in objs)
        {
            obj.GetComponent<IAMechant>().isUpdateEnable = true;
        }

    }


    IEnumerator SpeedUp()
    {
        spawn.GetComponent<SpawnFantome>().SetSpeedVariation(-50);
        DisplayTime(timeRemaining);
        yield return new WaitForSeconds (timeRemaining);
        Debug.Log("timeEnd");

        spawn.GetComponent<SpawnFantome>().SetSpeedVariation(50);  //fin de la variation de la vitesse
        Destroy(this.gameObject);

    }

    IEnumerator SpeedDown()
    {
        spawn.GetComponent<SpawnFantome>().SetSpeedVariation(50);
        DisplayTime(timeRemaining);
        yield return new WaitForSeconds(timeRemaining);
        Debug.Log("timeEnd");

        spawn.GetComponent<SpawnFantome>().SetSpeedVariation(-50);  //fin de la variation de la vitesse
        Destroy(this.gameObject);

    }


    void AddCartouche()
    {
        spawn.GetComponent<gestionTouch>().nbCartouche++;
        Destroy(this.gameObject);
    }

    
    IEnumerator Combo()
    {
        spawn.GetComponent<SpawnFantome>().setValScore(spawn.GetComponent<SpawnFantome>().getValScore() * 2);
        spawn.GetComponent<SpawnFantome>().SetSpeedVariation(-80);
        spawn.GetComponent<SpawnFantome>().appVariation = 1;
        DisplayTime(timeRemaining);
        yield return new WaitForSeconds(timeRemaining);
        Debug.Log("timeEnd");

        spawn.GetComponent<SpawnFantome>().setValScore(spawn.GetComponent<SpawnFantome>().getValScore() / 2);
        spawn.GetComponent<SpawnFantome>().SetSpeedVariation(80);
        spawn.GetComponent<SpawnFantome>().appVariation = 3;
        Destroy(this.gameObject);

    }


    void Bombe()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("fantomeMechant");
        foreach (var obj in objs)
        {
            Destroy(obj);
        }

        Destroy(this.gameObject);

    }

}
