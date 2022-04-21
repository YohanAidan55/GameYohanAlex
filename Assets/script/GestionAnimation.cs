
using UnityEngine;

public class GestionAnimation: MonoBehaviour
{
    public float delay;
   
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
