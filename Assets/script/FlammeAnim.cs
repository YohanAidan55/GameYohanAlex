using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlammeAnim : MonoBehaviour
{
    [SerializeField]
    int anim;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().SetInteger("version", anim);
    }


}
