using UnityEngine;
public class DefensePanier : MonoBehaviour
    {
        public float pointDeVie = 1000;
        public string color;
        public bool angle;
        private int n;
        public int nbrEnnemy = 0;
        
        public Sprite spriteDetruit;

        private void Start()
        {
            color = gameObject.GetComponentInParent<PanContent>().color;
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
