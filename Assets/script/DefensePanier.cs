using UnityEngine;
public class DefensePanier : MonoBehaviour
    {
        public float pointDeVie = 1000;
        public string color;
        public int valApp;
        private int n;
        public int nbrEnnemy = 0;

        public Sprite spriteDetruit;

        private void Start()
        {
            color = this.gameObject.GetComponentInParent<PanContent>().color;
        }

        // Update is called once per frame
        void Update()
        {
             if (pointDeVie <= 0)
             {                       
                  this.gameObject.GetComponentInParent<PanContent>().isDestroy = true;
                  this.gameObject.GetComponent<SpriteRenderer>().sprite = spriteDetruit;
             }
        }     
        
        void OnTriggerEnter2D(Collider2D other)
        {
            if ((other.gameObject.tag == "fantomeMechant") && (color == other.gameObject.GetComponent<IAMechant>().color))
            {
                nbrEnnemy += 1;
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
