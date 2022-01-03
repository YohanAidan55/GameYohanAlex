using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bonusScript : MonoBehaviour
{

    public int n;   //type du bonus indiquant la fonction à utiliser

    GameObject spawn;

    // Start is called before the first frame update
    void Start()
    {
        spawn = GameObject.Find("touch");
    }


    // Update is called once per frame
    void Update()
    {
        this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -3) * Time.timeScale;
    }


    public void BonusSelected()
    {
        if (n == 0)
        {
            MelangePanier();
        }
        else if (n == 1)
        {
            SpeedUp();
        }
        else if (n == 2)
        {
            SpeedDown();
        }
        else if (n == 3)
        {
            AddCartouche();
        }

            Destroy(this.gameObject);
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

    }


    void SpeedUp()
    {
        spawn.GetComponent<SpawnFantome>().SetSpeedVariation(-50);
        this.gameObject.tag = null;        //retire le tag pour ne pas recliquer sur le bonus
        this.gameObject.GetComponent<SpriteRenderer>().sprite = null;       //retire le sprite

        LancerTimer(1000);

        spawn.GetComponent<SpawnFantome>().SetSpeedVariation(50);  //fin de la variation de la vitesse
    }

    void SpeedDown()
    {
        spawn.GetComponent<SpawnFantome>().SetSpeedVariation(50);
        this.gameObject.tag = null;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = null;

        LancerTimer(1000);

        spawn.GetComponent<SpawnFantome>().SetSpeedVariation(-50);  //fin de la variation de la vitesse
    }


    void AddCartouche()
    {
        spawn.GetComponent<touchFantome>().nbCartouche++;
    }


    void LancerTimer(int n)
    {
        while(n > 0)
        {
            n -= (int)Time.timeScale;
        }
    }

}
