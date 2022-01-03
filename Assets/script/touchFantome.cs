using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class touchFantome : MonoBehaviour
{

    Vector2 iniPos;
    Vector2 startPos;
    Vector2 currentPos;

    bool clicFantome = false; //verif si clic sur un fantome
    bool release = true; // verif si lache
    int nb;
    GameObject fantom;

    public int nbCartouche; //nombre de cartouche antiFantome du joueur
    bool exploded = false;

    // Start is called before the first frame update
    void Start()
    {
        iniPos = transform.position;
        //nbCartouche = 0; //commence la partie avec 0 cartouche antiFantome
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.touchCount > 0)           //tactile
        {
            Touch touch = Input.GetTouch(nb);
            switch (touch.phase)
            {

                case TouchPhase.Began: //le touch prend la valeur de l'endroit où l'on clique

                    var ray = Camera.main.ScreenPointToRay(touch.position); //récupère la position du clic
                    startPos = ray.origin + ray.direction;

                    this.transform.position = startPos;

                    this.transform.position = new Vector3(transform.position.x, transform.position.y, -10);

                    if (clicFantome == true)
                    {
                        fantom.transform.position = startPos;
                    }

                    release = false;

                    break;

                case TouchPhase.Moved: //le touch suit le doigth

                    var ray2 = Camera.main.ScreenPointToRay(touch.position);
                    currentPos = ray2.origin + ray2.direction;

                    this.transform.position = currentPos;

                    this.transform.position = new Vector3(transform.position.x, transform.position.y, -10);

                    if (clicFantome == true)
                    {
                        fantom.transform.position = currentPos;
                    }

                    break;

                case TouchPhase.Ended: //le touch retourne à son endroit inititial

                    this.transform.position = iniPos;
                    clicFantome = false;
                    release = true;
                    fantom = null;

                    break;
            }
        }

        if (Input.GetMouseButtonDown(0))    //souris
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            startPos = ray.origin + ray.direction;
            transform.position = startPos;

            if ((clicFantome == true)&&(fantom != null))
            {
                fantom.transform.position = startPos;
            } 
            release = false;

            //exploded = true;
        }

        if (release == false)
        {
            var ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
            currentPos = ray2.origin + ray2.direction;
            transform.position = currentPos;

            if ((clicFantome == true)&&(fantom != null))
            {
                fantom.transform.position = currentPos;
            }
        }

        

        if (Input.GetMouseButtonUp(0))  //le touch retourne à son endroit inititial
        {
            if (clicFantome)
            {
                if (fantom.GetComponent<fantomeScript>().isError)
                {
                    SceneManager.LoadScene("menuLose");
                }
            }

            transform.position = iniPos;
            clicFantome = false;
            release = true;
            fantom = null;
            //exploded = false;
        }

    }


    void OnTriggerStay2D(Collider2D other)
    {
        if ((other.gameObject.tag == "fantome") && (other.gameObject.GetComponent<fantomeScript>().move == false))          //si le joueur touche un fantome pas emprisoné
        {
            clicFantome = true;
            fantom = other.gameObject;
        }

        if (other.gameObject.tag == "bonus")
        {
            other.gameObject.GetComponent<bonusScript>().BonusSelected();
        }

        /*if ((other.gameObject.tag == "fantomeMechant") && (nbCartouche > 0) && !release)
        {
            //this.GetComponent<CapsuleCollider2D>().enabled = true;
            exploded = true;
            //Destroy(other.gameObject);
            //nbCartouch--;
        }*/

    }

}
