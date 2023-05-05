using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class menuLose : MonoBehaviour
{
    public int score;

    [SerializeField]
    AudioSource backMusic;

    InterstitialAdsButton addButton;

    void Start()
    {
        addButton = GameObject.Find("AdsButton").GetComponent<InterstitialAdsButton>();

        Debug.Log("Menu lose");
        GameObject.Find("BackGroundMusic").GetComponent<AudioSource>().mute = false;

        if (PlayerPrefs.GetInt("Mute") == 0)
        {
            GameObject.Find("Main Camera").GetComponent<AudioListener>().enabled = true;
        }
        else
        {
            GameObject.Find("Main Camera").GetComponent<AudioListener>().enabled = false;
        }

        PlayerPrefs.SetInt("deadCount", PlayerPrefs.GetInt("deadCount") + 1);
        Debug.Log(PlayerPrefs.GetInt("deadCount"));
    }

    void Update()
        {
            score = PlayerPrefs.GetInt("score");
            GameObject.Find("ScoreFinal").GetComponent<Text>().text = "Score: " +score;
        }

    public void Play()
    {
        RunAd("game");
    }

    public void MainMenu()
    {
        RunAd("menu");
    }

    private void RunAd(string sc)
    {
        if (PlayerPrefs.GetInt("deadCount") == 3)
        {
            GameObject.Find("BackGroundMusic").GetComponent<AudioSource>().mute = true;
            addButton.ShowAd();
            PlayerPrefs.SetInt("deadCount", 0);
            addButton.waitEnd(sc);
        }
        else if (PlayerPrefs.GetInt("deadCount") > 3)
        {   
            addButton.LoadAd();
            PlayerPrefs.SetInt("deadCount", 0);
            SceneManager.LoadScene(sc);
        }
        else
        {
            SceneManager.LoadScene(sc);
        }
    }




}



