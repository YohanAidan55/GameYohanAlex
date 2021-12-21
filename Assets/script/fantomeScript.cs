using UnityEngine;
using UnityEngine.SceneManagement;

public class fantomeScript : MonoBehaviour
{
    public int modeDeJeu;
    public bool move = false;
    int valApp;
    int n = 30;

    public bool isError = false;

    public string color; //La couleur est définit dans ce script

    public GameObject fantomeMechant;
    private GameObject FMInstantiate;
    public GameObject parent;

    bool ajout = false;

    void Update()
    {
        modeDeJeu = PlayerPrefs.GetInt("mode");      

        if (move == false)
        {
            this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -3) * Time.timeScale;
        }
        else
        {
            Move();
        }
    }
    

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "panier"){ 

            if(other.gameObject.GetComponent<PanContent>().color == color)
            {
                GameObject.Find("touch").GetComponent<Score>().sc += 1;

                int i; //index du tableau du collider panier
                GameObject[] listFant = other.gameObject.GetComponent<PanContent>().Pan;
                for (i = 0; i < listFant.Length; i++)
                {
                    if ((listFant[i] == null) && (ajout == false)) {    //accede seulement à la première case NonReorderableAttribute rempli
                        listFant[i] = this.gameObject;      //ajoute le fantome au tavbleau du panier
                        this.gameObject.transform.parent = other.gameObject.transform;    //définit le panier du fantome comme étant son parent
                        ajout = true;
                    }
                }
                isError = false;

            }else{
                isError = true;
            }

        }
        if ((other.gameObject.tag == "tapis") && (move == false))
        {
            if (modeDeJeu == 2)
            {
               // transformFantome();
            }
            else if (modeDeJeu == 1)
            {
                SceneManager.LoadScene("menuLose"); 
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "panier"){
              if (other.gameObject.GetComponent<PanContent>().color == color)
              {
                GameObject.Find("touch").GetComponent<Score>().sc -= 1;
              }
        }
        if ((other.gameObject.tag == "tapis") && (move == false))
        {
            if (modeDeJeu == 2)
            {
                transformFantome();
            }
            else if (modeDeJeu == 1)
            {
                SceneManager.LoadScene("menuLose"); 
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {   //active la funtion Move si c'est le bon panier
        if ((other.gameObject.tag == "panier") && (other.gameObject.GetComponent<PanContent>().color == color))
        {
            move = true;
        }
    }


    void transformFantome()
    {
        Destroy(gameObject);
        parent = GameObject.Find("NavMesh2D");
        
        FMInstantiate = Instantiate(fantomeMechant, transform.position, transform.rotation, parent.transform);
        FMInstantiate.GetComponent<IAMechant>().color = color; //Donne la couleur du fantome méchant
    }
    
    void Move()
    {    //pour chaque n=30 le fantome change de direction
        if (move == true)
        {
            n += 1;
            if (n > 30)
            {
                this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2) * Time.timeScale);
                n = 0;
            }
        }
    }
}