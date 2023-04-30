using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DefensePanier : MonoBehaviour
    {
        public float pointDeVie = 1000;
        public string color;
        public bool angle;
        private int n;
        public int nbrEnnemy = 0;

        public Sprite spriteDetruit;
        [SerializeField] AudioClip panierDefenseClip;
        AudioSource _audioSource;


        private void Start()
        {
            _audioSource = GameObject.Find("Canvas").GetComponent<AudioSource>();
            color = gameObject.GetComponentInParent<PanContent>().color;
        }

        void Update()
        {
            foreach (Transform child in transform)
            {
                if (child.tag == "flame") 
                { 
                    child.gameObject.transform.localScale = new Vector3(2.5f - ((1000 - pointDeVie) / 1000), (2.5f - ((1000 - pointDeVie) / 1000)), 0f);


                    foreach (Transform child2 in child.transform)
                    {
                        if (child2.tag == "light")
                        {
                            child2.gameObject.GetComponent<Light2D>().intensity = 1.6f - ((1000 - pointDeVie) / 1000);
                        }
                    }
                }            

            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if ((other.gameObject.tag == "fantomeMechant") && (color == other.gameObject.GetComponent<IAMechant>().color))
            {
                nbrEnnemy += 1;
            }
        }
        
        void OnTriggerStay2D(Collider2D other)
        {
            if ((other.gameObject.tag == "fantomeMechant") && (color == other.gameObject.GetComponent<IAMechant>().color))
            {
                if (pointDeVie == 0)
                {
                    _audioSource.clip = panierDefenseClip;
                    _audioSource.Play();
                    gameObject.GetComponentInParent<PanContent>().isDestroy = true;
                    Destroy(gameObject.transform.GetChild(0).gameObject);
                }
            }
        }
        
        void OnTriggerExit2D(Collider2D other)
        {
            if ((other.gameObject.tag == "fantomeMechant") && (color == other.gameObject.GetComponent<IAMechant>().color))
            {
                nbrEnnemy -= 1;
            }
        }
    }
