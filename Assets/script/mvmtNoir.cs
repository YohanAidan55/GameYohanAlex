using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mvmtNoir : MonoBehaviour
{

    public int n;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        n += 1;
        if (n > 30)
        {
            this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-4, 2), Random.Range(-1, 3) * Time.timeScale);
            n = 0;
        }
    }
}
