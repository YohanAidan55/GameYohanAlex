using UnityEngine;
public class DefensePanier : MonoBehaviour
    {
        public int pointDeVie = 1000;
        public string color;
        public int valApp;
        private int n;

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
    }
