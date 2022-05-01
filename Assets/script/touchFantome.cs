using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class touchFantome : MonoBehaviour
{
    int modeDeJeu;

    public int status = 0;     //0 = nul --- 1 = begin --- 2 = move --- 3 =  end

    Vector2 iniPos;
    Vector2 startPos;
    Vector2 currentPos;

    public bool clicFantome = false; //verif si clic sur un fantome
    public bool release = true; // verif si lache
    GameObject fantom;

    bool exploded = false;
    bool mechantTouch = false;

    public bool useMouse;  //est à true pour l'objet touch uniquement

    // Start is called before the first frame update
    void Start()
    {
        modeDeJeu = PlayerPrefs.GetInt("mode");

        iniPos = new Vector2(-15, 0);

        status = -1; //si le status ne change pas alors on est pas en mode tactile
    }

    // Update is called once per frame
    void Update()
    {

            switch (status)         // gerer depuis touch => gestion touch
            {
                case 1:

                    if ((clicFantome == true)&& (fantom != null))
                    {
                        fantom.transform.position = this.transform.position;
                    }

                    release = false;

                    break;

                case 2:

                    if ((clicFantome == true)&&(fantom != null))
                    {
                        fantom.transform.position = this.transform.position;
                    }
                    exploded = true;    //si l'objet est en mouvement, alors la cartouche ne fonctionne pas 

                    break;

                case 3:

                    if ((clicFantome == true) && (fantom != null))
                    {
                        fantom.transform.position = this.transform.position;
                    }

                    break;

                case 4:

                    if (clicFantome)
                    {
                        if (fantom.GetComponent<fantomeScript>().isError)
                        {
                            StartCoroutine(LunchLose());
                        }

                        if ((!fantom.GetComponent<fantomeScript>().move) && ((fantom.transform.position.x < -1) || (fantom.transform.position.x > 1)))      //si le fantome est laché hors du tapis
                        {
                            if (modeDeJeu == 1)
                            {
                                StartCoroutine(LunchLose());
                            }
                            else
                            {
                            fantom.GetComponent<fantomeScript>().transformFantome();
                                fantom = null;
                            }
                        }

                        fantom.GetComponent<fantomeScript>().isCatch = false;
                    }

                    transform.position = iniPos;
                    clicFantome = false;
                    release = true;                   
                    fantom = null;
                    exploded = false;

                    status = 0;

                    break;
            }


        
            if (Input.GetMouseButtonDown(0) && useMouse && status == -1)    //souris
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

            if (Input.GetMouseButtonUp(0) && useMouse && status == -1)  //le touch retourne à son endroit inititial
            {
                if (clicFantome)
                {
                    if (fantom.GetComponent<fantomeScript>().isError)
                    {
                        StartCoroutine(LunchLose());
                    }

                    if ((!fantom.GetComponent<fantomeScript>().move) && ((fantom.transform.position.x < -1) || (fantom.transform.position.x > 1)))
                    {
                        if (modeDeJeu == 1)
                        {
                            StartCoroutine(LunchLose());
                        }
                        else
                        {
                            fantom.GetComponent<fantomeScript>().transformFantome();
                            fantom = null;
                        }                   
                    }

                    fantom.GetComponent<fantomeScript>().isCatch = false;
                }

                transform.position = iniPos;
                clicFantome = false;
                release = true;               
                fantom = null;
                exploded = false;
            }


            if ((release == false) && useMouse)
            {
                var ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
                currentPos = ray2.origin + ray2.direction;
                transform.position = currentPos;

                if ((clicFantome == true) && (fantom != null))
                {
                    fantom.transform.position = currentPos;
                }

                if ((Input.GetAxis("Mouse X") != 0) || (Input.GetAxis("Mouse Y") != 0))     //si la souris bouge, alors les cartouches anti-fatnomes ne fonctionnent pas
                {
                    exploded = true;
                }
            }



            if (mechantTouch)   //si le joueur a utilisé une cartourhce
            {
                    GameObject.Find("player").GetComponent<gestionTouch>().nbCartouche--;
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

        if ((other.gameObject.tag == "bonus") && !exploded)         //si le joueur touche un bonus non utilisé
        {
            other.gameObject.GetComponent<bonusScript>().BonusSelected();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
       if ((other.gameObject.tag == "fantomeMechant") && (GameObject.Find("player").GetComponent<gestionTouch>().nbCartouche > 0) && !release && !exploded)        //si le joueur veut utiliser une cartouche
       {
            Destroy(other.gameObject);
            mechantTouch = true;
        }
    }

    public IEnumerator LunchLose()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("menuLose");
    }
}
