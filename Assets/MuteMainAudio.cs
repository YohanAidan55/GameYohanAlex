using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteMainAudio : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("BackGroundMusic").GetComponent<AudioSource>().mute = true;
    }


}
