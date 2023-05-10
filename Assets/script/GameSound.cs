using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSound : MonoBehaviour
{

    AudioSource[] sources;
    // Start is called before the first frame update
    void Start()
    {
        sources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];

        if(PlayerPrefs.GetInt("Mute") != 0)
        {
            Mute(true);
        }
        else
        {
            Mute(false);
        }
    }

    void Mute(bool val)
    {
        foreach(AudioSource asource in sources)
        {
            asource.mute = val;
        }
    }
}
