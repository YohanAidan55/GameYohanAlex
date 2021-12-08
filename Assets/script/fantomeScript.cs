using UnityEngine;
using UnityEngine.SceneManagement;

public class fantomeScript : MonoBehaviour
{
    public int modeDeJeu;
    public bool move = false;
    int valApp;
    int n = 30;

    public string color; //La couleur est définit dans ce script

    public GameObject fantomeMechant;
    private GameObject FMInstantiate;

    bool ajout = false;

    void Update()
    {
        modeDeJeu = PlayerPrefs.GetInt("mode");
        Move();

        if (move == false)
        {
            n++;
            if (n >= valApp)
            {
                float a = Random.value * 100;  //choisi une couleur aléatoire
                mass(a);
                n = 0;
            }
        }
    }
    
    void mass(float x)
    {
        if (x < 25)
        {
            this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1) * Time.timeScale;
        }
        if ((x < 50) && (x >= 25))
        {
            this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -2) * Time.timeScale;
        }
        if ((x < 75) && (x >= 50))
        {
            this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -5) * Time.timeScale;
        }
        if ((x <= 100) && (x >= 75))
        {
            this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -4) * Time.timeScale;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.tag == "panier") && (other.gameObject.GetComponent<PanContent>().color == color))
        {
            GameObject.Find("touch").GetComponent<Score>().sc += 1;

            int i; //index du tableau du collider panier
            GameObject[] listFant = other.gameObject.GetComponent<PanContent>().Pan;
            for(i = 0; i < listFant.Length; i++)
            {
                if ((listFant[i] == null) && (ajout == false)) {    //accede seulement à la première case NonReorderableAttribute rempli
                    listFant[i] = this.gameObject;
                    ajout = true;
                }
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
        if ((other.gameObject.tag == "panier") && (other.gameObject.GetComponent<PanContent>().color == color))
        {
            GameObject.Find("touch").GetComponent<Score>().sc -= 1;
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
        Destroy(this.gameObject);
        FMInstantiate = Instantiate(fantomeMechant, this.transform.position, this.transform.rotation);
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