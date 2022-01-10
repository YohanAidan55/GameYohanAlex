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
    public bool release = true; // verif si lache
    public int nb;
    GameObject fantom;

    public int nbCartouche; //nombre de cartouche antiFantome du joueur
    bool exploded = false;
    bool mechantTouch = false;

    // Start is called before the first frame update
    void Start()
    {
        iniPos = transform.position;
        nbCartouche = 0; //commence la partie avec 0 cartouche antiFantome
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.touchCount > nb)           //tactile
        {
            Touch touch = Input.GetTouch(nb);
            switch (touch.phase)
            {

                case TouchPhase.Began: //le touch prend la valeur de l'endroit où l'on clique

                    var ray = Camera.main.ScreenPointToRay(touch.position); //récupère la position du clic
                    //startPos = Camera.main.ScreenToWorldPoint(touch.position);
                    startPos = ray.origin + ray.direction;

                    this.transform.position = startPos;

                  //  this.transform.position = new Vector3(transform.position.x, transform.position.y, -10);

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

                    //this.transform.position = new Vector3(transform.position.x, transform.position.y, -10);

                    if (clicFantome == true)
                    {
                        fantom.transform.position = currentPos;
                    }

                    exploded = true;

                    break;

                case TouchPhase.Ended: //le touch retourne à son endroit inititial

                    if (clicFantome)
                    {
                        if (fantom.GetComponent<fantomeScript>().isError)
                        {
                            LunchLose();
                        }

                        if (!fantom.GetComponent<fantomeScript>().move)
                        {
                            fantom.GetComponent<fantomeScript>().transformFantome();
                            fantom = null;
                        }
                    }

                    transform.position = iniPos;
                    clicFantome = false;
                    release = true;
                    fantom = null;
                    exploded = false;

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

            if ((Input.GetAxis("Mouse X") != 0) || (Input.GetAxis("Mouse Y") != 0))     //si la souris bouge, alors les cartouches anti-fatnomes ne fonctionnent pas
            {
                exploded = true;
            }
        }

        

        if (Input.GetMouseButtonUp(0))  //le touch retourne à son endroit inititial
        {
            if (clicFantome)
            {
                if (fantom.GetComponent<fantomeScript>().isError)
                {
                    LunchLose();
                }

                if (!fantom.GetComponent<fantomeScript>().move)
                {
                    fantom.GetComponent<fantomeScript>().transformFantome();
                    fantom = null;
                }
            }

            transform.position = iniPos;
            clicFantome = false;
            release = true;
            fantom = null;
            exploded = false;
        }

        if (mechantTouch)   //si le joueur a utilisé une cartourhce
        {
            nbCartouche--;
            mechantTouch = false;
        }

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if ((other.gameObject.tag == "fantome") && (other.gameObject.GetComponent<fantomeScript>().move == false) && (fantom == null))          //si le joueur touche un fantome pas emprisoné, et qu'il en a pas déjà attrapé un
        {
            clicFantome = true;
            fantom = other.gameObject;
            fantom.GetComponent<fantomeScript>().isCatch = true;
        }

        if ((other.gameObject.tag == "bonus") && !exploded)
        {
            other.gameObject.GetComponent<bonusScript>().BonusSelected();
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
       if ((other.gameObject.tag == "fantomeMechant") && (nbCartouche > 0) && !release && !exploded)
       {
            Destroy(other.gameObject);
            mechantTouch = true;
        }
    }


    public void LunchLose()
    {
        SceneManager.LoadScene("menuLose");
    }
}
