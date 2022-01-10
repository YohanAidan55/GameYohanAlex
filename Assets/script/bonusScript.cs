using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bonusScript : MonoBehaviour
{

    public int n;   //type du bonus indiquant la fonction à utiliser

    GameObject spawn;

    bool isEffected = false; //indique si le bonus a été utilisé 

    // Start is called before the first frame update
    void Start()
    {
        spawn = GameObject.Find("touch");
    }


    // Update is called once per frame
    void Update()
    {
        this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -3) * Time.timeScale;
        if((this.transform.position.y <= -10)&&(!isEffected))
        {
            Destroy(this.gameObject);
        }
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
        GameObject.Find("PanierVert").GetComponent<PanContent>().resetRotation();   //repasse la rotation à 0 pour les fantomes dans les paniers

        GameObject.Find("PanierRouge").transform.position = GameObject.Find("PanierJaune").transform.position;
        GameObject.Find("PanierRouge").transform.rotation = GameObject.Find("PanierJaune").transform.rotation;
        GameObject.Find("PanierRouge").GetComponent<PanContent>().resetRotation();   //repasse la rotation à 0 pour les fantomes dans les paniers

        GameObject.Find("PanierJaune").transform.position = GameObject.Find("PanierBleu").transform.position;
        GameObject.Find("PanierJaune").transform.rotation = GameObject.Find("PanierBleu").transform.rotation;
        GameObject.Find("PanierJaune").GetComponent<PanContent>().resetRotation();   //repasse la rotation à 0 pour les fantomes dans les paniers

        GameObject.Find("PanierBleu").transform.position = x;
        GameObject.Find("PanierBleu").transform.rotation = y;
        GameObject.Find("PanierBleu").GetComponent<PanContent>().resetRotation();   //repasse la rotation à 0 pour les fantomes dans les paniers

        GameObject[] sysAngle = GameObject.FindGameObjectsWithTag("systeme");
        for(int i = 0; i < sysAngle.Length; i++)
        {
            if (sysAngle[i].GetComponent<DefensePanier>().angle)
            {
                sysAngle[i].transform.localPosition = new Vector2(sysAngle[i].transform.localPosition.x, -sysAngle[i].transform.localPosition.y);
            }
        }

        Destroy(this.gameObject);

    }


    IEnumerator SpeedUp()
    {
        spawn.GetComponent<SpawnFantome>().SetSpeedVariation(-50);     

        yield return new WaitForSeconds(10f);
        Debug.Log("timeEnd");

        spawn.GetComponent<SpawnFantome>().SetSpeedVariation(50);  //fin de la variation de la vitesse
        Destroy(this.gameObject);

    }

    IEnumerator SpeedDown()
    {
        spawn.GetComponent<SpawnFantome>().SetSpeedVariation(50);

        yield return new WaitForSeconds(10f);
        Debug.Log("timeEnd");

        spawn.GetComponent<SpawnFantome>().SetSpeedVariation(-50);  //fin de la variation de la vitesse
        Destroy(this.gameObject);

    }


    void AddCartouche()
    {
        spawn.GetComponent<touchFantome>().nbCartouche++;
        Destroy(this.gameObject);
    }


    IEnumerator Combo()
    {
        spawn.GetComponent<SpawnFantome>().setValScore(spawn.GetComponent<SpawnFantome>().getValScore() * 2);
        spawn.GetComponent<SpawnFantome>().SetSpeedVariation(-50);
        spawn.GetComponent<SpawnFantome>().appVariation = 0;

        yield return new WaitForSeconds(10f);
        Debug.Log("timeEnd");

        spawn.GetComponent<SpawnFantome>().setValScore(spawn.GetComponent<SpawnFantome>().getValScore() / 2);
        spawn.GetComponent<SpawnFantome>().SetSpeedVariation(50);
        spawn.GetComponent<SpawnFantome>().appVariation = 0;
        Destroy(this.gameObject);

    }

}
