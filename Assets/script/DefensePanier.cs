using UnityEngine;
public class DefensePanier : MonoBehaviour
    {
        public int pointDeVie = 10;
        public string color ;
        public int valApp;
        private int n;
      
        // Update is called once per frame
        void Update()
        {
            if (pointDeVie == 0)
                {
                    Destroy(this.gameObject);
                }
        }
        
        void OnTriggerStay2D(Collider2D other)
        {   //active la funtion Move si c'est le bon panier
            if ((other.gameObject.tag == "fantomeMechant") && (other.gameObject.GetComponent<couleur>().color == this.gameObject.GetComponent<DefensePanier>().color))
            {
                n += (int)Time.timeScale;
                if (n >= valApp)
                {
                    pointDeVie -= 1;
                    n = 0;
                }
                
            }
        }
    }
