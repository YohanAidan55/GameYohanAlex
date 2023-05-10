using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour{

    public int mode;

    [SerializeField] Sprite spMute, spDemute;
    [SerializeField] Image monImage;

    public bool mute;

    void Awake()
    {
        GameObject.Find("BackGroundMusic").GetComponent<AudioSource>().mute = false;
        mute = PlayerPrefs.GetInt("Mute") != 0;
        if (SceneManager.GetActiveScene().name == "menu")
        {
            GameObject.Find("Mute").GetComponent<Image>().sprite = mute ? spMute : spDemute;
        }

        if (mute)
        {
            GameObject.Find("BackGroundMusic").GetComponent<AudioSource>().mute = true;
        }
        else
        {
            GameObject.Find("BackGroundMusic").GetComponent<AudioSource>().mute = false;
        }

        

    }

    public void PlayMode1()
    {
        mode = 1;
        PlayerPrefs.SetInt("mode", mode);
        PlayerPrefs.SetInt("deadCount", 0);
        SceneManager.LoadScene("game");
    }
    
    public void PlayMode2()
    {
        mode = 2;
        PlayerPrefs.SetInt("mode", mode);
        PlayerPrefs.SetInt("deadCount", 0);
        SceneManager.LoadScene("game");
    }


    public void LoadCredit()
    {
        SceneManager.LoadScene("credit");

    }

    public void loadBonusList()
    {
        SceneManager.LoadScene("listeDesBonus");
    }

    public void MuteSong()
    {
        mute = !mute;
        setPlayerParam(mute);

        GameObject.Find("Mute").GetComponent<Image>().sprite = mute ? spMute : spDemute;
        GameObject.Find("BackGroundMusic").GetComponent<AudioSource>().mute = mute;
    }

    void setPlayerParam(bool b)
    {
        int i = b ? 1 : 0;
        PlayerPrefs.SetInt("Mute", i);
    }
}
