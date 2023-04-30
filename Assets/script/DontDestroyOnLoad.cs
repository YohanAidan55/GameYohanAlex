using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DontDestroyOnLoad : MonoBehaviour
{

    public static DontDestroyOnLoad instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }


        DontDestroyOnLoad(this.gameObject);

        instance = this;
    }
}

