using System;
using TMPro;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour{

    public int mode;

    [SerializeField] Sprite spMute, spDemute;
    [SerializeField] Image monImage;
    [SerializeField] TextMeshPro hsclassique, hsdynamique;

    public bool mute;

    InterstitialAdsButton addButton;

    void Awake()
    {


        GameObject.Find("BackGroundMusic").GetComponent<AudioSource>().mute = false;
        mute = PlayerPrefs.GetInt("Mute") != 0;
        if (SceneManager.GetActiveScene().name == "menu")
        {
            addButton = GameObject.Find("AdsButton").GetComponent<InterstitialAdsButton>();
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

        hsclassique.text = "Meilleur score: " + PlayerPrefs.GetInt("hsclassique");
        hsdynamique.text = "Meilleur score: " + PlayerPrefs.GetInt("hsdynamique"); ;
        
    }

    public void PlayMode1()
    {
        PlayerPrefs.SetInt("deadCount", PlayerPrefs.GetInt("deadCount") + 1);
        Debug.Log(PlayerPrefs.GetInt("deadCount"));
        mode = 1;
        PlayerPrefs.SetInt("mode", mode);
        addButton.RunAd("game");
    }
    
    public void PlayMode2()
    {
        PlayerPrefs.SetInt("deadCount", PlayerPrefs.GetInt("deadCount") + 1);
        Debug.Log(PlayerPrefs.GetInt("deadCount"));
        mode = 2;
        PlayerPrefs.SetInt("mode", mode);
        addButton.RunAd("game");
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
